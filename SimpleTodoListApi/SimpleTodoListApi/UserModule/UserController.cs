using Nancy;
using Nancy.ModelBinding;
using System;
using System.Threading.Tasks;

namespace SimpleTodoList.UserModule
{
    public class UserController : NancyModule
    {

        public UserController() : base("/api/users")
        {
            var repo = new UserRepository();

            Get("/login/{email}/{pass}", async args =>
            {
                var email = args.email;
                var pass = args.pass;

                var existingUser = await repo.Find(emailAddress: email);
                if (existingUser != pass)
                {
                    return HttpStatusCode.PreconditionFailed;
                }
                return OkResponse(existingUser);
            });
        }

        private dynamic OkResponse(User user)
        {
            return
                this.Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithHeader("Location", this.Request.Url.SiteBase + "/users/" + user.Id)
                    .WithModel(user);
        }
    }
}
