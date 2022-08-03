namespace UnitTests
{
    [TestCaseOrderer("UnitTests.Orderers.PriorityOrderer", "UnitTests")]
    public class UnitTests
    {
        private readonly string UrlApi = "https://localhost:7030";
        private readonly HttpClient httpClient = new();

        [Fact, TestPriority(0)]
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
        [Fact, TestPriority(0)]
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
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid password\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(0)]
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
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid login\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(1)]
        public async Task RegisterTest4()
        {
            var myData = new
            {
                UserName = "test",
                Password = "********"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/register", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"This login is already in use\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(1)]
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
            Assert.Equal("\"test\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(1)]
        public async Task LoginTest2()
        {
            var myData = new
            {
                UserName = "test",
                Password = "********"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/login", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"This login or password is not used\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(1)]
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
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid password\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(1)]
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
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid login\"", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task AddHistoryTest()
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
            var myDatanew = new
            {
                IdVideo = "IdTest"
            };
            jsonData = JsonConvert.SerializeObject(myDatanew);
            content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            response = await httpClient.PostAsync($"{UrlApi}/musics", content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response = await httpClient.GetAsync($"{UrlApi}/musics");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.Contains("IdTest", result);
        }
        [Fact, TestPriority(2)]
        public async Task DeleteHistoryTest()
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
            var myDatanew = new
            {
                IdVideo = "IdTest"
            };
            jsonData = JsonConvert.SerializeObject(myDatanew);
            content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            JObject json = JObject.Parse(result);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(json["token"].ToString());
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{UrlApi}/musics"),
                Content = content
            };
            response = await httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            response = await httpClient.GetAsync($"{UrlApi}/musics");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            result = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("IdTest", result);
        }
        [Fact, TestPriority(3)]
        public async Task DeleteTest1()
        {
            var myData = new
            {
                UserName = "test",
                Password = "********"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"This login or password is not used\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(3)]
        public async Task DeleteTest2()
        {
            var myData = new
            {
                UserName = "",
                Password = ""
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid login\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(3)]
        public async Task DeleteTest3()
        {
            var myData = new
            {
                UserName = "1",
                Password = "1"
            };
            string jsonData = JsonConvert.SerializeObject(myData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{UrlApi}/delete", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("\"Invalid password\"", await response.Content.ReadAsStringAsync());
        }
        [Fact, TestPriority(4)]
        public async Task DeleteTest4()
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
    }
}