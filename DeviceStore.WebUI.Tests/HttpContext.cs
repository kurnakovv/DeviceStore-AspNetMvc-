using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DeviceStore.WebUI.Tests
{
    class HttpContext : HttpContextBase
    {
        private Request _request;
        private Response _response;
        private HttpCookieCollection _cookies;
        private IPrincipal _fakeUser;

        public HttpContext()
        {
            _cookies = new HttpCookieCollection();
            _request = new Request(_cookies);
            _response = new Response(_cookies);
        }

        public override IPrincipal User
        {
            get
            {
                return _fakeUser;
            }
            set
            {
                _fakeUser = value;
            }

        }

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return _response;
            }
        }
    }

    public class Response : HttpResponseBase
    {
        private readonly HttpCookieCollection _cookies;

        public Response(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }
    }

    public class Request : HttpRequestBase
    {
        private readonly HttpCookieCollection _cookies;

        public Request(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }
    }
}
