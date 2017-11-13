using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.Enums
{
    public class UMengUrlList
    {
        /// <summary>
        /// 消息发送
        /// </summary>
        public static string ApiSend { get; set; }
        /// <summary>
        /// 任务状态查询
        /// </summary>
        public static string ApiStatus { get; set; }
        /// <summary>
        /// 任务取消
        /// </summary>
        public static string ApiCancel { get; set; }
        /// <summary>
        /// 文件上传接口
        /// </summary>
        public static string ApiUpload { get; set; }

        static UMengUrlList()
        {
            ApiSend = "https://msgapi.umeng.com/api/send";
            ApiStatus = "https://msgapi.umeng.com/api/status";
            ApiCancel = " https://msgapi.umeng.com/api/cancel";
            ApiUpload = "https://msgapi.umeng.com/upload";
        }
    }
}
