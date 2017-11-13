using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.ViewModel
{
    //device_token 设备ID，唯一
    //友盟文档地址：http://dev.umeng.com/push/android/api-doc
    //通知：发送后会在系统通知栏收到展现，同时响铃或振动提醒用户。 
    //消息：以透传的形式传递给客户端，无显示，发送后不会在系统通知栏展现，第三方应用后需要开发者写代码才能看到。  
    public class UMengPushModel: UMengMarker
    {
        public UMengPushModel()
        {
            timestamp = GetTimeStamp();
            bool uMengEnvironment = Convert.ToInt32(ConfigurationManager.AppSettings["UMengEnvironment"]) == 0;
            production_mode = uMengEnvironment.ToString().ToLower();
        }

        /// <summary>
        /// 必填 应用唯一标识
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        ///  必填 消息发送类型,其值可以为:广播，单播，组播...
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///  可选 
        ///  设备唯一表示当type=unicast时,必填, 表示指定的单个设备
        ///  当type=listcast时,必填,要求不超过500个, 多个device_token以英文逗号间隔
        /// </summary>
        public string device_tokens { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时，必填，alias的类型,  alias_type可由开发者自定义,
        /// 开发者在SDK中调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        public string alias_type { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时, 开发者填写自己的alias。
        /// 要求不超过50个alias,多个alias以英文逗号间隔。在SDK中调用setAlias(alias, alias_type)时所设置的alias
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 可选 当type=filecast时，file内容为多条device_token, 
        /// device_token以回车符分隔
        /// 当type=customizedcast时，file内容为多条alias，
        ///alias以回车符分隔，注意同一个文件内的alias所对应 的alias_type必须和接口参数alias_type一致。
        /// 注意，使用文件播前需要先调用文件上传接口获取file_id, 具体请参照"2.4文件上传接口"
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        ///暂时不用
        ///可选终端用户筛选条件,如用户标签、地域、应用版本以及渠道等,  具体请参考附录G。
        /// </summary>
        public object filter { get; set; }

        /// <summary>
        /// 必填 消息内容(Android最大为1840B), 包含参数说明如下(JSON格式):
        /// </summary>
        public object payload { get; set; }
        /// <summary>
        /// 可选 用户自定义key-value。只对"通知" (display_type=notification)生效。
        /// 可以配合通知到达后,打开App,打开URL,打开Activity使用。
        /// key1："" key2：""
        /// </summary>
        //public Dictionary<string,string> extra { get; set; }
        /// <summary>
        ///可选 true/false 正式/测试模式。测试模式下，只会将消息发给测试设备。
        ///测试设备需要到web上添加。 Android: 测试设备属于正式设备的一个子集。
        /// </summary>
        public string production_mode { get; set; }
        /// <summary>
        /// 可选 发送消息描述，建议填写。
        /// </summary>
        public string description { get; set; }

        // "policy":   暂时不用， 可选 发送策略
        // {
        //"start_time":"xx",   // 可选 定时发送时间，若不填写表示立即发送。
        //                             定时发送时间不能小于当前时间
        //                             格式: "yyyy-MM-dd HH:mm:ss"。 
        //                             注意, start_time只对任务生效。
        //"expire_time":"xx",  // 可选 消息过期时间,其值不可小于发送时间或者
        //                             start_time(如果填写了的话), 
        //                             如果不填写此参数，默认为3天后过期。格式同start_time
        //"max_send_num": xx   // 可选 发送限速，每秒发送的最大条数。
        //                             开发者发送的消息如果有请求自己服务器的资源，可以考虑此参数。
        //"out_biz_no": "xx"     // 可选 开发者对消息的唯一标识，服务器会根据这个标识避免重复发送。
        //                             有些情况下（例如网络异常）开发者可能会重复调用API导致
        //                             消息多次下发到客户端。如果需要处理这种情况，可以考虑此参数。
        //                             注意, out_biz_no只对任务生效。
        //}


        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }

   
}
