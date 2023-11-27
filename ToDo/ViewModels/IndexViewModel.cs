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

namespace ToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private string welcomeTitle;
        private string userName = "杜炆洋";
        private ObservableCollection<TaskBar> taskBarList;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IDialogHostService service;

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public IndexViewModel(IDialogHostService service)
        {
            InitTaskBar();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            string NowTime = DateTime.Now.ToString("yyy年MM月dd日dddd");
            WelcomeTitle = "你好，" + UserName + "! " + NowTime;

            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.service = service;
        }


        private void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo":
                    AddToDo();
                    break;
                case "AddMemo":
                    AddMemo();
                    break;
            }
        }

        private void AddToDo()
        {
            service.ShowDialog("AddToDoView", null);
        }

        private void AddMemo()
        {
            service.ShowDialog("AddMemoView", null);
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
