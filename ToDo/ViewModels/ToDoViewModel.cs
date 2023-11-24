
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
using ToDo.Services.ServiceImpl;

namespace ToDo.ViewModels
{
    public class ToDoViewModel : BindableBase
    {
        private ObservableCollection<ToDoDto> toDoDtos;
        private bool isRightDrawerOpen;
        private readonly IToDoService toDoService;

        public DelegateCommand AddCommand { get; set; }
        public ToDoViewModel(IToDoService toDoService)
        {
            AddCommand = new DelegateCommand(() => { IsRightDrawerOpen = true; });
            this.toDoService = toDoService;
            ToDoDtos = new ObservableCollection<ToDoDto>();
            CreateToDoList();
        }

        private async void CreateToDoList()
        {
            var todoResult = await toDoService.GetPageListAsync(new Shared.Parameters.QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
            });
            if (todoResult.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
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
