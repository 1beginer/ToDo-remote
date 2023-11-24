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

namespace ToDo.ViewModels
{
    public class MemoViewModel : NavigationViewMode
    {
        private ObservableCollection<MemoDto> memoDtos;
        private bool isRightDrawerOpen;
        private readonly IMemoService memoService;

        public DelegateCommand AddCommand { get; set; }
        public MemoViewModel(IMemoService memoService, IContainerProvider provider) : base(provider)
        {
            AddCommand = new DelegateCommand(() => { IsRightDrawerOpen = true; });
            this.memoService = memoService;
            MemoDtos = new ObservableCollection<MemoDto>();

        }

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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetDataAsync();
        }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; }
        }
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }
    }
}
