﻿using System;
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
            var users = _Context.Users.Include(x => x.Photos);

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
    }
}