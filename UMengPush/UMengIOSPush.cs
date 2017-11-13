using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Enums;
using UMengPush.ViewModel;

namespace UMengPush
{
    public class UMengIOSPush
    {

        private readonly static string AppIosKey = ConfigurationManager.AppSettings["UMengIOSAppKey"];

        /// <summary>
        /// 初始化IOS通知实体
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        private static UMengPushModel IOSModelInit(UMengSendMsgModel reqModel)
        {
            UMengPushModel iosModel = new UMengPushModel
            {
                appkey = AppIosKey,
                description = reqModel.Description,
                payload = new UMengPushIOSContentModel()
                {
                    aps = new UMengPushIOSApsModel
                    {
                        alert = new UMengPushIOSMsgModel
                        {
                            title = reqModel.Title
                            //,body = reqModel.Text
                        }
                    },
                    ExtraData = new UMengPushIOSCustomData
                    {
                        Title = reqModel.
                        Title,Content = reqModel.Text
                    }
                }
            };
            return iosModel;
        }

        /// <summary>
        /// 通过多个设备号发送ios推送
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static bool PushNotoficationByDeviceTokens(UMengSendMsgModel reqModel)
        {
            UMengPushModel iosModel = IOSModelInit(reqModel);
            UMengTools.InitPushModelByDeviceTokens(iosModel, reqModel.DeviceTokens,EquipmentTypeEnum.IOS);
            UMengResponseModel model =UMengTools.UMengPostRequest(UMengUrlList.ApiSend, EquipmentTypeEnum.IOS, iosModel);
            return model != null && model.ret.Equals("SUCCESS");
        }

        /// <summary>
        /// 通过多个用户发送ios推送
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static bool PushNotoficationByUsers(UMengSendMsgModel reqModel)
        {
            UMengPushModel iosModel = IOSModelInit(reqModel);
            UMengTools.InitPushModelByUsers(iosModel, reqModel.DeviceTokens, EquipmentTypeEnum.IOS);
            UMengResponseModel model = UMengTools.UMengPostRequest(UMengUrlList.ApiSend, EquipmentTypeEnum.IOS, iosModel);
            //2010：与alias对应的device_tokens为空
            //在单播的时候，如果安卓的用户推送给IOS设备，则会返回2010错误
            return model != null && (model.ret.Equals("SUCCESS") || model.data.error_code== "2010");
        }

    }
}
