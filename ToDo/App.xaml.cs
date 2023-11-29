using DryIoc;
using ToDo.Common;
using ToDo.ViewModels;
using ToDo.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToDo.Services;
using ToDo.Services.ServiceImpl;
using ToDo.Views.DialogViews;
using ToDo.ViewModels.DialogViewModels;

namespace ToDo
{

    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        /// <summary>
        /// 注销方法
        /// </summary>
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();
            var dialog = containerProvider.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                Current.MainWindow.Show();
            });
        }
        /// <summary>
        /// Prism 框架的初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                var service = App.Current.MainWindow.DataContext as IConfigurationService;
                if (service != null)
                    service.Configuration();
                base.OnInitialized();
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5264", serviceKey: "webUrl");

            containerRegistry.RegisterScoped<IToDoService, ToDoService>();
            containerRegistry.RegisterScoped<IMemoService, MemoService>();
            containerRegistry.RegisterScoped<ILoginService, LoginService>();
            containerRegistry.RegisterScoped<IDialogHostService, DialogHostService>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>("LoginView");

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>("Main");
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>("IndexView");
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>("MemoView");
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>("SettingsView");
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>("ToDoView");
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>("SkinView");
            containerRegistry.RegisterForNavigation<SystemSettingView, SystemSettingViewModel>("SystemSettingView");
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>("AboutView");

            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>("AddMemoView");
            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>("AddToDoView");
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>("MsgView");


        }
    }
}