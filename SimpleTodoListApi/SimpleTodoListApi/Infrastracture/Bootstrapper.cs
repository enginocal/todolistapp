namespace SimpleTodoList.Infrastracture
{
    using System;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Nancy;
    using Nancy.Authentication.JwtBearer;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
         => NancyInternalConfiguration.WithOverrides(builder => builder.StatusCodeHandlers.Clear());

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var keyByteArray = Encoding.ASCII.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA==");
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:5000",
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "Simple TodoList Core App",
                // Validate the token expiry
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var configuration = new JwtBearerAuthenticationConfiguration
            {
                TokenValidationParameters = tokenValidationParameters
            };

            //enable the JwtBearer authentication
            pipelines.EnableJwtBearerAuthentication(configuration);

            pipelines.OnError += (ctx, ex) =>
             {
                 // write to central log store
                 Console.WriteLine(ex);
                 return ex;
             };
        }
    }
}
