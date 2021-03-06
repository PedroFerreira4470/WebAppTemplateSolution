﻿using Application.Common.Contracts.V1.ResponseType;
using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
        public async Task<(Result Result, string UserId)> CreateUserAsync(User user, string password)
        {
            if (_userManager.Users.Any(p => p.Email == user.Email))
            {
                return (new Result(false, new List<string> { "Email Already Exist" }), null);
            }

            if (_userManager.Users.Any(p => p.UserName == user.UserName))
            {
                return (new Result(false, new List<string> { "Username Already Exist" }), null);
            }

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user is not null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        private async Task<Result> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<(User, SignInResult)> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new RestException(HttpStatusCode.Unauthorized);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return (user, signInResult);
        }
    }
}
