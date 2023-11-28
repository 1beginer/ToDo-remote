using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ToDo.Common;
using ToDo.Extensions;

namespace ToDo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDialogHostService service;

        public MainWindow(IEventAggregator aggregator, IDialogHostService service)
        {
            InitializeComponent();

            //注册等待窗口
            aggregator.Resgiter(arg =>
            {
                this.DialogHost.IsOpen = arg.IsOpen;

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();
            });

            MinBtn.Click += (s, e) =>
                {
                    this.WindowState = WindowState.Minimized;
                };
            WinBtn.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            };

            CloseBtn.Click += async (s, e) =>
            {
                var dialogResult = await service.Question("温馨提示", "是否退出系统");
                if (dialogResult.Result != ButtonResult.OK) return;
                this.Close();
            };
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };
            LeftMenu.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.service = service;
        }
    }
}
