using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    public class MemoDto : BaseDto
    {
        private string title;
        private string? content;
        private DateTime updateTime;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        public string? Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; OnPropertyChanged(); }
        }


    }
}
