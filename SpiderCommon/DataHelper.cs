using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SpiderCommon
{
    public static class DataHelper
    {
        /// <summary>
        /// 获取泛型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRowValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this object dataRowValue)
        {
            if (dataRowValue is DBNull)
            {
                return default;
            }
            if (dataRowValue == null || dataRowValue.Equals(""))
            {
                return default;
            }            
            return (T)Convert.ChangeType(dataRowValue, typeof(T));          
           
        }

        /// <summary>
        /// 根据秒数获取时间
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeByMilliSecond(long a)
        {
            if (a == 0)
            {
                return null;
            }
            return DateTime.Parse(DateTime.Now.ToString("1970-01-01 00:00:00")).AddMilliseconds(a);
        }

        /// <summary>
        /// 判断表是否为空
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool TableIsNull(this DataTable table)
        {
            return table == null || table.Rows.Count == 0;
        }

        /// <summary>
        /// table转list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        //public static List<T> GetTs<T>(this DataTable table)
        //{
        //    if (table.TableIsNull())
        //    {
        //        return null;
        //    }
        //    Type type = typeof(T);
        //    var propertys = type.GetProperties();
        //    List<T> list = new List<T>();
        //    foreach (DataRow row in table.Rows)
        //    {
        //        T t = (T)Activator.CreateInstance(type);
        //        foreach (var item in propertys)
        //        {
        //            if (row[item.Name] is DBNull)
        //            {
        //                item.SetValue(t, default);
        //            }
        //            else
        //            {
        //                item.SetValue(t, row[item.Name]);
        //            }
        //        }
        //        list.Add(t);
        //    }
        //    return list;
        //}

        /// <summary>
        /// 显示经纬度处理
        /// </summary>
        /// <param name="ajLocationValue"></param>
        /// <returns></returns>
        public static double Aj2GPSLocation(double ajLocationValue)
        {
            int du = (int)ajLocationValue;
            double temp = (ajLocationValue - du) * 100;//需要转换的部分（分）
            int fen = (int)temp;
            temp = (temp - fen) * 100;//秒
            return Math.Round(du + (double)fen / 60 + temp / 3600, 9);
        }

    }
}
