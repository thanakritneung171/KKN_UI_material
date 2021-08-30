using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KKN.Dao.Extensions
{
    public static class SqlDataReaderExtension
    {
        public static DateTime GetDateTime(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToDateTime(val);
        }

        public static DateTime? GetDateTimeNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToDateTime(val);
            }
            return null;
        }

        public static decimal GetDecimal(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDecimal(colIndex);
            }
            return default(int);
        }

        public static decimal GetDecimal(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToDecimal(val);
        }

        public static decimal? GetDecimalNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToDecimal(val);
            }
            return null;
        }

        public static decimal? GetDecimalNullable(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDecimal(colIndex);
            }
            return null;
        }

        public static string GetString(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToString(val);
        }

        public static string GetStringNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return val.ToString();
            }
            return null;
        }

        public static int GetInt(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);
            }
            return default(int);
        }

        public static int GetInt(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToInt32(val);
        }

        public static int? GetIntNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToInt32(val);
            }
            return null;
        }

        public static int? GetIntNullable(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);
            }
            return null;
        }

        public static short? GetShortNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToInt16(val);
            }
            return null;
        }

        public static short? GetShortNullable(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt16(colIndex);
            }
            return null;
        }


        public static short GetShort(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt16(colIndex);
            }
            return default(short);
        }

        public static short GetShort(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToInt16(val);
        }

        public static bool GetBool(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToBoolean(val);
        }

        public static bool? GetBoolNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToBoolean(val);
            }
            return null;
        }

        public static byte? GetByteNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToByte(val);
            }
            return null;
        }

        public static double GetDouble(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToDouble(val);
        }

        public static double? GetDoubleNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return Convert.ToDouble(val);
            }
            return null;
        }

        public static float GetFloat(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return (float)Convert.ToDecimal(val);
        }

        public static float? GetFloatNullable(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            if (val != DBNull.Value)
            {
                return (float)Convert.ToDecimal(val);
            }
            return null;
        }

        public static byte[] GetByteArrayNullable(this SqlDataReader reader, string colName)
        {
            byte[] result = new byte[0];
            if (!Convert.IsDBNull(reader[colName]))
            {
                result = (byte[])reader[colName];
            }
            return result;
        }

        public static long GetLong(this SqlDataReader reader, string colName)
        {
            object val = reader[colName];
            return Convert.ToInt64(val);
        }
    }
}
