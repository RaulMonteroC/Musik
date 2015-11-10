using Musik.Core.Data;
using Sphere.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musik.Core.Bussiness
{
    public static class AuthorizationHandler
    {
        private static Repository<User> repository;

        static AuthorizationHandler()
        {
            repository = new ContextRepository<User>();
        }

        public static bool AreValidCredentials(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return false;

            var user = repository.Get(m => m.Username == username && m.Password == password);
            var userExists = user != null;

            return userExists;
        }
    }
}
