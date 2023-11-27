
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
using ToDo.Shared.Parameters;
using Prism.Ioc;
using Prism.Regions;
using Microsoft.Win32;

namespace ToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewMode
    {
        private readonly IToDoService toDoService;

        private ObservableCollection<ToDoDto> toDoDtos;
        private bool isRightDrawerOpen;

        private ToDoDto currentToDo;
        private string search;
        private int? selectedIndex;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        public ToDoViewModel(IToDoService toDoService, IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Select);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            this.toDoService = toDoService;
            ToDoDtos = new ObservableCollection<ToDoDto>();

        }

        /// <summary>
        /// 普通命令整合
        /// </summary>
        /// <param name="obj"></param>
        private async void Execute(string obj)
        {
            switch (obj)
            {
                case "add":
                    CurrentToDo = new ToDoDto();
                    IsRightDrawerOpen = true;
                    break;
                case "inquire":
                    GetDataAsync();
                    break;
                case "save":
                    Save();
                    break;

            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);
            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;
            var todoResult = await toDoService.GetPageListAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
            });
            if (todoResult.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
            }

            UpdateLoading(false);
        }
        /// <summary>
        /// 被导航时
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetDataAsync();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentToDo.Title) || string.IsNullOrWhiteSpace(CurrentToDo.Content))
                return;
            UpdateLoading(true);
            try
            {
                if (CurrentToDo.Id > 0)
                {
                    var updateResult = await toDoService.UpdateAsync(CurrentToDo);
                    if (updateResult.Status)
                    {
                        var todo = toDoDtos.FirstOrDefault(t => t.Id == CurrentToDo.Id);
                        //修改集合内的数据，当然也可以再次调用查询方法但是可以不访问数据库，应该此方法性能更好
                        if (todo != null)
                        {
                            todo.Title = CurrentToDo.Title;
                            todo.Content = CurrentToDo.Content;
                            todo.Status = CurrentToDo.Status;
                        }
                        IsRightDrawerOpen = false;
                    }
                }
                else
                {
                    var addResult = await toDoService.AddAsync(CurrentToDo);
                    if (addResult.Status)
                    {
                        ToDoDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
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

        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="dto"></param>
        private async void Delete(ToDoDto dto)
        {
            try
            {
                UpdateLoading(true);
                var deleteResult = await toDoService.DeletedAsync(dto.Id);
                if (deleteResult.Status)
                {
                    var Dtodo = ToDoDtos.FirstOrDefault(t => t.Id == dto.Id);
                    ToDoDtos.Remove(Dtodo);
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

        /// <summary>
        /// 查找单个数据
        /// </summary>
        /// <param name="dto"></param>
        private async void Select(ToDoDto dto)
        {
            try
            {
                UpdateLoading(true);
                var todoResult = await toDoService.GetFirstOfDefaultAsync(dto.Id);
                if (todoResult.Status)
                {
                    CurrentToDo = todoResult.Result;
                    IsRightDrawerOpen = true;
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

        /// <summary>
        /// 数据展示集合
        /// </summary>
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; }
        }
        /// <summary>
        /// 右边栏控制
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public ToDoDto CurrentToDo
        {
            get { return currentToDo; }
            set { currentToDo = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 索引状态
        /// </summary>
        public int? SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }
    }

}
