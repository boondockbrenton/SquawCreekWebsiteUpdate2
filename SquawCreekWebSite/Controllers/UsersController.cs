using SquawCreekWebSite.DB;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SquawCreekWebSite.Controllers
{ 
    /// <summary>
    /// The controller for the users actions
    /// </summary>
    [System.Web.Mvc.RoutePrefix("api/users")]
    public class UsersController : ApiController
    {

        public IHttpActionResult PostEvents(UsersFilter ev)
        {
            var value = new NameValueCollection();
            value["sid"] = "DG43tSG235%24";
            value["token"] = "%2fsdf2";
            var cookie = new CookieHeaderValue("sqkwebsite", value);

            switch (ev.action)
            {
                case "checkUserPass":
                    return new HttpResult(Content(HttpStatusCode.OK, CheckUserPass(ev.user, ev.password)), res => res.Headers.AddCookies(new[] { cookie }));
                    //return Ok(CheckUserPass(ev.user, ev.password));             
                default:
                    return Ok(-1);
            }
        }

        private string CheckUserPass(string user, string pass)
        {
            if ((new UsersDB()).VerifyUserPass(user, pass))
                return "/HTML/tee-times-calendar.html";
            else
                return string.Empty;
        }
    }

    public class UsersFilter
    {
        public string action { get; set; }
        public long id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }

    public class HttpResult : IHttpActionResult
    {
        private readonly IHttpActionResult _decorated;
        private readonly Action<HttpResponseMessage> _response;

        public HttpResult(IHttpActionResult decorated, Action<HttpResponseMessage> response)
        {
            _decorated = decorated;
            _response = response;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await _decorated.ExecuteAsync(cancellationToken);
            _response(response);
            return response;
        }
    }
}
