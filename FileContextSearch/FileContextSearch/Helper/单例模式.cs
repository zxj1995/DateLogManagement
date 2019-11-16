using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileContextSearch.Helper
{
    public class SingletonSecond
    {
        private static SingletonSecond _SingletonSecond = null;
        public static SingletonSecond _SingletonSecond2 = null;
        public static SingletonSecond _SingletonSecond3 = null;
        static SingletonSecond()
        {

            _SingletonSecond = new SingletonSecond();
        }

        public static SingletonSecond CreateInstance()
        {
            return _SingletonSecond;
        }
        public static SingletonSecond CreateInstance2()
        {
            return _SingletonSecond2;
        }
        public SingletonSecond CreateInstance3()
        {
            return _SingletonSecond2;
        }
        private string aaa = "aaa";
    }
}

