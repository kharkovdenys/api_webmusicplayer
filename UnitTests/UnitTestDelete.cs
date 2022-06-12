namespace TestProject
{
    public class UnitTestDelete
    {
        private readonly string UrlApi = "https://databaseandapi.azurewebsites.net";
        private readonly HttpClient httpClient = new();
        [Fact]
        public async Task DeleteTest1()
        {
            var myData = new
            {
                UserName = "test",
                Password = "my_password"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task DeleteTest2()
        {
            var myData = new
            {
                UserName = "test",
                Password = "********"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task DeleteTest3()
        {
            var myData = new
            {
                UserName = "",
                Password = ""
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task DeleteTest4()
        {
            var myData = new
            {
                UserName = "1",
                Password = "1"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
