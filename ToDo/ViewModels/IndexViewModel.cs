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

namespace ToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private string welcomeTitle;
        private string userName = "杜炆洋";
        private ObservableCollection<TaskBar> taskBarList;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;

        /// <summary>
        /// 构造方法
        /// </summary>
        public IndexViewModel()
        {
            string NowTime = DateTime.Now.ToString("yyy年MM月dd日dddd");
            WelcomeTitle = "你好，" + UserName + "! " + NowTime;
            InitTaskBar();
            TestData();
        }


        /// <summary>
        /// 初始化任务栏 
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
        private void TestData()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();

            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto { Title = "待办事项" + i, Content = "内容" + i });
                MemoDtos.Add(new MemoDto { Title = "备忘录" + i, Content = "内容" + i });
            }
        }
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
    }
}
