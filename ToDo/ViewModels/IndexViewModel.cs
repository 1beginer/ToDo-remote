﻿using ToDo.Common.Models;
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

namespace ToDo.ViewModels
{
    public class IndexViewModel : NavigationViewMode
    {
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private string welcomeTitle;
        private string userName = "杜炆洋";
        private ObservableCollection<TaskBar> taskBarList;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IDialogHostService service;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<ToDoDto> ToDoCompltedCommand { get; private set; }


        /// <summary>
        /// 构造方法
        /// </summary>
        public IndexViewModel(IDialogHostService service, IContainerProvider provider) : base(provider)
        {
            InitTaskBar();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            string NowTime = DateTime.Now.ToString("yyy年MM月dd日dddd");
            WelcomeTitle = "你好，" + UserName + "! " + NowTime;

            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            ToDoCompltedCommand = new DelegateCommand<ToDoDto>(Complted);
            this.service = service;

            toDoService = provider.Resolve<IToDoService>();
            memoService = provider.Resolve<MemoService>();

        }

        private async void Complted(ToDoDto dto)
        {
            var updateResult = await toDoService.UpdateAsync(dto);
            if (updateResult.Status)
            {
                var todo = ToDoDtos.FirstOrDefault(t => t.Id.Equals(dto.Id));
                if (todo != null)
                {
                    ToDoDtos.Remove(todo);
                }
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
                        var todoModel = ToDoDtos.FirstOrDefault(t => t.Id.Equals(todo.Id));
                        if (todoModel != null)
                        {
                            todoModel.Title = todo.Title;
                            todoModel.Content = todo.Content;

                        }
                    }
                }
                else
                {
                    var addResult = await toDoService.AddAsync(todo);
                    if (addResult.Status)
                    {
                        ToDoDtos.Add(addResult.Result);
                    }
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
                        var memoModel = ToDoDtos.FirstOrDefault(t => t.Id.Equals(memo.Id));
                        if (memoModel != null)
                        {
                            memoModel.Title = memo.Title;
                            memoModel.Content = memo.Content;

                        }
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Status)
                    {
                        MemoDtos.Add(addResult.Result);
                    }
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
                new TaskBar { Icon = "ClockFast", Title = "汇总", Content = "9", Color = "#FF0CA0FF", Target = "" },
                new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Content = "27", Color = "#FF1ECA3A", Target = "" },
                new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Content = "100%", Color = "#FF02C6DC", Target = "" },
                new TaskBar { Icon = "PlaylistStar", Title = "备忘录", Content = "13", Color = "#FFFFA000", Target = "" }
            };
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
        /// 待办集合
        /// </summary>
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 备忘集合
        /// </summary>
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        #endregion


    }
}
