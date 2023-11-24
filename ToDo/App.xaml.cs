
using Prism.Ioc;
using System.Windows;
using ToDo.ViewModels;
using ToDo.Views;

namespace ToDo
{

    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>("IndexView");
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>("MemoView");
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>("SettingsView");
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>("ToDoView");
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>("SkinView");
            containerRegistry.RegisterForNavigation<SystemSettingView, SystemSettingViewModel>("SystemSettingView");
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>("AboutView");
        }
    }
}