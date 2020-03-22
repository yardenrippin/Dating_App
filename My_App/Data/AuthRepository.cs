﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_App.Modles;

namespace My_App.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(X => X.Name == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PaswwordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] paswwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(paswwordSalt))
            {
                
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i]!=passwordHash[i]) return false;



                }

            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
        Byte[] PasswordHash, PasswordSalt;
        createPasswordHash(password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PaswwordSalt = PasswordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public async Task<bool> UserExists(string usenume)
        {
          
                if (await _context.Users.AnyAsync(X => X.Name == usenume))

                    return true;

                return false;
            
        
           

        }
    }
}
