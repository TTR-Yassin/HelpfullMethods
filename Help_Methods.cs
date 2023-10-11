using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OdemeServiceReference;

namespace Aucation.Classess
{
    public static  class Help_Methods
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetZaman(int y, int m, int w, int d)
        {
            string Zaman = "";
            if (y > 0) Zaman += " " + y + " Yıl";
            if (m > 0) Zaman += " " + m + " Ay";
            if (w > 0) Zaman += " " + w + " Hafta";
            if (d > 0) Zaman += " " + d + " Gün";

            return Zaman;
        }

        public static string FixNum(int num)
        {
            return num < 10 && num > -1 ? "0" + num : num.ToString();
        }

        public static List<string> GetIcons()
        {
            List<string> icons = new List<string>();
            icons.Add("iconname.svg");
            icons.Add("iconname.svg");
	    icons.Add("iconname.svg");
	    icons.Add("iconname.svg");
            return icons;
        }

        private const string keyString = "E546C8DF278CD6981069B522E695D4A5";
        //private const string secretkey = "catch me if you can";
        public  static string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public static string EncryptString(string text,string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public static string Encrypt(string textToEncrypt, string publickey)
        {
            try
            {
                string ToReturn = "";
                publickey = publickey.Substring(0, 6);
                string secretkey = publickey + "in";
                publickey += "on";
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string Decrypt(string textToDecrypt, string publickey)
        {
            try
            {
                string ToReturn = "";
                publickey = publickey.Substring(0, 6);
                string privatekey = publickey + "in";
                publickey += "on";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
        public static  List<string> Get_String_List(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return new List<string>();
            string dec_hash = DecryptString(hash);
            List<string> String_List = dec_hash.Split(',').ToList();
            return String_List;

        }

        public static string Set_String_List(List<string> String_List)
        {
            if (String_List == null || String_List.Count == 0)
                return "";
            string hash = string.Join(',', String_List);
            hash = EncryptString(hash);
            return hash;
        }

        public  static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public  static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }


        public static string GetPublicIPAddress()
        {
            try
            {
                String address = "";
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (System.IO.StreamReader stream = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    address = stream.ReadToEnd();
                }

                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);

                return address;
            }
            catch 
            {
                return "1.1.1.1";
            }
            
        }
        public static string  GetKode (long num)
        {
            double temp = num;
            int index = (int)'A';
            while (temp >= 10000000)
            {
                index++;
                temp -= 9999999;
            }
            string res = "AUC" + DateTime.Now.ToString("yy") + (char)index;
            int lastIndex = 6 - (int)Math.Log10(temp);
            while (lastIndex > 0)
            {
                res += "0";
                lastIndex--;
            }
            res += temp;
            return res;
        }



		public static string GetIconText(string str)
		{
			Console.WriteLine(str);
			str = str.Substring(str.IndexOf("class=\"icon-txt\">") + 17);
			Console.WriteLine(str);
			str = str.Substring(0, str.IndexOf("</div>"));
			if (str.Contains(' '))
			{
				str = str.Substring(0, str.IndexOf(" "));
				str += "....";
			}
			Console.WriteLine(str);
			return str;
		}

		public static string GeTWorkingUsername(string str)
		{
            str = str.ToLower();
            str = str.Replace(" ", "_");
			str = str.Replace(".", "_");
			str = str.Replace(",", "_");
			str = str.Replace("-", "_");
			str = str.Replace("ı", "i");
			str = str.Replace("ğ", "g");
			str = str.Replace("ü", "u");
			str = str.Replace("ö", "o");
			str = str.Replace("ç", "c");
			str = str.Replace("ş", "s");

            return str;
		}

		public static string GetIconColor(string str)
        {
            Console.WriteLine(str);
            str = str.Substring(str.IndexOf(" fill=\"") + 7);
            Console.WriteLine(str);
            str = str.Substring(0, str.IndexOf("\" stroke="));
            if (str == "") return "White";
            else if (str == "#f00") return "Red";
            else if (str == "#e9e924") return "Yellow";
            else if (str == "#249225") return "Green";
            else if (str == "#69d7d7") return "Cyan";
            else if (str == "#009dff") return "Blue";
            else if (str == "#ff80ff") return "Magenta";
            else if (str == "#fff") return "White";
            else return "White";
        }

        public static DateTime AddWorkingDay(int daysToAdd)
        {
            DateTime date = DateTime.Now;
            while (daysToAdd > 0)
            {
                date = date.AddDays(1);

                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    daysToAdd -= 1;
                }
            }
            return date;
        }

    }
}
