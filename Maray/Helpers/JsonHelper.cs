using Newtonsoft.Json;

namespace Maray.Helpers
{
    internal class JsonHelper
    {
        /// <summary> 序列化成Json </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static string ToJson(Object obj)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(obj,
                                           Newtonsoft.Json.Formatting.Indented,
                                           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        #region JsonFile

        /// <summary> 序列化成Json文件 </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static string WriteToJsonFile(string filePath, Object obj)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(obj,
                                           Newtonsoft.Json.Formatting.Indented,
                                           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                File.WriteAllText(filePath, result);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary> 序列化成Json文件 </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static T ReadFromJsonFile<T>(string filePath)
        {
            try
            {
                var temp = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<T>(temp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return result;
            }
            catch
            {
                throw;
            }
        }

        #endregion JsonFile
    }
}