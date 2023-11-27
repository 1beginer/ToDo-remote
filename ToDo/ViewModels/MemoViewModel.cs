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
using ToDo.Services;
using ToDo.Shared.Parameters;
using Prism.Ioc;
using Prism.Regions;
using System.CodeDom.Compiler;
using System.Windows.Controls.Primitives;
using MaterialDesignColors;

namespace ToDo.ViewModels
{
    public class MemoViewModel : NavigationViewMode
    {
        private ObservableCollection<MemoDto> memoDtos;
        private bool isRightDrawerOpen;
        private readonly IMemoService memoService;
        private MemoDto currentMemo;
        private string search;

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }

        public MemoViewModel(IMemoService memoService, IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<MemoDto>(Select);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            this.memoService = memoService;
            MemoDtos = new ObservableCollection<MemoDto>();

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
                    CurrentMemo = new MemoDto();
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
        /// 获取数据方法
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);
            var memoResult = await memoService.GetPageListAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
            });
            if (memoResult.Status)
            {
                MemoDtos.Clear();
                foreach (var item in memoResult.Result.Items)
                {
                    MemoDtos.Add(item);
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
        /// 数据展示集合
        /// </summary>
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
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
        public MemoDto CurrentMemo
        {
            get { return currentMemo; }
            set { currentMemo = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentMemo.Title) || string.IsNullOrWhiteSpace(CurrentMemo.Content))
                return;
            UpdateLoading(true);
            try
            {
                if (CurrentMemo.Id > 0)
                {
                    var updateResult = await memoService.UpdateAsync(CurrentMemo);
                    if (updateResult.Status)
                    {
                        var memo = MemoDtos.FirstOrDefault(t => t.Id == CurrentMemo.Id);
                        //修改集合内的数据，当然也可以再次调用查询方法但是可以不访问数据库，应该此方法性能更好
                        if (memo != null)
                        {
                            memo.Title = CurrentMemo.Title;
                            memo.Content = CurrentMemo.Content;

                        }
                        IsRightDrawerOpen = false;
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(CurrentMemo);
                    if (addResult.Status)
                    {
                        MemoDtos.Add(addResult.Result);
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
        /// 查找单个数据
        /// </summary>
        /// <param name="dto"></param>
        private async void Select(MemoDto dto)
        {
            try
            {
                UpdateLoading(true);
                var memoResult = await memoService.GetFirstOfDefaultAsync(dto.Id);
                if (memoResult.Status)
                {
                    CurrentMemo = memoResult.Result;
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
        /// 删除命令
        /// </summary>
        /// <param name="dto"></param>
        private async void Delete(MemoDto dto)
        {
            try
            {
                UpdateLoading(true);
                var deleteResult = await memoService.DeletedAsync(dto.Id);
                if (deleteResult.Status)
                {
                    var Dtodo = MemoDtos.FirstOrDefault(t => t.Id == dto.Id);
                    MemoDtos.Remove(Dtodo);
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
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
    }
}
