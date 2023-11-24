using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    /// <summary>
    /// 数据传输实体基类
    /// </summary>
    public class BaseDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        //public DateTime CreateTime { get; set; }
        /// <summary>
        /// 属性更改事件处理程序
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// 实现通知更新
        /// </summary>
        /// <param name="propertyName">要实现通知更新的属性名</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
