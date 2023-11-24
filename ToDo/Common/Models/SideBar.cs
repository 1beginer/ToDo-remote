using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDo.Common.Models
{
    /// <summary>
    /// 侧边栏实体类
    /// </summary>
    public class SideBar : BindableBase
    {

        private string icon;
        private string title;
        private string nameSpace;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        /// <summary>
        /// 菜单命名空间-可以导航到那个视图，命名控件中
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }

    }
}
