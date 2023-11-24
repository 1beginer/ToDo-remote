using ToDo.Common.Models;
using ToDo.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Views;

namespace ToDo.ViewModels
{
    public class SettingViewModel : BindableBase
    {
        private ObservableCollection<SideBar> settingBar;
        private readonly IRegionManager regionManager;
        public DelegateCommand<SideBar> NavigateCommand { get; private set; }
        public ObservableCollection<SideBar> SettingBar
        {
            get { return settingBar; }
            set { settingBar = value; }
        }

        public SettingViewModel(IRegionManager regionManager)
        {
            CreateSettingBar();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<SideBar>(Navigate);
            regionManager.RegisterViewWithRegion(PrismManager.SettingsViewRegionName, typeof(SystemSettingView));
        }

        private void Navigate(SideBar bar)
        {
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(bar.NameSpace);
        }

        private void CreateSettingBar()
        {
            SettingBar = new ObservableCollection<SideBar>() {
                new SideBar(){  Icon = "Palette", Title = "个性化", NameSpace = "SkinView" },
                new SideBar(){  Icon = "Cog", Title = "系统设置", NameSpace = "SystemSettingView"},
                new SideBar(){  Icon = "Information", Title = "关于更多", NameSpace = "AboutView"}
            };
        }
    }
}
