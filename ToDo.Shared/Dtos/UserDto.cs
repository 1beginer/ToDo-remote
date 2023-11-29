using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string account;
        private string? name;
        private string password;

        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }


    }
}
