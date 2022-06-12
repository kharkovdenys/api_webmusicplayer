public class TokenService : ITokenService
{
    private string key = "_webmusic-chaha_";
    public string Create(string UserName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, UserName),
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken("chaha", "chaha", claims,
            expires: DateTime.Today.AddDays(365), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    public string Read(string token)
    {
        var keys = Encoding.ASCII.GetBytes(key);
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keys),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        var claims = handler.ValidateToken(token, validations, out var tokenSecure);
        return claims.Identity!.Name!;
    }
}