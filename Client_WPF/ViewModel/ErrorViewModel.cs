using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.RSSService;
using Common.DataModel;
using Client_WPF.View;

namespace Client_WPF.ViewModel
{
    class ErrorViewModel : BindableObject
    {
        public WebResult.ErrorCodeList Error { get; private set; }
        public string Message { get; private set; }

        public ErrorViewModel()
        {
            Error = WebResult.ErrorCodeList.SUCCESS;
            Message = "";

            ErrorDataModel.Instance.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Instance_PropertyChanged);
            Error = ErrorDataModel.Instance.Error;
            Message = ErrorDataModel.Instance.ErrorText;
        }

        void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Error = ErrorDataModel.Instance.Error;
            Message = ErrorDataModel.Instance.ErrorText;
            new ErrorModal()
            {
                DataContext = this
            }.ShowDialog();
        }
    }
}
