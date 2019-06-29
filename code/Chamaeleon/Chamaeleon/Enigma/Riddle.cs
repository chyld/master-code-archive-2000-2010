using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Chyld.Chamaeleon.Enigma
{
    public static class Riddle
    {
        public static String GetKey(this String s)
        {
            return s.Encrypt();
        }

        public static String GetUsr(this String s)
        {
            String cipher = s.Encrypt();
            StringBuilder user = new StringBuilder();

            foreach (var c in cipher)
                user.Append(c.MakeLatin(false));

            return user.ToString();
        }

        public static String GetPwd(this String s)
        {
            return s.Encrypt();
        }

        private static String Encrypt(this String s)
        {
            s = s.Trim();
            s = s.ToLower();

            if (String.IsNullOrEmpty(s)) return String.Empty;

            String palindrome = Reverse(s) + s;

            String md5 = GetMD5(palindrome);
            String sha = GetSHA512(md5);
            String pwd = sha.Substring(s.Length, 16);

            return pwd;
        }

        private static Char MakeLatin(this Char c, Boolean isUpper)
        {
            Int32 start_character = isUpper ? 65 : 97;
            Int32 number_of_letter = 26;
            Int32 first_char_value = Convert.ToInt32(c);
            Int32 char_offset = first_char_value % number_of_letter;

            return Convert.ToChar(start_character + char_offset);
        }

        private static Char MakeNumber(this Char c)
        {
            Int32 start_character = 48;
            Int32 number_of_letter = 10;
            Int32 first_char_value = Convert.ToInt32(c);
            Int32 char_offset = first_char_value % number_of_letter;

            return Convert.ToChar(start_character + char_offset);
        }

        private static String Reverse(String str)
        {
            Int32 len = str.Length;
            char[] arr = new char[len];

            for (Int32 i = 0; i < len; i++)
            {
                arr[i] = str[len - 1 - i];
            }

            return new String(arr);
        }

        private static String GetMD5(String strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            MD5 md5 = new MD5CryptoServiceProvider();
            String strHex = "";

            HashValue = md5.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        private static String GetSHA1(String strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            SHA1Managed SHhash = new SHA1Managed();
            String strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        private static String GetSHA256(String strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            SHA256Managed SHhash = new SHA256Managed();
            String strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        private static String GetSHA384(String strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            SHA384Managed SHhash = new SHA384Managed();
            String strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        private static String GetSHA512(String strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            SHA512Managed SHhash = new SHA512Managed();
            String strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }
    }
}
