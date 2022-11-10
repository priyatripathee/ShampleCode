using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace BusinessEntities
{
    public static class security
    {
        public static string Encryptdata(string TextToEnc)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[TextToEnc.Length];
            string key = "G@l@xy$0ft";
            encode = Encoding.UTF8.GetBytes(key);
            encode = Encoding.UTF8.GetBytes(TextToEnc);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string TextToDnc)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(TextToDnc);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}
