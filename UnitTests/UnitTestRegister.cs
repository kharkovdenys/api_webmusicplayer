namespace TestProject
{
    public class UnitTestRegister
    {
        private readonly string UrlApi = "https://databaseandapi.azurewebsites.net";
        private readonly HttpClient httpClient=new();
        
        [Fact]
        public async Task RegisterTest1()
        {
            var myData = new
            {
                UserName = "test",
                Password = "my_password"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/register", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task RegisterTest2()
        {
            var myData = new
            {
                UserName = "test",
                Password = "*******"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/register", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task RegisterTest3()
        {
            var myData = new
            {
                UserName = "",
                Password = "my_password"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/register", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task RegisterTest4()
        {
            var myData = new
            {
                UserName = "test",
                Password = "*******"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/register", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}