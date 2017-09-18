namespace CorePlugs20.OdinString
{
    public static class StringCharHelper
    {
        public static string StringFill(this string str,int fillToNum,int leftOrRight=0,string fillChar="0")
        {
            if(leftOrRight==0)
            {
                int sum = fillToNum-str.Length;
                for (int i = 0; i < sum; i++)
                {
                
                    str = fillChar+str;
                }                
            }
            return str;
        }
    }
}