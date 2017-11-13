using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.Enums
{

    /// <summary>
    ///  点击"通知"的后续行为，默认为打开app。
    /// </summary>
    public enum UMengPushNotiEnum
    {
        /// <summary>
        /// 打开应用
        /// </summary>
        go_app = 1,
        /// <summary>
        /// 跳转到URL
        /// </summary>
        go_url = 2,
        /// <summary>
        ///  打开特定的activity
        /// </summary>
        go_activity = 3,
        /// <summary>
        /// 用户自定义内容。
        /// </summary>
        go_custom = 4
    }
}
