using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.ViewModel
{
    public class UMengResponseModel
    {
        /// <summary>
        /// 返回结果，"SUCCESS"或者"FAIL
        /// </summary>
        public string ret { get; set; }

        public UMengResponseDataModel data { get; set; }
    }

    public class UMengResponseDataModel
    {
        //当"ret"为"SUCCESS"时,包含如下参数:
        /// <summary>
        /// 当type为unicast、listcast或者customizedcast且alias不为空时:
        /// </summary>
        public string msg_id { get; set; }
        /// <summary>
        /// 当type为于broadcast、groupcast、filecast、customizedcast
        /// 且file_id不为空的情况(任务)
        /// </summary>
        public string task_id { get; set; }

        /// <summary>
        /// 上传文件成功返回file_id
        /// </summary>
        public string file_id { get; set; }

        //当"ret"为"FAIL"时,包含如下参数:
        public string error_code { get; set; }
    }
}
