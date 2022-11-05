using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Extensions
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// Convert decimal to price VND format
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string ToVND(this decimal price)
        {
            return $"{price:#,##0.00} đ";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
