using ToDo.Common.Models;
using ToDo.Extensions;
using ToDo.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using ToDo.Common;
using DryIoc;
using Prism.Ioc;

namespace ToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase, IConfigurationService
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<SideBar> sideBars;
        public ObservableCollection<SideBar> SideBars
        {
            get { return sideBars; }
            set { sideBars = value; RaisePropertyChanged(); }
        }

        private readonly IContainerProvider provider;
        private readonly IRegionManager regionManager;
        private readonly IDialogHostService service;
        private IRegionNavigationJournal journal;

        public DelegateCommand<SideBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }

        public MainWindowViewModel(IContainerProvider provider, IRegionManager regionManager, IDialogHostService service)
        {
            UserName = AppSession.Name;
            SideBars = new ObservableCollection<SideBar>();
            journal = new RegionNavigationJournal();
            this.provider = provider;
            this.regionManager = regionManager;
            this.service = service;
            NavigateCommand = new DelegateCommand<SideBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null & journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null & journal.CanGoForward)
                    journal.GoForward();
            });
            LoginOutCommand = new DelegateCommand(() =>
            {
                App.LoginOut(provider);
            });
            regionManager.RegisterViewWithRegion(PrismManager.MainViewRegionName, typeof(IndexView));
        }

        private void Navigate(SideBar obj)
        {
            if (!(obj == null || string.IsNullOrWhiteSpace(obj.NameSpace)))
                regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, callback =>
                {
                    journal = callback.Context.NavigationService.Journal;
                });
        }

        private void CreateSideBars()
        {
            //这里的Icon是MaterialDesign框架中的IconPackIcon的kind 可以通过binding的方式
            SideBars.Add(new SideBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            SideBars.Add(new SideBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            SideBars.Add(new SideBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            SideBars.Add(new SideBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

        /// <summary>
        /// 配置首页初始化参数
        /// </summary>
        public void Configuration()
        {
            UserName = AppSession.Name;
            CreateSideBars();
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }


    }
}
