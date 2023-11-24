
using ToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared.Dtos;

namespace ToDo.ViewModels
{
    public class ToDoViewModel : BindableBase
    {
        private ObservableCollection<ToDoDto> toDoDtos;
        private bool isRightDrawerOpen;

        public DelegateCommand AddCommand { get; set; }
        public ToDoViewModel()
        {
            AddCommand = new DelegateCommand(() => { IsRightDrawerOpen = true; });
            TestData();

        }

        private void TestData()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            for (int i = 0; i < 20; i++)
            {
                ToDoDtos.Add(new ToDoDto { Title = "待办事项" + i, Content = "内容" + i });
            }
        }
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; }
        }
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
    }
}
