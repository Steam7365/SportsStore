using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        /// <summary>
        /// 将value转换为Json保存至Seesion[key]中
        /// </summary>
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取Session[key]的Json数据并转换为T类型
        /// </summary>
        /// <returns>Json数据</returns>
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
