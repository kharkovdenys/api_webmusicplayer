var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddDbContext<UserDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connect"));
});

builder.Services.AddDbContext<MusicDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connect"));
});
builder.Services.AddSingleton<ITokenService>(new TokenService());
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapPost("/register", async ([FromBody] User newuser, UserDb db) =>
{
    if (newuser.UserName == "" || newuser.UserName == null)
    {
        return Results.BadRequest("Invalid login");
    }
    if (newuser.Password.Length < 8 || newuser.Password == null)
    {
        return Results.BadRequest("Invalid password");
    }
    var finduser = await db.Users.FirstOrDefaultAsync(x => x.UserName == newuser.UserName);
    if (finduser != null)
        return Results.BadRequest("This login is already in use");
    newuser.Password = BCrypt.Net.BCrypt.HashPassword(newuser.Password);
    await db.Users.AddAsync(newuser);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapPost("/login", async ([FromBody] User checkuser, ITokenService tokenService, UserDb db) =>
{
    if (checkuser.UserName == "" || checkuser.UserName == null)
    {
        return Results.BadRequest("Invalid login");
    }
    if (checkuser.Password.Length < 8 || checkuser.Password == null)
    {
        return Results.BadRequest("Invalid password");
    }
    var finduser = await db.Users.FirstOrDefaultAsync(x => x.UserName == checkuser.UserName);
    if (finduser == null || !BCrypt.Net.BCrypt.Verify(checkuser.Password, finduser.Password))
    {
        return Results.BadRequest("This login or password is not used");
    }
    var token = tokenService.Create(checkuser.UserName);
    return Results.Ok(new { token });
});
app.MapPost("/delete", async ([FromBody] User checkuser, UserDb db) =>
{
    if (checkuser.UserName == "" || checkuser.UserName == null)
    {
        return Results.BadRequest("Invalid login");
    }
    if (checkuser.Password.Length < 8 || checkuser.Password == null)
    {
        return Results.BadRequest("Invalid password");
    }
    var finduser = await db.Users.FirstOrDefaultAsync(x => x.UserName == checkuser.UserName);
    if (finduser == null || !BCrypt.Net.BCrypt.Verify(checkuser.Password, finduser.Password))
    {
        return Results.BadRequest("This login or password is not used");
    }
    db.Users.Remove(finduser);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapGet("/info", (HttpContext context, UserDb db, ITokenService tokenService) =>
{
    try
    {
        var token = context.Request.Headers.Authorization;
        return Results.Ok(tokenService.Read(token!));
    }
    catch (Exception)
    {
        return Results.BadRequest("Error Authentication");
    }
});
app.MapGet("/musics", async (HttpContext context, MusicDb db, ITokenService tokenService) =>
{
    try
    {
        var token = context.Request.Headers.Authorization;
        string UserName = tokenService.Read(token!);
        return Results.Ok(await db.Musics.Where(m => m.UserName == UserName).OrderByDescending(m => m.date).Take(20).ToListAsync());
    }
    catch (Exception)
    {
        return Results.BadRequest("Token error");
    }
});
app.MapPost("/musics", async ([FromBody] MusicId music, MusicDb db, HttpContext context, ITokenService tokenService) =>
 {
     try
     {
         var token = context.Request.Headers.Authorization;
         string UserName = tokenService.Read(token!);
         var find = await db.Musics.FirstOrDefaultAsync(x => x.UserName == UserName && x.IdVideo == music.IdVideo);
         if(find != null)
             db.Musics.Remove(find);
         Music history = new(music.IdVideo, UserName, DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
         await db.Musics.AddAsync(history);
         await db.SaveChangesAsync();
         return Results.Ok();
     }
     catch (Exception)
     {
         return Results.BadRequest("Token error");
     }
 });
app.MapDelete("/musics", async ([FromBody] MusicId music, MusicDb db, HttpContext context, ITokenService tokenService) =>
{
    try
    {
        var token = context.Request.Headers.Authorization;
        string UserName = tokenService.Read(token!);
        var find = await db.Musics.FirstOrDefaultAsync(x=>x.UserName==UserName&&x.IdVideo==music.IdVideo);
        if(find==null)
            return Results.BadRequest("Not found");
        db.Musics.Remove(find);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception)
    {
        return Results.BadRequest("Token error");
    }

});
app.MapPost("/user", async ([FromBody] UserName find, UserDb db, HttpContext context) =>
{
    try
    {
    if (find.username.Length<1)
            return Results.BadRequest("Find data empty");
        return Results.Ok(await db.Users.Select(x=>x.UserName).Where(y => y.Substring(0,find.username.Length)== find.username).ToListAsync());
    }
    catch (Exception)
    {
        return Results.BadRequest("Error data");
    }
});
app.Run();