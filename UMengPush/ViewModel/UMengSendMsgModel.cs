using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Enums;

namespace UMengPush.ViewModel
{
    public class UMengSendMsgModel
    {
        public UMengSendMsgModel()
        {
            Description = "小壹在线";
            AfterOpen = UMengPushNotiEnum.go_app;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 通知文字描述
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 发送消息描述，建议填写
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 设备ID || 用户ID
        /// </summary>
        public List<string> DeviceTokens { get; set; }

        public UMengPushNotiEnum AfterOpen { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_activity"时，必填。通知栏点击后打开的Activity
        /// </summary>
        public string Activity { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_url"时，必填。
        /// 通知栏点击后跳转的URL，要求以http或者https开头  
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 可选 display_type=message, 或者display_type=notification且 "after_open"为"go_custom"时，
        /// 该字段必填。用户自定义内容, 可以为字符串或者JSON格式。
        /// </summary>
        public string Custom { get; set; }
    }
}
