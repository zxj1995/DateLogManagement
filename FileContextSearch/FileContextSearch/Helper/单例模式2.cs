using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileContextSearch.Helper
{
    public class SingletonSecond1
    {
        private static SingletonSecond1 _SingletonSecond1 = null;

        static SingletonSecond1()
        {

            _SingletonSecond1 = new SingletonSecond1();
        }

        public static SingletonSecond1 CreateInstance()
        {
            return _SingletonSecond1;
        }
    }
}
