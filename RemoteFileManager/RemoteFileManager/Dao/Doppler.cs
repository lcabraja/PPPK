using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace RemoteFileManager.Dao {
    public class Doppler {
        [JsonProperty("PPPK5_BUCKET")]
        public string BucketName { get; set; }

        [JsonProperty("PPPK5_ACCESSKEY")]
        public string AccessKey { get; set; }

        [JsonProperty("PPPK5_SECRET")]
        public string Secret { get; set; }
        private static string GetDopplerToken() {
            return "dp.st.dev.BTrP16znkoZmoSXFpchJc7YnKEJlDS1zTUxuVzZVeDf";
            var token = Environment.GetEnvironmentVariable("DOPPLER_TOKEN");
            if (token == null && File.Exists("token")) {
                token = File.ReadAllText("token");
            }
            if (token == null) {
                string[] args = Environment.GetCommandLineArgs();
                foreach (var arg in args) {
                    if (arg.StartsWith("--token=") || arg.StartsWith("-t=")) {
                        token = arg.Split("=")[^1];
                    }
                }
            }
            if (token == null) {
                MessageBox.Show("Cannot launch app, missing credentials...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            return token;
        }
        public static Doppler FetchSecrets() {
            var DOPPLER_TOKEN = GetDopplerToken();
            var client = new RestClient("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(DOPPLER_TOKEN + ":"))}");
            IRestResponse response = client.Execute(request);
            var credentials = JsonConvert.DeserializeObject<Doppler>(response.Content);
            return credentials!;
        }
    }
}
