using System;
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
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="short"/> otherwise returns zero(0)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static short ToShort(this string input)
        {
            return input.ToShort(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="short"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static short ToShort(this string input, short defaultValue)
        {
            return short.TryParse(input, out short number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{short}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static short? ToShortNullable(this string input)
        {
            return input.ToShortNullable(null);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{short}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static short? ToShortNullable(this string input, short? defaultValue)
        {
            return short.TryParse(input, out short number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="int"/> otherwise returns zero(0) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt(this string input)
        {
            return input.ToInt(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="int"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static int ToInt(this string input, int defaultValue)
        {
            return int.TryParse(input, out int number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{int}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int? ToIntNullable(this string input)
        {
            return input.ToIntNullable(null);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{int}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static int? ToIntNullable(this string input, int? defaultValue)
        {
            return int.TryParse(input, out int number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="long"/> otherwise returns zero(0) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToLong(this string input)
        {
            return input.ToLong(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="long"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static long ToLong(this string input, long defaultValue)
        {
            return long.TryParse(input, out long number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{long}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long? ToLongNullable(this string input)
        {
            return input.ToLongNullable(null);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{long}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static long? ToLongNullable(this string input, long? defaultValue)
        {
            return long.TryParse(input, out long number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="double"/> otherwise returns zero(0) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(this string input)
        {
            return input.ToDouble(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="double"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static double ToDouble(this string input, double defaultValue)
        {
            return double.TryParse(input, out double number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{double}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double? ToDoubleNullable(this string input)
        {
            return input.ToDoubleNullable(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{double}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static double? ToDoubleNullable(this string input, double? defaultValue)
        {
            return double.TryParse(input, out double number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="decimal"/> otherwise returns zero(0) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string input)
        {
            return input.ToDecimal(decimal.Zero);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="decimal"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string input, decimal defaultValue)
        {
            return decimal.TryParse(input, out decimal number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{decimal}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(this string input)
        {
            return input.ToDecimalNullable(null);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{decimal}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(this string input, decimal? defaultValue)
        {
            return decimal.TryParse(input, out decimal number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="float"/> otherwise returns zero(0) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float ToFloat(this string input)
        {
            return input.ToFloat(0);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="float"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static float ToFloat(this string input, float defaultValue)
        {
            return float.TryParse(input, out float number) ? number : defaultValue;
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{float}"/> otherwise returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float? ToFloatNullable(this string input)
        {
            return input.ToFloatNullable(null);
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> value to <see cref="Nullable{float}"/> otherwise returns the <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue">The default value if parse fails</param>
        /// <returns></returns>
        public static float? ToFloatNullable(this string input, float? defaultValue)
        {
            return float.TryParse(input, out float number) ? number : defaultValue;
        }
        /// <summary>
        /// Converts the following string values to <see cref="bool"/> whether the string value equals one of these:
        /// 0, 1, false, true, f, t, no, yes, no, yes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException("Value is null or whitespace value.");
            var val = value.ToLower().Trim();
            if (val.Equals("0"))
                return false;
            if (val.Equals("1"))
                return true;
            if (val.Equals("false"))
                return false;
            if (val.Equals("true"))
                return true;
            if (val.Equals("f"))
                return false;
            if (val.Equals("t"))
                return true;
            if (val.Equals("no"))
                return false;
            if (val.Equals("yes"))
                return true;
            if (val.Equals("n"))
                return false;
            if (val.Equals("y"))
                return true;
            throw new ArgumentException("Value is not a boolean value.");
        }
        /// <summary>
        /// Converts the following string values to <see cref="bool"/> whether the string value equals one of these:
        /// 0, 1, false, true, f, t, no, yes, no, yes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool? ToBooleanNullable(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;
            var val = value.ToLower().Trim();
            if (val.Equals("0"))
                return false;
            if (val.Equals("1"))
                return true;
            if (val.Equals("false"))
                return false;
            if (val.Equals("true"))
                return true;
            if (val.Equals("f"))
                return false;
            if (val.Equals("t"))
                return true;
            if (val.Equals("no"))
                return false;
            if (val.Equals("yes"))
                return true;
            if (val.Equals("n"))
                return false;
            if (val.Equals("y"))
                return true;
            throw new ArgumentException("Value is not a boolean value.");
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> to <seealso cref="DateTime"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string input)
        {
            return DateTime.TryParse(input, out DateTime dateTime) ? dateTime : new DateTime();
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> to <seealso cref="DateTime"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(this string input)
        {
            return DateTime.TryParse(input, out DateTime dateTime) ? dateTime : new DateTime?();
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> to <seealso cref="TimeSpan"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string input)
        {
            return TimeSpan.TryParse(input, out TimeSpan timeSpan) ? timeSpan : new TimeSpan();
        }
        /// <summary>
        /// Tries to parse a <see cref="string"/> to <seealso cref="TimeSpan"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TimeSpan? ToTimeSpanNullable(this string input)
        {
            return TimeSpan.TryParse(input, out TimeSpan timeSpan) ? timeSpan : new TimeSpan?();
        }
        /// <summary>
        /// Tries to parse a <typeparamref name="T"/> to <seealso cref="Enum"/> of <typeparamref name="T"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string input, bool ignoreCase)
        {
            if (!input.IsNullOrEmpty())
            {
                return (T)Enum.Parse(typeof(T), input, ignoreCase);
            }
            return default;
        }
        /// <summary>
        /// Check whether the value of the <see cref="string"/> matches regular expression pattern 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern">The regular expression pattern
        /// </param>
        /// <returns></returns>
        public static bool Match(this string input, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// Formats the <see cref="string"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatString(this string input, params object[] args)
        {
            return string.Format(input, args);
        }
        /// <summary>
        /// Removes all white spaces from the <see cref="string"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string input)
        {
            return input.Replace(" ", string.Empty);
        }
        /// <summary>
        /// Checks wether the <see cref="string"/> value is parseable to a <see cref="DateTime"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(this string input)
        {
            return DateTime.TryParse(input, out DateTime dt);
        }
        /// <summary>
        /// Converts to upper case the first char of the <see cref="string"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUpperFirstChar(this string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }
        /// <summary>
        /// Converts to lower case the first char of the <see cref="string"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerFirstChar(this string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }
        /// <summary>
        /// Converts to upper case every first char of a word of the <see cref="string"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUpperFirstWordChar(this string value)
        {
            return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
        }
        /// <summary>
        /// Converts to lower case every first char of a word of the <see cref="string"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerFirstWordChar(this string value)
        {
            return Regex.Replace(value, "([A-Z])([a-z])", "$1 $2");
        }
        static readonly char[] padding = { '=' };
        /// <summary>
        /// Performs a url safe enconding
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string UrlSafeEncode(this string base64String)
        {
            if (!string.IsNullOrWhiteSpace(base64String))
                return base64String.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
            else
                return base64String;
        }
        /// <summary>
        /// Performs a url safe decoding
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Encrypts data using the supplied <paramref name="encryptionkey"/>
        /// </summary>
        /// <param name="textData"></param>
        /// <param name="encryptionkey"></param>
        /// <returns></returns>
        public static string Encrypt(this string textData, string encryptionkey)
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
        /// <summary>
        /// Decrypts data using the supplied <paramref name="encryptionkey"/>
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="encryptionkey"></param>
        /// <returns></returns>
        public static string Decrypt(this string encryptedText, string encryptionkey)
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
        /// <summary>
        /// Converts a query <see cref="string"/> to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static T FromQueryString<T>(this string queryString) where T : class, new()
        {
            var dictionary = HttpUtility.ParseQueryString(queryString).ToDictionary();
            return dictionary.ToType<T>();
        }
        /// <summary>
        /// Generates a new unique numeric identifier using the date and time with a special unique arrangement.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generates a unique file name.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GenerateUniqueFileName(this string FileName)
        {
            var fileExtension = Path.GetExtension(FileName);
            var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
            return $"{uniqueFileName}{fileExtension}";
        }
        /// <summary>
        /// Checks if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Checks if the value parameter is null or an empty string (""); otherwise, false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}

