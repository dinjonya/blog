using System;

namespace CorePlugs20.OdinObjects
{
    public static class OdinObject
    {
        public static int ToInt(this Object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static Int16 ToInt16(this Object obj)
        {
            return Convert.ToInt16(obj);
        }

        public static Int64 ToInt64(this Object obj)
        {
            return Convert.ToInt64(obj);
        }
    }
}