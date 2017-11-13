using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UMengPush.ViewModel;

namespace UMengPush
{
    public class UMengPushHelper
    {
        /// <summary>
        /// 推送通知（设备ID）
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static bool PushNotoficationByDeviceTokens(UMengSendMsgModel reqModel)

        {
            return UMengAndroidPush.PushNotoficationByDeviceTokens(reqModel) && UMengIOSPush.PushNotoficationByDeviceTokens(reqModel);
        }

        /// <summary>
        /// 推送通知(用户ID)
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static bool PushNotoficationByUsers(UMengSendMsgModel reqModel)
        {
            return UMengAndroidPush.PushNotoficationByUsers(reqModel) && UMengIOSPush.PushNotoficationByUsers(reqModel);
        }

    }
}
