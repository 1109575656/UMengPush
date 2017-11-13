using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Enums;
using UMengPush.ViewModel;

namespace UMengPush
{
    class Program
    {
        static void Main(string[] args)
        {
            //安卓点击后跳到APP，IOS需通过自定义参数进行后续操作
            UMengPushHelper.PushNotoficationByDeviceTokens(new UMengSendMsgModel
            {
                 Title="标题",
                 Text= "通知文字描述",
                 DeviceTokens=new List<string>
                 {
                    "As6KwntK8Rh8h_ttmvyPNmdE5IYKKYOihRK4icAEgysF"
                 },
                 AfterOpen = UMengPushNotiEnum.go_app
            });
        }
    }
}
