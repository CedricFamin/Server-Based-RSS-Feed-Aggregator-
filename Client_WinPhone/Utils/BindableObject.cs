using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Common.Utils
{
    /// <summary>
    /// A basic class for bindable object
    /// </summary>
    public class BindableObject : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        #region Fields
        private Dictionary<int, PropertyChangedEventArgs> cacheEventArgs;
        protected bool EnableRaisePropertyChanged = true;
        #endregion

        #region Ctor
        public BindableObject()
        {
            this.cacheEventArgs = new Dictionary<int, PropertyChangedEventArgs>();
        }
        #endregion /// Ctor

        #region Protected Methods
        protected PropertyChangedEventArgs GetEventArgs(string propertyName)
        {
            PropertyChangedEventArgs result;

            this.cacheEventArgs.TryGetValue(propertyName.GetHashCode(), out result);
            if (result == null)
            {
                result = new PropertyChangedEventArgs(propertyName);
                this.cacheEventArgs[propertyName.GetHashCode()] = result;
            }
            return result;
        }

        protected void RaisePropertyChange(string propertyName)
        {
            if (!EnableRaisePropertyChanged) return;
            VerifyProperty(propertyName);
            PropertyChangedEventArgs args = GetEventArgs(propertyName);
            PropertyChanged(this, args);
        }
        #endregion

        #region Private Methods
        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            type.GetHashCode();

            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new Exception(string.Format("{0} is not a public property of {1}", propertyName, type.FullName));
            }
        }
        #endregion
    }
}
