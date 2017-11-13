using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.ViewModel
{
    public class UMengPushIOSContentModel
    {
        /// <summary>
        /// // 必填 严格按照APNs定义来填写
        /// </summary>
        public UMengPushIOSApsModel aps { get; set; }

        public UMengPushIOSCustomData ExtraData { get; set; }

    }

    public class UMengPushIOSCustomData
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
    }

    public class UMengPushIOSApsModel
    {
        public UMengPushIOSMsgModel alert { get; set; }
        public int badge { get; set; }
        public string sound { get; set; }
        /// <summary>
        /// 生成json后转为content-available
        /// </summary>
        public string content_available { get; set; }
        /// <summary>
        /// 可选, 注意: ios8才支持该字段。
        /// </summary>
        public string category { get; set; }
    }

    public class UMengPushIOSMsgModel
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public string body { get; set; }
    }
}
