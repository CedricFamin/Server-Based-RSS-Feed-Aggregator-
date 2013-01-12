using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public class SingletonBase<T> 
        where T : new()
    {
        
        private static T instance;
        private static object syncRoot = new Object();

        private SingletonBase() 
        {
        }

        public static T Instance
        {
            get 
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
                return instance;
            }
        }
    }
}
