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
    public class MemoViewModel : BindableBase
    {
        private ObservableCollection<MemoDto> memoDtos;
        private bool isRightDrawerOpen;

        public DelegateCommand AddCommand { get; set; }
        public MemoViewModel()
        {
            AddCommand = new DelegateCommand(() => { IsRightDrawerOpen = true; });
            TestData();

        }

        private void TestData()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto { Title = "备忘录" + i, Content = "内容" + i });
            }
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
