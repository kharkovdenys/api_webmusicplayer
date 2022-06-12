namespace TestProject
{
    public class UnitTestHistory
    {
        private readonly string UrlApi = "https://databaseandapi.azurewebsites.net";
        private readonly HttpClient httpClient = new();
        [Fact]
        public async Task AddTest1()
        {
            var myData = new
            {
                UserName = "Admin",
                Password = "rootroot"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            var myDatanew = new
            {
                IdVideo = "test1"
            };
            jsonData = JsonConvert.SerializeObject(myDatanew);
            var contentnew = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.PostAsync($"{UrlApi}/musics", contentnew);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response = await httpClient.GetAsync($"{UrlApi}/musics");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Contains("test1", result);
        }
        [Fact]
        public async Task DeleteTest1()
        {
            var myData = new
            {
                UserName = "Admin",
                Password = "rootroot"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            var myDatanew = new
            {
                IdVideo = "test1"
            };
            jsonData = JsonConvert.SerializeObject(myDatanew);
            var contentnew = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new System.Uri($"{UrlApi}/musics"),
                Content = contentnew
            };
            response = await httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            response = await httpClient.GetAsync($"{UrlApi}/musics");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("test1", result);
        }
    }
}
