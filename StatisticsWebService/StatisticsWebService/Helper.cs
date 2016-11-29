using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Runtime.Caching;
using System.Reflection;

namespace StatisticsWebService
{
    public class Helper
    {

/// <summary>
/// Reads file to a List
/// </summary>
/// <param name="strFilePath"></param>
/// <returns></returns>
        private static List<Statistics> ConvertCSVtoList(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }

            List<Statistics> list = new List<Statistics>();
            list = ConvertDataTableTList<Statistics>(dt);

            return list;
        }
        /// <summary>
        /// Converts DataTable To List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> ConvertDataTableTList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }  
        /// <summary>
        /// Cashes given File with a given name
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="cacheName"></param>
        public static void CacheData(string strFilePath, string cacheName)
        {

            if (HttpRuntime.Cache[cacheName] == null)
            {
                HttpRuntime.Cache.Remove(cacheName);
            }
            HttpRuntime.Cache.Insert(cacheName, ConvertCSVtoList(strFilePath), null, DateTime.Now.AddMinutes(15), TimeSpan.Zero);
        }

        /// <summary>
        /// Queries List
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static List<Statistics> QueryList(List<Statistics> lst, string no)
        {
            List<Statistics> returnList = new List<Statistics>();

            returnList = lst.Where(p => p.UID == no).ToList();

            return returnList;
        }

    }
}