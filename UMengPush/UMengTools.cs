using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UMengPush.Enums;
using UMengPush.ViewModel;

namespace UMengPush
{
    public static class UMengTools
    {
        private readonly static string AppAndroidSecret = ConfigurationManager.AppSettings["UMengAndroidAppMasterSecret"];
        private readonly static string AppIosMasterSecret = ConfigurationManager.AppSettings["UMengIOSAppMasterSecret"];
        private static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(?<=:)null(?=[,}])");

        /// <summary>
        /// 初始化类型
        /// </summary>
        /// <param name="pushModel"></param>
        /// <param name="deviceTokens"></param>
        /// <param name="equipmentType">文件播必传</param>
        public static void InitPushModelByDeviceTokens(UMengPushModel pushModel, List<string> deviceTokens, EquipmentTypeEnum equipmentType)
        {
            if (pushModel == null)
            {
                pushModel = new UMengPushModel();
            }
            if (deviceTokens == null || deviceTokens.Count == 0)
            {
                pushModel.type = UMengPushTypeEnum.broadcast.ToString();
            }
            else if (deviceTokens.Count == 1)
            {
                //单播
                pushModel.type = UMengPushTypeEnum.unicast.ToString();
                pushModel.device_tokens = deviceTokens[0];
            }
            else if (deviceTokens.Count > 1 && deviceTokens.Count < 500)
            {
                //列播
                pushModel.type = UMengPushTypeEnum.listcast.ToString();
                pushModel.device_tokens = string.Join(",", deviceTokens.ToArray());
            }
            else if (deviceTokens.Count > 500)
            {
                //文件播
                pushModel.type = UMengPushTypeEnum.filecast.ToString();
                //上传文件后返回的file_id
                var response = Upload(pushModel.appkey, deviceTokens, equipmentType);
                if (response.ret.Equals("SUCCESS") && !string.IsNullOrEmpty(response.data.file_id))
                {
                    pushModel.file_id = response.data.file_id;
                }
            }
        }
        /// <summary>
        /// 初始化类型
        /// </summary>
        /// <param name="pushModel"></param>
        /// <param name="deviceTokens"></param>
        /// <param name="equipmentType">文件播必传</param>
        public static void InitPushModelByUsers(UMengPushModel pushModel, List<string> deviceTokens, EquipmentTypeEnum equipmentType)
        {
            if (pushModel == null || deviceTokens==null || deviceTokens.Count<1)
            {
                return;
            }
            //customizedcast(通过开发者自有的alias进行推送)
            pushModel.type = UMengPushTypeEnum.customizedcast.ToString();
            pushModel.alias_type = "AliasTypeXiaoYi";
            if (deviceTokens.Count < 51)
            {
                //使用默认方式
                pushModel.alias = string.Join(",", deviceTokens.ToArray());
            }
            else
            {
                //上传文件后返回的file_id
                var response = Upload(pushModel.appkey, deviceTokens, equipmentType);
                if (response.ret.Equals("SUCCESS") && !string.IsNullOrEmpty(response.data.file_id))
                {
                    pushModel.file_id = response.data.file_id;
                }
            }
        }
        /// <summary>
        /// 调用文件上传接口，获取file_id
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="deviceTokens"></param>
        /// <param name="equipmentType"></param>
        /// <returns></returns>
        private static UMengResponseModel Upload(string appkey,List<string > deviceTokens, EquipmentTypeEnum equipmentType)
        {
            UMengUploadRequestModel model = new UMengUploadRequestModel();
            model.appkey = appkey;
            model.timestamp = GetTimeStamp();
            model.content = string.Join("\n", deviceTokens.ToArray()) + "\n";
            return UMengTools.UMengPostRequest(UMengUrlList.ApiUpload, equipmentType, model);
        }

        /// <summary>
        /// 签名并返回附带签名的url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postBody">请求参数json串</param>
        /// <param name="equipmentType">IOS/安卓</param>
        /// <returns></returns>
        public static string Sign(string url, string postBody, EquipmentTypeEnum equipmentType)
        {
            //签名规则：Sign = MD5($http_method$url$post-body$app_master_secret);
            string signStr = string.Format("POST{0}{1}{2}", url, postBody, equipmentType == EquipmentTypeEnum.Android ? AppAndroidSecret : AppIosMasterSecret);
            return string.Format("{0}?sign={1}", url, MD5(signStr));
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string MD5(string input)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0').ToLower();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static UMengResponseModel UMengPostRequest<T>(string url, EquipmentTypeEnum equipmentType, T req) where T: UMengMarker
        {
            string serialization = JsonConvert.SerializeObject(req);
            //null替换为""
            serialization = regex.Replace(serialization, "\"\"");
            //获取附带签名的url
            url = Sign(url, serialization, equipmentType);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            byte[] bs = Encoding.UTF8.GetBytes(serialization);
            request.ContentLength = bs.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }
            try
            {
                Stream stream = request.GetResponse().GetResponseStream();
                string response = null;
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    response = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<UMengResponseModel>(response);
            }
            catch (WebException e)
            {
                string message = e.Message;
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Stream myResponseStream = ((HttpWebResponse)e.Response).GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<UMengResponseModel>(retString);
                }
                return new UMengResponseModel
                {
                    ret = "FAIL",
                    data = new UMengResponseDataModel
                    {
                        error_code = "-1"
                    }
                };
            }
        }
    }
}
