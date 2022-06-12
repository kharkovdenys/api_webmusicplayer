public class Music
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string IdVideo { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string date { get; set; } = string.Empty;
    public Music(string IdVideo,string UserName,string date)
    {
        this.IdVideo = IdVideo;
        this.UserName = UserName;
        this.date = date;
    }
}
