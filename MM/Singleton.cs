using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance;

        public static T GetInstance()
        {
            lock (typeof(T))
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
