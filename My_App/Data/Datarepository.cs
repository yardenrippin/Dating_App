using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_App.Helpers;
using My_App.Modles;

namespace My_App.Data
{
    public class Datarepository : IDatingRepository
    {

        public DataContext _Context { get; }

        public Datarepository(DataContext context)
        {
            _Context = context;
        }



        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _Context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<PageList<User>> GetUsers( UserParams userParams)
        {
            var users = _Context.Users.Include(x => x.Photos)
                .OrderByDescending(x=>x.LastActive).AsQueryable();

            users = users.Where(x => x.Id != userParams.UserId);

            users = users.Where(x => x.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await getuserlikse(userParams.UserId, userParams.Likers);
                users = users.Where(x => userLikers.Contains(x.Id));

            }

            else if (userParams.Likees)
            {
                var userLikees = await getuserlikse(userParams.UserId, userParams.Likers);
                users = users.Where(x => userLikees.Contains(x.Id));
            }
        

            else
            {
                if ((userParams.MinAge != 18 || userParams.MaxAge != 99))
                { 
                var mindb = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxdb = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(X => X.DateOfBirth >= mindb && X.DateOfBirth <= maxdb);
            }
        }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":users = users.OrderByDescending(x => x.Created);
                        break;
                    default:
                        users = users.OrderByDescending(X => X.LastActive);
                        break;
                }
            }
            return await PageList<User>.CreateAsync(users, userParams.PageNumber, userParams.Pagesize);
        }

        public async Task<bool> Saveall()
        {
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<photo> GetPhoto(int id)
        {
            var photo = await _Context.photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<photo> GetmainPhotoOfUser(int userId)
        {
            return await _Context.photos.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(P => P.Ismain);
        }

        public async Task<Likes> GetLikes(int userid, int recipientid)
        {
            return await _Context.Likes.FirstOrDefaultAsync
                (x => x.LikerId == userid && x.LikeeId == recipientid);
           
        }

        private async Task<IEnumerable<int>> getuserlikse(int id,bool likers)
        {
            var user = await _Context.Users.Include(X => X.Likers)
                .Include(X => X.Likees)
                .FirstOrDefaultAsync(X => X.Id == id);

            if (likers)
            {
                var usersid = user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);

                return usersid;

            }
            else
            {
                var usersid = user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);


                return usersid;
            }

        }
    }
}