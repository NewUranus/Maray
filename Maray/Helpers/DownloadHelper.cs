using Maray.Configs;

using System.Reflection;

namespace Maray.Helpers
{
    internal class DownloadHelper
    {
        /// <summary> DownloadString </summary>
        /// <param name="url"> </param>
        public async Task<string> DownloadStringAsync(string url, bool blProxy, string userAgent)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(1000 * 30);
                var result = await HttpClientHelper.GetInstance().GetAsync(url, cts.Token);
                return result;
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return "";
            }
        }

        /// <summary> 获取嵌入文本资源 </summary>
        /// <param name="res"> </param>
        /// <returns> </returns>
        public static string GetEmbedText(string res)
        {
            string result = string.Empty;

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(res))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
            return result;
        }

        public static T GetEmbedText<T>(string res)
        {
            string result = DownloadHelper.GetEmbedText(res);
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception(PathConfig.v2raySampleClient);
            }

            T v2rayConfig = JsonHelper.FromJsonString<T>(result);
            if (v2rayConfig == null)
            {
                throw new Exception(PathConfig.v2raySampleClient);
            }
            return v2rayConfig;
        }
    }
}