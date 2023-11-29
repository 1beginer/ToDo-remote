using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common;
using ToDo.Extensions;
using ToDo.Services;
using ToDo.Shared.Dtos;

namespace ToDo.Views.DialogViews
{
    public class LoginViewModel : BindableBase, IDialogAware
    {

        public string Title { get; set; } = "ToDo";
        private string account;
        private string password;
        private int selectedIndex = 0;
        private RegisterUserDto registerUserDto;



        private readonly ILoginService loginService;
        private readonly IEventAggregator aggregator;

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            RegisterUserDto = new RegisterUserDto();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
            this.aggregator = aggregator;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();
                    break;
                case "LoginOut":
                    LoginOut();
                    break;
                case "Go":
                    SelectedIndex = 1;
                    break;
                case "Return":
                    SelectedIndex = 0;
                    break;
                case "Resgiter":
                    Register();
                    break;
            }

        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(Password))
                return;
            var loginResult = await loginService.LoginAsync(new UserDto() { Account = Account, Password = Password });
            if (loginResult.Status)
            {
                AppSession.Name = loginResult.Result.Name;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                return;
            }

            else
                //登录失败提示..
                aggregator.SendMessage("登录失败", "LoginView");
        }
        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }
        public event Action<IDialogResult> RequestClose;

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(RegisterUserDto.Account) ||
                string.IsNullOrWhiteSpace(RegisterUserDto.Name) ||
                string.IsNullOrWhiteSpace(RegisterUserDto.Password) ||
                string.IsNullOrWhiteSpace(RegisterUserDto.Newpassword))
            {
                aggregator.SendMessage("请输入完整的注册信息！", "LoginView");
                return;
            }
            if (RegisterUserDto.Password != RegisterUserDto.Newpassword)
            {
                aggregator.SendMessage("密码不一致，请重新输入！", "LoginView");
                return;
            }
            var resgiterResult = await loginService.ResgiterAsync(new UserDto()
            {
                Account = RegisterUserDto.Account,
                Name = RegisterUserDto.Name,
                Password = RegisterUserDto.Password,
            });
            if (resgiterResult != null && resgiterResult.Status)
            {
                aggregator.SendMessage("注册成功", "LoginView");
                //注册成功，返回登录界面
                SelectedIndex = 0;
            }
            else
                aggregator.SendMessage(resgiterResult.Message, "LoginView");


        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        public RegisterUserDto RegisterUserDto
        {
            get { return registerUserDto; }
            set { registerUserDto = value; RaisePropertyChanged(); }
        }

    }
}
