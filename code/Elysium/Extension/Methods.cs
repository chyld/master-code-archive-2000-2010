namespace Chyld.Elysium.Extension
{
    using System;

    /* *** *** *** *** *** *** *** *** *** *** *** ***  */
    public static class Methods
    {
        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public static Int32 ToInt32(this String s)
        {
            Int32 result;

            if(Int32.TryParse(s, out result))
                return result;
            else
                throw new ArgumentException("The argument, " + s + ", to the extension method ToInt32 cannot be parsed.");
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */
        public static DateTime ToDateTime(this String s)
        {
            DateTime result;

            if(DateTime.TryParse(s, out result))
                return result;
            else
                throw new ArgumentException("The argument, " + s + ", to the extension method ToDateTime cannot be parsed.");
        }
    }
}
