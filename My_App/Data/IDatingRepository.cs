using My_App.Helpers;
using My_App.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> Saveall();
        Task<PageList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<photo> GetPhoto(int id);
        Task<photo> GetmainPhotoOfUser(int userId);
    }
}
