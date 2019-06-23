using Microsoft.AspNetCore.Http;

namespace UserCheck
{
    public class Check
    {
        public static int CheckUserState(HttpRequest request, HttpContext context)
        {
            if (request.Cookies.ContainsKey("user"))
            {
                string id = request.Cookies["user"];
                if (context.Session.GetString(id) != null)
                {
                    return 1;
                }
                return -1;
            }
            return -1;
        }
    }
}
