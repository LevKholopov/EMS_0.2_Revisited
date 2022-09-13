﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace EMS_Library
{
    /// <summary>
    /// Class containing utility methods for the programm.
    /// </summary>
    public static class Utility
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Generates random number in specified range.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="ceiling"></param>
        /// <returns>Random number</returns>
        public static int RandomInt(int floor, int ceiling) => rnd.Next(floor, ceiling);

        /// <summary>
        /// Generates random nuber with specified number of didgits.
        /// </summary>
        /// <param name="numOfDigits"></param>
        /// <returns></returns>
        public static int RandomInt(int numOfDigits) => rnd.Next((int)(1 * Math.Pow(10, numOfDigits - 1)), (int)(1 * Math.Pow(10, numOfDigits)));

        /// <summary>
        /// Generates random boolean value.
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool() => rnd.Next(2) == 1;

        /// <summary>
        /// Generates random char.
        /// </summary>
        /// <returns></returns>
        public static char RandomChar() => (char)rnd.Next((int)'a', (int)'z' + 1);

        /// <summary>
        /// Generates random numeric char.
        /// </summary>
        /// <returns></returns>
        public static char RandomNumericChar() => (char)rnd.Next((int)'0', (int)'9' + 1);

        /// <summary>
        /// Generates random string of specified length.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandomString(int len)
        {
            string str = "";
            for (int i = 0; i < len; i++)
                str += RandomChar();
            return str;
        }

        /// <summary>
        /// Generates random string of specified length that contains only numbers.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandomNumericString(int len)
        {
            string str = "";
            for (int i = 0; i < len; i++)
                str += RandomNumericChar();
            return str;

        }

        /// <summary>
        /// Generates random DateTime object.
        /// </summary>
        /// <returns></returns>
        public static DateTime RandomDateTime() => new DateTime(RandomInt(1800, 2022), RandomInt(1, 13), RandomInt(1, 29), RandomInt(0, 24), RandomInt(0, 60), RandomInt(0, 60));
    }



    public static class Extensions
    {
        /// <summary>
        /// Converts an object to a byte array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ObjectToByteArray(this object obj)
        {
            if (!Attribute.IsDefined(obj.GetType(), typeof(SerializableAttribute)))
                throw new ArgumentException("The object must have the Serializable attribute.", "obj");
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Prefoms "bit by bit" comparison between two objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool BitByBitEquals(this object obj, object other)
        {
            byte[] first = obj.ObjectToByteArray();
            byte[] second = other.ObjectToByteArray();
            for (int i = 0; i < first.Length; i++)
                if (first[i] != second[i]) return false;
            return true;
        }

        /// <summary>
        /// Checks if enumerable collection consists of only it's default values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection, int interval = 1)
        {
            if (collection == null || collection.Count() == 0) return true;
            if (default(T) == null) return collection.Contains(default);
            byte[] def = default(T)?.ObjectToByteArray();

            for (int i = 0; i < collection.Count(); i += interval)
            {
                byte[] data = collection.ElementAt(i).ObjectToByteArray();
                for (int j = 0; j < data.Length; j++)
                    if (data[j] != def[j]) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if string contains any of substrings from provided array.
        /// </summary>
        /// <param name="arr">
        /// Array of strings.
        /// </param>
        public static bool ContainsAnyOf(this string str, string[] arr)
        {
            if (arr == null || arr.Length == 0) return false;
            foreach (string item in arr)
                if (str.Contains(item)) return true;
            return false;
        }

        /// <summary>
        /// Checks if the string is parsable into provided type.
        /// בודק האם אפשר להמיר סטרינג לטיפוס המתבקש
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Parsable(this string str, Type type)
        {
            if (type == typeof(int)) return int.TryParse(str, out _);
            else if (type == typeof(DateTime)) return DateTime.TryParse(str, out _);
            else if (type == typeof(TimeSpan)) return TimeSpan.TryParse(str, out _);
            else if (type == typeof(float) || type == typeof(double)) return double.TryParse(str, out _);
            else if (type == typeof(System.Net.Mail.MailAddress)) return System.Net.Mail.MailAddress.TryCreate(str, out _);
            return true;
        }

        /// <summary>
        /// Checks if all strings in provided array are parsable into given type.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AllParsable(this string[] arr, Type type)
        {
            foreach (string item in arr)
                if (!item.Parsable(type)) return false;
            return true;
        }

        /// <summary>
        /// Prints any enumerable collection into console.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void DebugPrint<T>(this IEnumerable<T> collection)
        {
            if (collection == null || collection.Count() == 0) return;
            foreach (T item in collection)
                if (item != null) Console.WriteLine(item.ToString());
        }

        /// <summary>
        /// Capitalizing first letter in the string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string CapitalizeFirst(this string s)
        {
            if (string.IsNullOrEmpty(s)) throw new ArgumentNullException("CapitalizeFirst: string was empty or null");
            StringBuilder sb = new StringBuilder(s);
            if (sb[0] > 'a' && sb[0] < 'z')
                sb[0] = (char)(sb[0] - 32);
            return sb.ToString();
        }

        /// <summary>
        /// Converts provided string array to contineous string
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToString(this string[] arr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in arr)
                builder.Append(str + ", ");
            return builder.ToString().Remove(builder.Length - 2);
        }

        /// <summary>
        /// Provides a subarray starting from "startIndex" and with length "lenght" 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        public static T[] SubArray<T>(this T[] arr, int startIndex, int endIndex)
        {
            T[] result = new T[endIndex-startIndex];
            for (int i = startIndex, j=0; i < endIndex; i++,j++)
                result[j] = arr[i];
            return result;
        }

        /// <summary>
        /// Search for an object in the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns>An object. If nothing found, returns default value for that type.</returns>
        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (T item in collection)
                if (predicate(item)) return item;
            return default(T);
        }

        /// <summary>
        /// Checks if a string represents Israeli internal ID number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsStateID(this string id)
        {
            if(!id.Parsable(typeof(int))) return false;
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (id == null || id == "" || id.Length > 9) return false;
            id = id.PadLeft(9, '0'); // מוסיף את הספרה 0 מצד שמאל עד לאורך 9 ספרות
            for (int i = 0; i < 9; i++)
            {
                int num = int.Parse(id.Substring(i, 1)) * id_12_digits[i];
                if (num > 9)
                    num = (num / 10) + (num % 10);
                count += num;
            }
            return count % 10 == 0;
        }

        /// <summary>
        /// Counting didgits in an integer using a while iteration.
        /// </summary>
        /// <param name="x"></param>
        public static int CountDidgits(this int x)
        {
            byte digits = 0;
            while ((x /= 10) != 0) ++digits;
            return digits;
        }

        /// <summary>
        /// Rescales image to appropriate size for FR
        /// (see EMS_Library.Config->FR & Images)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Rescale(this Bitmap image, float width, float height)
        {
            SolidBrush brush = new SolidBrush(Color.Black);

            float scale = Math.Min(width / image.Width, height / image.Height);

            Bitmap bmp = new Bitmap((int)width, (int)height);
            Graphics graph = Graphics.FromImage(bmp);

            graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int scaleWidth = (int)(image.Width * scale);
            int scaleHeight = (int)(image.Height * scale);

            graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(image, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);

            return bmp;
        }
    }
}
