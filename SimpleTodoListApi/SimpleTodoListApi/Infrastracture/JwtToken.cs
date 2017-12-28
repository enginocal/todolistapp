using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleTodoList.Infrastracture
{
    public static class JwtToken
    {
        public static string GetJwtToken(string client_id)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client_id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            //must the same as your setting in your boostrapper class  
            var symmetricKeyAsBase64 = "Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA==";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "Simple TodoList App",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(10)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(10).TotalSeconds
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
