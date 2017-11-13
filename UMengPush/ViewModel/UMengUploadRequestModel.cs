using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.ViewModel
{
    public class UMengUploadRequestModel: UMengMarker
    {
        public string appkey { get; set; }

        public string timestamp { get; set; }

        public string content { get; set; }

    }
}
