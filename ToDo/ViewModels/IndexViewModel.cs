using ToDo.Common.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared.Dtos;
using Prism.Commands;
using ToDo.Views.DialogViews;
using Prism.Services.Dialogs;
using ToDo.Common;
using Prism.Ioc;
using ToDo.Services.ServiceImpl;
using ToDo.Services;
using System.Numerics;
using Prism.Regions;
using ToDo.Extensions;
using System.Threading.Tasks.Dataflow;

namespace ToDo.ViewModels
{
    public class IndexViewModel : NavigationViewMode
    {
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private string welcomeTitle;
        private string userName = "杜炆洋";
        private SummeryDto summery;
        private ObservableCollection<TaskBar> taskBarList;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IRegionManager regionManager;
        private readonly IDialogHostService service;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<ToDoDto> ToDoCompltedCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }


        /// <summary>
        /// 构造方法
        /// </summary>
        public IndexViewModel(IDialogHostService service, IContainerProvider provider) : base(provider)
        {
            InitTaskBar();
            //string NowTime = DateTime.Now.ToString("yyy年MM月dd日dddd");
            WelcomeTitle = "你好，" + UserName + DateTime.Now.GetDateTimeFormats('D')[1].ToString();

            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            ToDoCompltedCommand = new DelegateCommand<ToDoDto>(Complted);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            this.regionManager = provider.Resolve<IRegionManager>();
            this.service = service;

            toDoService = provider.Resolve<IToDoService>();
            memoService = provider.Resolve<MemoService>();

        }

        private void Navigate(TaskBar bar)
        {
            if (string.IsNullOrWhiteSpace(bar.Target))
                return;
            NavigationParameters param = new NavigationParameters();
            if (bar.Title == "已完成")
            {
                param.Add("Value", 2);
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.Target, param);
        }

        private async void Complted(ToDoDto dto)
        {
            UpdateLoading(true);
            try
            {
                UpdateLoading(true);
                var updateResult = await toDoService.UpdateAsync(dto);
                if (updateResult.Status)
                {
                    var todo = Summery.ToDoList.FirstOrDefault(t => t.Id.Equals(dto.Id));
                    if (todo != null)
                    {
                        Summery.CompletedCount += 1;
                        Summery.ToDoList.Remove(todo);
                        Summery.CompletedRatio = (Summery.CompletedCount / (double)Summery.Sum).ToString("0%");
                        this.Refresh();
                    }
                    aggregator.SendMessage("已完成！");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                UpdateLoading(false);
            }

        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo":
                    AddToDo(null);
                    break;
                case "AddMemo":
                    AddMemo(null);
                    break;
            }
        }

        private async void AddToDo(ToDoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await service.ShowDialog("AddToDoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");
                if (todo.Id > 0)
                {
                    var updateResult = await toDoService.UpdateAsync(todo);
                    if (updateResult.Status)
                    {
                        var todoModel = Summery.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id));
                        if (todoModel != null)
                        {
                            todoModel.Title = todo.Title;
                            todoModel.Content = todo.Content;
                        }
                        aggregator.SendMessage("修改成功！");
                    }
                }
                else
                {
                    var addResult = await toDoService.AddAsync(todo);
                    if (addResult.Status)
                    {
                        Summery.Sum += 1;
                        Summery.ToDoList.Add(addResult.Result);
                        Summery.CompletedRatio = (Summery.CompletedCount / (double)Summery.Sum).ToString("0%");
                        this.Refresh();
                    }
                    aggregator.SendMessage("添加成功！");
                }

            }

        }

        private async void AddMemo(MemoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var dialogResult = await service.ShowDialog("AddMemoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                if (memo.Id > 0)
                {
                    var updateResult = await memoService.UpdateAsync(memo);
                    if (updateResult.Status)
                    {
                        var memoModel = Summery.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                        if (memoModel != null)
                        {
                            memoModel.Title = memo.Title;
                            memoModel.Content = memo.Content;
                            aggregator.SendMessage("修改成功！");
                        }
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Status)
                    {
                        Summery.MemoCount += 1;
                        Summery.MemoList.Add(addResult.Result);
                        this.Refresh();
                    }
                    aggregator.SendMessage("添加成功！");
                }
            }
        }

        /// <summary>
        /// 初始化任务栏方法 
        /// </summary>
        public void InitTaskBar()
        {
            TaskBarList = new ObservableCollection<TaskBar>
            {
                new TaskBar { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "ToDoView" },
                new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "ToDoView" },
                new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "" },
                new TaskBar { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "MemoView" }
            };
        }
        public void Refresh()
        {
            TaskBarList[0].Content = summery.Sum.ToString();
            TaskBarList[1].Content = summery.CompletedCount.ToString();
            TaskBarList[2].Content = summery.CompletedRatio;
            TaskBarList[3].Content = summery.MemoCount.ToString();

        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                UpdateLoading(true);
                var SummaryResult = await toDoService.SummaryAsync();
                if (SummaryResult.Status)
                {
                    Summery = SummaryResult.Result;
                    Refresh();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }


        }

        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public string WelcomeTitle
        {
            get { return welcomeTitle; }
            set { welcomeTitle = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 任务栏集合
        /// </summary>
        public ObservableCollection<TaskBar> TaskBarList
        {
            get { return taskBarList; }
            set { taskBarList = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 汇总
        /// </summary>
        public SummeryDto Summery
        {
            get { return summery; }
            set { summery = value; RaisePropertyChanged(); }
        }
        #endregion


    }
}
