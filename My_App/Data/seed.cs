using My_App.Modles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Data
{
    public class seed
    {
        public static void Sedduser(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userdata = System.IO.File.ReadAllText("Data/UserrSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userdata);

                foreach (var user in users)
                {
                    byte[] passwordhash, passwordsalt;
                    createPasswordHash("password", out passwordhash, out passwordsalt);
                    user.PasswordHash = passwordhash;
                    user.PaswwordSalt = passwordsalt;
                    user.Name = user.Name.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }

        }

        private static void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
