using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OneLine.Extensions
{
    public static class StringExtensions
    {
        public static short ToShort(this string input)
        {
            return input.ToShort(0);
        }

        public static short ToShort(this string input, short defaultValue)
        {
            return short.TryParse(input, out short number) ? number : defaultValue;
        }

        public static short? ToShortNullable(this string input)
        {
            return input.ToShortNullable(null);
        }

        public static short? ToShortNullable(this string input, short? defaultValue)
        {
            return short.TryParse(input, out short number) ? number : defaultValue;
        }

        public static int ToInt(this string input)
        {
            return input.ToInt(0);
        }

        public static int ToInt(this string input, int defaultValue)
        {
            return int.TryParse(input, out int number) ? number : defaultValue;
        }

        public static int? ToIntNullable(this string input)
        {
            return input.ToIntNullable(null);
        }

        public static int? ToIntNullable(this string input, int? defaultValue)
        {
            return int.TryParse(input, out int number) ? number : defaultValue;
        }

        public static long ToLong(this string input)
        {
            return input.ToLong(0);
        }

        public static long ToLong(this string input, long defaultValue)
        {
            return long.TryParse(input, out long number) ? number : defaultValue;
        }

        public static long? ToLongNullable(this string input)
        {
            return input.ToLongNullable(null);
        }

        public static long? ToLongNullable(this string input, long? defaultValue)
        {
            return long.TryParse(input, out long number) ? number : defaultValue;
        }

        public static double ToDouble(this string input)
        {
            return input.ToDouble(0);
        }

        public static double ToDouble(this string input, double defaultValue)
        {
            return double.TryParse(input, out double number) ? number : defaultValue;
        }

        public static double? ToDoubleNullable(this string input)
        {
            return input.ToDoubleNullable(0);
        }

        public static double? ToDoubleNullable(this string input, double? defaultValue)
        {
            return double.TryParse(input, out double number) ? number : defaultValue;
        }

        public static decimal ToDecimal(this string input)
        {
            return input.ToDecimal(decimal.Zero);
        }

        public static decimal ToDecimal(this string input, decimal defaultValue)
        {
            return decimal.TryParse(input, out decimal number) ? number : defaultValue;
        }

        public static decimal? ToDecimalNullable(this string input)
        {
            return input.ToDecimalNullable(null);
        }

        public static decimal? ToDecimalNullable(this string input, decimal? defaultValue)
        {
            return decimal.TryParse(input, out decimal number) ? number : defaultValue;
        }

        public static bool ToBoolean(this string value)
        {
            var val = value.ToLower().Trim();
            if (val == "0")
                return false;
            if (val == "1")
                return true;
            if (val == "false")
                return false;
            if (val == "true")
                return true;
            if (val == "f")
                return false;
            if (val == "t")
                return true;
            if (val == "no")
                return false;
            if (val == "yes")
                return true;
            if (val == "n")
                return false;
            if (val == "y")
                return true;
            throw new ArgumentException("Value is not a boolean value.");
        }

        public static bool? ToBooleanNullable(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var val = value.ToLower().Trim();
            if (val == "0")
                return false;
            if (val == "1")
                return true;
            if (val == "false")
                return false;
            if (val == "true")
                return true;
            if (val == "f")
                return false;
            if (val == "t")
                return true;
            if (val == "no")
                return false;
            if (val == "yes")
                return true;
            if (val == "n")
                return false;
            if (val == "y")
                return true;
            throw new ArgumentException("Value is not a boolean value.");
        }

        public static DateTime ToDateTime(this string input)
        {
            return DateTime.TryParse(input, out DateTime dateTime) ? dateTime : new DateTime();
        }

        public static DateTime? ToDateTimeNullable(this string input)
        {
            return DateTime.TryParse(input, out DateTime dateTime) ? dateTime : new DateTime?();
        }

        public static TimeSpan ToTimeSpan(this string input)
        {
            return TimeSpan.TryParse(input, out TimeSpan timeSpan) ? timeSpan : new TimeSpan();
        }

        public static TimeSpan? ToTimeSpanNullable(this string input)
        {
            return TimeSpan.TryParse(input, out TimeSpan timeSpan) ? timeSpan : new TimeSpan?();
        }

        public static T ToEnum<T>(this string input, bool ignoreCase)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return (T)Enum.Parse(typeof(T), input, ignoreCase);
            }
            return default(T);
        }

        public static bool Match(this string input, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        public static string FormatString(this string input, params object[] args)
        {
            return string.Format(input, args);
        }

        public static string RemoveSpaces(this string input)
        {
            return input.Replace(" ", string.Empty);
        }

        public static bool IsDate(this string input)
        {
            return DateTime.TryParse(input, out DateTime dt);
        }

        public static T? ToNullable<T>(this string s) where T : struct
        {
            T? result = new T?();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }

        public static string ToLowerFirstChar(this string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }

        public static string ToUpperFirstChar(this string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string UppercaseLettersAfterSpace(this string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
        }

        public static string LowerrcaseLettersAfterSpace(this string value)
        {
            return Regex.Replace(value, "([A-Z])([a-z])", "$1 $2");
        }

        public static bool IsOkResponse(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return value.ToUpper().Trim() == "OK";
        }

        public static bool IsErrorResponse(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return value.ToUpper().Trim() == "ERROR";
        }
        static readonly char[] padding = { '=' };
        public static string UrlSafeEncode(this string base64String)
        {
            if (!string.IsNullOrWhiteSpace(base64String))
                return base64String.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
            else
                return base64String;
        }
        public static string UrlSafeDecode(this string urlSafeBase64String)
        {
            if (string.IsNullOrWhiteSpace(urlSafeBase64String))
            {
                return urlSafeBase64String;
            }
            else
            {
                string base64String = urlSafeBase64String.Replace('_', '/').Replace('-', '+');
                switch (urlSafeBase64String.Length % 4)
                {
                    case 2: base64String += "=="; break;
                    case 3: base64String += "="; break;
                }
                return base64String;
            }
        }
        public static string EncryptData(this string textData, string encryptionkey)
        {
            using (RijndaelManaged objrij = new RijndaelManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 0x80,
                BlockSize = 0x80
            })
            {
                byte[] passBytes = Encoding.UTF8.GetBytes(encryptionkey);
                byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;                                                   
                ICryptoTransform objtransform = objrij.CreateEncryptor();
                byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
                return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
            }
        }
        public static string DecryptData(this string encryptedText, string encryptionkey)
        {
            using (RijndaelManaged objrij = new RijndaelManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 0x80,
                BlockSize = 0x80
            })
            {
                byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);
                byte[] passBytes = Encoding.UTF8.GetBytes(encryptionkey);
                byte[] EncryptionkeyBytes = new byte[0x10];
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                return Encoding.UTF8.GetString(TextByte);
            }  
        }
        public static T FromQueryString<T>(this string queryString) where T : class, new()
        {
            var dictionary = HttpUtility.ParseQueryString(queryString).ToDictionary();
            return dictionary.ToType<T>();
        }
        public static string NewNumericIdentifier(this string value)
        {
            return DateTime.Now.TimeOfDay.Milliseconds.ToString() +
                    DateTime.Now.Date.DayOfYear.ToString() +
                    DateTime.Now.Date.Year.ToString() +
                    DateTime.Now.TimeOfDay.Hours.ToString() +
                    DateTime.Now.Date.Month.ToString() +
                    DateTime.Now.TimeOfDay.Seconds.ToString() +
                    DateTime.Now.TimeOfDay.Minutes.ToString() +
                    DateTime.Now.Date.Day.ToString();
        }
        public static string GenerateUniqueFileName(this string FileName)
        {
            var fileExtension = Path.GetExtension(FileName);
            var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
            return $"{uniqueFileName}{fileExtension}";
        }
    }
}

