using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    public class RegisterUserDto : UserDto
    {
        private string newpassword;

        public string Newpassword
        {
            get { return newpassword; }
            set { newpassword = value; OnPropertyChanged(); }
        }

    }
}
