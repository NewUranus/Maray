using Newtonsoft.Json;

using System.Runtime.Serialization.Formatters.Binary;

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

        #region JsonString

        /// <summary> 反序列化成对象 </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="strJson"> </param>
        /// <returns> </returns>
        public static T FromJsonString<T>(string strJson)
        {
            try
            {
                T obj = JsonConvert.DeserializeObject<T>(strJson);
                return obj;
            }
            catch
            {
                return JsonConvert.DeserializeObject<T>("");
            }
        }

        /// <summary> 序列化成Json </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static string ToJsonString(Object obj)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(obj,
                                           Formatting.Indented,
                                           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
            return result;
        }

        #endregion JsonString

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

        /// <summary> 深度拷贝 </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static T DeepCopy<T>(T obj)
        {
            try
            {
                var temp = JsonConvert.SerializeObject(obj);
                return (T)JsonConvert.DeserializeObject<T>(temp);
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return default(T);
            }
        }
    }
}