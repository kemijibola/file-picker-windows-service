using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Lite.Service
{
    public  class FilePicker
    {
        static string baseUrl = GetConfigValue("APIPath");

        public static async Task<bool> Locator()
        {
            try
            {
                var filePath = GetConfigValue("ResponseLocation");
                var outputPath = GetConfigValue("OutputPath");

                //var di = new DirectoryInfo(filePath);

                //foreach (FileInfo fi in di.GetFiles())
                //{
                //    fi.Delete();
                //}

                string[] fileArray = Directory.GetFiles(filePath, "*.pgp");
                if (fileArray.Length > 0)
                {
                    
                    var decryptModel = new DecryptViewModel()
                    {
                        EncryptedFilePath = fileArray.FirstOrDefault(),
                        OutputPath = outputPath
                    };
                    using (var _client = new HttpClientManager().GetClient())
                    {
                        Logger.WriteLog($"Decryption process started at {DateTime.Now}");
                        var response = await _client.PostAsJsonAsync($"{baseUrl}indents/DecryptFile", decryptModel);
                        if (response.StatusCode.ToString().ToLower() == "ok" || response.StatusCode.ToString().ToLower() == "created")
                        {
                            try
                            {
                                var apiResponse = await response.Content.ReadAsAsync<DecryptResponseVM>(new List<JsonMediaTypeFormatter> { new JsonMediaTypeFormatter() });
                                Logger.WriteLog($"Decryption completed at {DateTime.Now}");
                                Logger.WriteLog(apiResponse.Message);
                            }
                            catch (Exception ex)
                            {
                                var apiResponse = await response.Content.ReadAsAsync<string>();
                                Logger.WriteLog(ex.Message.ToString() + apiResponse);
                            }
                        }
                        else
                        {
                            var apiResponse = await response.Content.ReadAsAsync<string>();
                            Logger.WriteLog(apiResponse);
                        }
                    }
                }
                

            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex.Message);
                Logger.WriteLog($"Process failed at {DateTime.Now}");
            }
            return true;
        }

        public static string GetConfigValue(string _key)
        {
            return ConfigurationManager.AppSettings[_key];
        }
    }
}
