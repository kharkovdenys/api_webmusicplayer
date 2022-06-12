

namespace TestProject
{
    public class UnitTestLogin
    {
        private readonly string UrlApi = "https://databaseandapi.azurewebsites.net";
        private readonly HttpClient httpClient = new();
        [Fact]
        public async Task LoginTest1()
        {
            var myData = new
            {
                UserName = "test",
                Password = "my_password"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.GetAsync($"{UrlApi}/info");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Equal("test".ToString(),result.Substring(0,result.Length-1).Substring(1));
        }
        [Fact]
        public async Task LoginTest2()
        {
            var myData = new
            {
                UserName = "test",
                Password = "*******"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.GetAsync($"{UrlApi}/info");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Equal("test".ToString(), result.Substring(0, result.Length - 1).Substring(1));
        }
        [Fact]
        public async Task LoginTest3()
        {
            var myData = new
            {
                UserName = "test",
                Password = ""
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.GetAsync($"{UrlApi}/info");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Equal("test".ToString(), result.Substring(0, result.Length - 1).Substring(1));
        }
        [Fact]
        public async Task LoginTest4()
        {
            var myData = new
            {
                UserName = "",
                Password = "my_password"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.GetAsync($"{UrlApi}/info");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Equal("test".ToString(), result.Substring(0, result.Length - 1).Substring(1));
        }
    }
}
