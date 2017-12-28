using Nancy;
using Nancy.ModelBinding;
using SimpleTodoList.Infrastracture;
using SimpleTodoList.UserModule;
using System;

namespace SimpleTodoList.Register
{
    public class RegisterModule : NancyModule
    {
        public RegisterModule():base("/api/register")
        {
            var repo = new UserRepository();
            Post("/", async args =>
             {
                 var registeringUser = this.Bind<RegisterModel>();
                 if (string.IsNullOrEmpty(registeringUser?.EmailAddress))
                 {
                     return new NotFoundResponse();
                 }

                 var isExist = await repo.IsExist(registeringUser.EmailAddress);
                 if (isExist)
                 {
                     var r = (Response)"This email address already registered.";
                     r.StatusCode = HttpStatusCode.NotAcceptable;
                     return r;
                 }

                 var user = new User()
                 {
                     Id = Guid.NewGuid(),
                     EmailAddress = registeringUser.EmailAddress,
                     Password = registeringUser.Password,
                     FirstName = registeringUser.FirstName,
                     LastName = registeringUser.LastName
                 };
                 await repo.Insert(user);
                 return CreatedResponse(user);
             });

            Get("/token/{clientId}", args =>
            {
                var token = JwtToken.GetJwtToken(args.clientId);
                return token;
            });
        }

        private dynamic CreatedResponse(User newUser)
        {
            return
                this.Negotiate
                    .WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", this.Request.Url.SiteBase + "/api/users/" + newUser.Id)
                    .WithModel(newUser);
        }
    }
}
