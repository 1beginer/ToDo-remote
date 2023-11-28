using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Shared.Dtos
{
    /// <summary>
    /// 汇总
    /// </summary>
    public class SummeryDto : BaseDto
    {
        private int sum;
        private int completedCount;
        private int memoCount;
        private string completedRatio;
        private ObservableCollection<ToDoDto> todoList;
        private ObservableCollection<MemoDto> memoList;
        /// <summary>
        /// 待办事项总数
        /// </summary>
        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        /// <summary>
        /// 完成待办事项数量
        /// </summary>
        public int CompletedCount
        {
            get { return completedCount; }
            set { completedCount = value; }
        }
        /// <summary>
        /// 备忘录属性
        /// </summary>
        public int MemoCount
        {
            get { return memoCount; }
            set { memoCount = value; }
        }
        /// <summary>
        /// 待办是事项完成比例
        /// </summary>
        public string CompletedRatio
        {
            get { return completedRatio; }
            set { completedRatio = value; }
        }

        /// <summary>
        /// 待办事项列表
        /// </summary>
        public ObservableCollection<ToDoDto> ToDoList
        {
            get { return todoList; }
            set { todoList = value; }
        }

        /// <summary>
        /// 备忘录列表
        /// </summary>
        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; }
        }

    }
}
