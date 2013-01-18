using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;
using Common.RSSService;
using System.ComponentModel;

namespace Common.DataModel
{
    public class ErrorDataModel : BindableObject
    {
        #region Singleton
        static private ErrorDataModel instance = new ErrorDataModel();
        static public ErrorDataModel Instance { get {return instance; } }
        #endregion

        public WebResult.ErrorCodeList Error { get; private set; }
        public string ErrorText { get; private set; }

        private ErrorDataModel()
        {
            Error = WebResult.ErrorCodeList.SUCCESS;
            ErrorText = null;
        }

        public bool EvalResponse(AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
                return true;
            Error = WebResult.ErrorCodeList.INTERNAL_ERROR;
            ErrorText = e.Error.Message;
            RaisePropertyChange("Error");
            return false;
        }

        public bool EvalWebResult(Common.RSSService.WebResult r)
        {
            if (r.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                return true;
            Error = r.ErrorCode;
            ErrorText = "WebService Error";
            RaisePropertyChange("Error");
            return false;
        }

        public bool EvalWebResult(Common.FeedService.WebResult r)
        {
            if (r.ErrorCode == Common.FeedService.WebResult.ErrorCodeList.SUCCESS)
                return true;
            Error = (WebResult.ErrorCodeList)r.ErrorCode;
            ErrorText = "WebService Error";
            RaisePropertyChange("Error");
            return false;
        }
    }
}
