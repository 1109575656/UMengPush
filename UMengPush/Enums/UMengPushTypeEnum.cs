using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.Enums
{
    /// <summary>
    ///  友盟推送类型
    /// </summary>
    public enum UMengPushTypeEnum
    {   
        /// <summary>
        /// 单播
        /// </summary>
        unicast = 1,
        /// <summary>
        /// 列播(要求不超过500个device_token)
        /// </summary>
        listcast = 2,
        /// <summary>
        /// 文件播(多个device_token可通过文件形式批量发送）
        /// </summary>
        filecast = 3,
        /// <summary>
        /// 广播
        /// </summary>
        broadcast = 4,
        /// <summary>
        /// 组播 (按照filter条件筛选特定用户群, 具体请参照filter参数) 
        /// </summary>
        groupcast = 5,
        /// <summary>
        /// customizedcast(通过开发者自有的alias进行推送),包括以下两种case: 
        /// - alias: 对单个或者多个alias进行推送
        /// - file_id: 将alias存放到文件后，根据file_id来推送
        /// </summary>
        customizedcast = 6
    }
}
