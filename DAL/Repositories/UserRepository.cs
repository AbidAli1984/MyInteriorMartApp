﻿using DAL.Repositories.Contracts;
using DAL.Models;
using DAL.USER;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.IDENTITY;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext userDbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<ApplicationUser> AdOrUpdateUser(ApplicationUser user)
        {
            var userToUpdte = await GetUserByMobileNo(user.PhoneNumber);
            if (userToUpdte != null)
            {
                userToUpdte.Otp = user.Otp;
                userDbContext.SaveChanges();
                return userToUpdte;
            }
            else
            {
                var result = await userDbContext.Users.AddAsync(user);
                userDbContext.SaveChanges();
                return result.Entity;
            }
        }

        public async Task<ApplicationUser> GetUserByMobileNo(string mobileNo)
        {
            return await userDbContext.Users.Where(x => x.PhoneNumber == mobileNo).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetRegisterdUserByMobileNo(string mobileNo)
        {
            return await userDbContext.Users.Where(x => x.PhoneNumber == mobileNo && x.IsRegistrationCompleted).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserByUserNameOrEmail(string userNameOrEmail)
        {
            return await userDbContext.Users.Where(i => i.UserName == userNameOrEmail || i.Email == userNameOrEmail).FirstOrDefaultAsync();
        }

        public async Task<bool> VerifyOTP(string phoneNumber, string otp)
        {
            var user = await userDbContext.Users.Where(x => x.PhoneNumber == phoneNumber && x.Otp == otp).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid)
        {
            return await userDbContext.UserProfile.Where(p => p.OwnerGuid == ownerGuid).FirstOrDefaultAsync();
        }

        public async Task DeleteUserByPhoneNumberOrEmail(ApplicationUser user)
        {
            var userToDelete = await userDbContext.Users.Where(x => x.PhoneNumber == user.PhoneNumber || x.Email == user.Email).FirstOrDefaultAsync();
            if (userToDelete != null)
            {
                userDbContext.Remove(userToDelete);
                userDbContext.SaveChanges();
            }
        }
    }
}
