using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        /// <summary>
        /// 获取调用视图的链接
        /// </summary>
        /// <param name="request">调用视图的链接</param>
        /// <returns></returns>
        public static string PathAndQuery(this HttpRequest request)
        {
            //request:Path:获取或设置请求路径
            //request.QueryString:获取或设置用于在Request.query中创建查询集合的原始查询字符串。
            //HasValue 如果查询字符串不为空，则为True
            return request.QueryString.HasValue
                    ? $"{request.Path}{request.QueryString}"
                    : request.Path.ToString();
        }
    }
}
