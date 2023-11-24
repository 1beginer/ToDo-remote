using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    /// <summary>
    /// 待办事项数据传输实体
    /// </summary>
    public class ToDoDto : BaseDto
    {
     private string title;

        private string? content;

        private int status;
        //[JsonProperty]
        //private DateTime updateTime;


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

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        //public DateTime UpdateTime
        //{
        //    get { return updateTime; }
        //    set { updateTime = value; OnPropertyChanged(); }
        //}
    }
}
