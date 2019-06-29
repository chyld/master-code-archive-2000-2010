using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Chyld.Apocrypha.Alias
{
    public static class Utility
    {
        public static String MakeUser(this String s, Int32 length)
        {
            StringBuilder sb = new StringBuilder();
            Int32 counter = 0;

            foreach (var c in s)
            {
                counter++;
                sb.Append(c.MakeLatin(false, counter));
                if (sb.Length == length)
                    break;
            }

            return sb.ToString();
        }

        public static String MakePass(this String s, Int32 length)
        {
            StringBuilder sb = new StringBuilder();
            Int32 counter = 0;

            foreach (var c in s)
            {
                counter++;
                sb.Append(c.MakeMixed(counter));
                if (sb.Length == length)
                    break;
            }

            return sb.ToString();
        }

        public static String Cut(this String s, Int32 length)
        {
            StringBuilder sb = new StringBuilder();
            Int32 counter = 0;

            foreach (var c in s)
            {
                counter++;
                sb.Append(c);
                if (sb.Length == length)
                    break;
            }

            return sb.ToString();
        }

        public static Char MakeLatin(this Char c, Boolean isUpper, Int32 offset)
        {
            Int32 start_character = isUpper ? 65 : 97;
            Int32 number_of_letter = 26;
            Int32 first_char_value = Convert.ToInt32(c) + (offset * 3);
            Int32 char_offset = first_char_value % number_of_letter;

            return Convert.ToChar(start_character + char_offset);
        }

        public static Char MakeMixed(this Char c, Int32 offset)
        {
            List<Char> characters = new List<Char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            Int32 number_of_letters = characters.Count;
            Int32 first_char_value = Convert.ToInt32(c) + (offset * 3);
            Int32 char_offset = first_char_value % number_of_letters;

            return characters[char_offset];
        }

        public static String Reverse(this String s)
        {
            Int32 len = s.Length;
            char[] arr = new char[len];

            for (Int32 i = 0; i < len; i++)
            {
                arr[i] = s[len - 1 - i];
            }

            return new String(arr);
        }

        public static String GetMD5(this String strPlain)
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

        public static String GetSHA1(this String strPlain)
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

        public static String GetSHA256(this String strPlain)
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

        public static String GetSHA384(this String strPlain)
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

        public static String GetSHA512(this String strPlain)
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
