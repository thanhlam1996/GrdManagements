using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win.UltraWinGrid;
using DevExpress.XtraGrid;
using System.Windows.Forms;

namespace CommonLib
{
    public sealed  class Constants
    {
        /// <summary>
        /// Gia tri the hien cho kieu ngay gio va co gia tri nho nhat
        /// </summary>
        public static DateTime NullDateTime = DateTime.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu decimal va co gia tri nho nhat
        /// </summary>
        public static decimal NullDecimal = decimal.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu Double va co gia tri nho nhat
        /// </summary>
        public static double NullDouble = double.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu Gia tri duy nhat GUID va co gia tri nho nhat
        /// </summary>
        public static Guid NullGuid = Guid.Empty;

        /// <summary>
        /// Gia tri the hien cho kieu Int va co gia tri nho nhat
        /// </summary>
        public static int NullInt = int.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu LongInt va co gia tri nho nhat
        /// </summary>
        public static long NullLong = long.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu Float va co gia tri nho nhat
        /// </summary>
        public static float NullFloat = float.MinValue;

        /// <summary>
        /// Gia tri the hien cho kieu String va co gia tri nho nhat
        /// </summary>
        public static string NullString = string.Empty;

        /// <summary>
        /// Gia tri lon nhat the hien cho kieu MaxDate va co gia tri nho nhat va dung cho CSDL
        /// </summary>
        public static DateTime MaxDate = new DateTime(9999, 1, 3, 23, 59, 59);

        /// <summary>
        /// Gia tri nho nhat the hien cho kieu MinDate va co gia tri nho nhat va dung cho CSDL
        /// </summary>
        public static DateTime MinDate = new DateTime(1753, 1, 1, 00, 00, 00);

        /// <summary>
        /// Gia tri the hien cho kieu StringXML  va co gia tri max dinh
        /// </summary>
        public static string NullXml = "<Root></Root>";

        static Type[] _baseTypeNumeric = new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) };
        public static Type[] BaseTypeNumeric
        {
            get
            {
                return _baseTypeNumeric;
            }
        }

        static Type[] _baseType = new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(char), typeof(string), typeof(bool), typeof(DateTime) };
        public static Type[] BaseType
        {
            get
            {
                return _baseType;

            }
        }

        static Type[] _gridType = new Type[] { typeof(UltraGrid), typeof(GridControl), typeof(DataGridView) };
        public static Type[] GridType
        {
            get
            {
                return _gridType;
            }
        }

        public static bool IsNumeric(object value)
        {
            return IsNumeric(value.GetType());
        }

        public static bool IsNumeric(Type type)
        {
            return _baseTypeNumeric.Any(t => t == type);
        }

        public static byte LenghtTypeNumeric(Type type)
        {
            byte len = 0;
            
            if (type == typeof(byte) || type == typeof(sbyte))
                len = 3;
            else if (type == typeof(short) || type == typeof(ushort))
                len = 5;
            else if (type == typeof(int) || type == typeof(uint))
                len = 10;
            else if (type == typeof(long))
                len = 19;
            else if (type == typeof(ulong))
                len = 20;
            else if (type == typeof(float))
                len = 10;
            else if (type == typeof(double))
                len = 19;
            else if (type == typeof(decimal))
                len = 29;

            return len;
        }
    }
}
