using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using CorePlugs20.CommonCore;
using CorePlugs20.OdinString;
using CorePlugs20.Models;
namespace CorePlugs20.WebApi
{
    public class ApiHelper
    {
        public static string GetStringFromRequestBody(Stream requestBody)
        {
            return new StreamReader(requestBody).ReadToEnd();
        }

        public static List<T>  JsonStringToModels<T>(string model)
        {
            return JsonConvert.DeserializeObject<List<T>>(model);
        }

        /// <summary>
        /// 异步 Get方法调用 webApi
        /// </summary>
        /// <typeparam name="T">返回的对象类型(ByteArray、Stream、String)</typeparam>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径以及参数</param>
        /// <returns>返回GET到的对象，如果响应失败抛出异常</returns>
        public static async Task<T> GetWebApiAsync<T>(string webApiUri, string webApiPath, 
                                                            Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                client.BaseAddress = new Uri(webApiUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                Task<HttpResponseMessage> httpResponseMessage = client.GetAsync(webApiPath, HttpCompletionOption.ResponseContentRead);
                return await GetResultAsync<T>(httpResponseMessage);
            }
        }

        public static  T PostValueAsync<T>(string webApiUri, string webApiPath, Object obj, 
                                                Dictionary<string, string> customHeaders = null, 
                                                string MediaType = "application/json",Encoding encoder=null)
		{
			using (HttpClient httpPostClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
				{
					httpPostClient.BaseAddress = new Uri(webApiUri);
					httpPostClient.DefaultRequestHeaders.Clear();
					httpPostClient.DefaultRequestHeaders.Accept.Clear();
					if (customHeaders != null)
					{
						foreach (KeyValuePair<string, string> customHeader in customHeaders)
						{
							httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
						}
					}
					httpPostClient.DefaultRequestHeaders.Connection.Add("keep-alive");
					string jsonContent = JsonConvert.SerializeObject(obj);
					Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsync(webApiPath, 
                                new StringContent(
                                    jsonContent,
                                    encoder==null?Encoding.UTF8:encoder,
                                    MediaType));
					string result = httpResponseMessage.Result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(result);

				}
		}


		public static async Task PostAsync(string webApiUri, string webApiPath, Object obj, 
                                                Dictionary<string, string> customHeaders = null, 
                                                string MediaType = "application/json",Encoding encoder=null)
		{
			await Task.Run(() =>
			{
				using (HttpClient httpPostClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
				{
					httpPostClient.BaseAddress = new Uri(webApiUri);
					httpPostClient.DefaultRequestHeaders.Clear();
					httpPostClient.DefaultRequestHeaders.Accept.Clear();
					if (customHeaders != null)
					{
						foreach (KeyValuePair<string, string> customHeader in customHeaders)
						{
							httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
						}
					}
					httpPostClient.DefaultRequestHeaders.Connection.Add("keep-alive");
					string jsonContent = JsonConvert.SerializeObject(obj);
                    
					Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsync(webApiPath, 
                                new StringContent(
                                    jsonContent,
                                    encoder==null?Encoding.UTF8:encoder,
                                    MediaType));
					return httpResponseMessage.Result.Content.ReadAsStringAsync();
				}
			});
		}
        
        public static async Task<string> PostDataAndFileAsync(string webApiUri, string webApiPath, Object obj, List<UploadFile_Model> files = null, Dictionary<string, string> customHeaders = null,
                                                      string MediaType = "application/x-www-form-urlencoded")
        {
            await Task.Run(() =>
            {
                using (HttpClient httpPostClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
                {
                    httpPostClient.BaseAddress = new Uri(webApiUri);
                    httpPostClient.DefaultRequestHeaders.Clear();
                    httpPostClient.DefaultRequestHeaders.Accept.Clear();
                    if (customHeaders != null)
                    {
                        foreach (KeyValuePair<string, string> customHeader in customHeaders)
                        {
                            httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                        }
                    }
                    httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    using (var multipartFormDataContent = new MultipartFormDataContent())
                    {

                        foreach (var item in obj.GetType().GetRuntimeProperties())
                        {
                            multipartFormDataContent.Add(new StringContent(item.GetValue(obj).ToString()), String.Format("\"{0}\"", item.Name));
                        }
                        if(files!=null && files.Count>0)
                        {
                            foreach (var item in files)
                            {
                                multipartFormDataContent.Add(new ByteArrayContent(item.UploadFile),item.ShowFile, item.FileName);
                            }
                        }
                        httpPostClient.DefaultRequestHeaders.Connection.Add("keep-alive");
                        Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsync(webApiPath, multipartFormDataContent);
                        return httpResponseMessage.Result.Content.ReadAsStringAsync();
                    }
                }
            });
            return null;
        }

        /// <summary>
        /// GET 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <returns>返回GET到的json字符串</returns>
        public static T GetWebApi<T>(string webApiUri, string webApiPath,
                                            Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                client.BaseAddress = new Uri(webApiUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                Task<HttpResponseMessage> httpResponseMessage = client.GetAsync(webApiPath, HttpCompletionOption.ResponseContentRead);
                return GetResult<T>(httpResponseMessage);
            }
        }

        /// <summary>
        /// POST 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <param name="obj">Post的对象参数</param>
        /// <returns>返回对应的信息</returns>
        public static void PostWebApi(string webApiUri, string webApiPath, Object obj,
                                                            Dictionary<string, string> customHeaders = null, string MediaType = "application/json",Encoding encoder=null)
        {
            
            using (HttpClient httpPostClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                httpPostClient.BaseAddress = new Uri(webApiUri);
                httpPostClient.DefaultRequestHeaders.Accept.Clear();
                httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                HttpContent content = new StringContent(JsonConvert.SerializeObject(obj),
                                    encoder==null?Encoding.UTF8:encoder,
                                    MediaType);
                Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsync(webApiPath, content);
                string result = httpResponseMessage.Result.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// POST 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <param name="obj">Post的对象参数</param>
        /// <returns>返回对应的信息</returns>
        public static T PostWebApi<T>(string webApiUri, string webApiPath, Object obj,
                                            Dictionary<string, string> customHeaders = null, string MediaType = "application/json",Encoding encoder=null) where T : class
        {
            using (HttpClient httpPostClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                httpPostClient.BaseAddress = new Uri(webApiUri);
                httpPostClient.DefaultRequestHeaders.Accept.Clear();
                httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                Task<HttpResponseMessage> httpResponseMessage= null;
                if(MediaType == "application/json")
                {
                    httpResponseMessage = httpPostClient.PostAsync(webApiPath, new StringContent(JsonConvert.SerializeObject(obj),
                                    encoder==null?Encoding.UTF8:encoder,
                                    MediaType));
                }
                else
                {
                    var lst = new List<string>();
                    foreach (var item in obj.GetType().GetRuntimeProperties())
                    {
                        lst.Add(item.Name+"="+item.GetValue(obj));
                    }
                    string param = string.Join("&",lst.ToArray());
                    httpResponseMessage = httpPostClient.PostAsync(webApiPath,new StringContent(param,Encoding.UTF8,MediaType));
                }
                return GetResult<T>(httpResponseMessage);
            }
        }

        private static T GetResult<T>(Task<HttpResponseMessage> httpResponseMessage)
        {
            HttpResponseMessage result = httpResponseMessage.Result;
            result.EnsureSuccessStatusCode();
            if (typeof(T) == typeof(byte[]))
            {
                return (T)Convert.ChangeType(result.Content.ReadAsByteArrayAsync().Result, typeof(T));
            }
            if (typeof(T) == typeof(Stream))
            {
                return (T)Convert.ChangeType(result.Content.ReadAsStreamAsync().Result, typeof(T));
            }
            if (typeof(T) == typeof(String))
            {
                return (T)Convert.ChangeType(result.Content.ReadAsStringAsync().Result, typeof(T));
            }
            else
            {
                string str = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        private static Task<T> GetResultAsync<T>(Task<HttpResponseMessage> httpResponseMessage)
        {
            HttpResponseMessage result = httpResponseMessage.Result;
            result.EnsureSuccessStatusCode();
            if (typeof(T) == typeof(byte[]))
            {
                return result.Content.ReadAsByteArrayAsync() as Task<T>;
            }
            else if (typeof(T) == typeof(Stream))
            {
                return result.Content.ReadAsStreamAsync() as Task<T>;
            }
            else
            {
                return result.Content.ReadAsStringAsync() as Task<T>;
            }
        }
    }
}