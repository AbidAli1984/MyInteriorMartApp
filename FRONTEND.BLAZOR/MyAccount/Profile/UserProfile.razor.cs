﻿using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Profile;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using IDUserProfile = BOL.IDENTITY.UserProfile;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class UserProfile
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationState { get; set; }
        [Inject]
        Helper helper { get; set; }
        [Inject]
        HelperFunctions helperFunction { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        public IDUserProfile userProfile { get; set; }

        UserProfileVM UserProfileVM { get; set; }

        public bool disable { get; set; }
        private bool isImageChange { get; set; }
        public string CurrentUserGuid { get; set; }
        public string IpAddressUser { get; set; }
        public string TimeZoneOfCountry { get; set; } = "India Standard Time";

        protected async override Task OnInitializedAsync()
        {
            try
            {
                UserProfileVM = new UserProfileVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    ApplicationUser applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    UserProfileVM.Email = applicationUser.Email;
                    UserProfileVM.Phone = applicationUser.PhoneNumber;
                    UserProfileVM.isVendor = applicationUser.IsVendor;

                    await GetCurrentUserProfile();
                }
            }
            catch (Exception exc)
            {

            }
        }

        public async Task GetCurrentUserProfile()
        {
            userProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);

            if (userProfile != null)
            {
                UserProfileVM.Gender = userProfile.Gender;
                UserProfileVM.Name = userProfile.Name;
                UserProfileVM.ImgUrl = userProfile.ImageUrl;
            }
        }

        private bool isValidFields()
        {
            if (string.IsNullOrEmpty(UserProfileVM.Gender) || string.IsNullOrEmpty(UserProfileVM.Name))
            {
                helper.ShowNotification(_notice, "All fields are compulsory.", NotificationType.Error);
                return false;
            }

            return true;
        }

        public async Task CreateProfileAsync()
        {
            if (!isValidFields())
                return;

            if (userProfile == null)
            {
                try
                {
                    IDUserProfile userProfile = new IDUserProfile
                    {
                        OwnerGuid = CurrentUserGuid,
                        IPAddress = IpAddressUser,
                        Name = UserProfileVM.Name,
                        Gender = UserProfileVM.Gender,
                        CreatedDate = helper.GetCurrentDateTime(TimeZoneOfCountry),
                        TimeZoneOfCountry = TimeZoneOfCountry,
                        ImageUrl = await MoveProfileImage()
                    };

                    await userProfileService.AddUserProfile(userProfile);
                    navManager.NavigateTo(UserProfileVM.isVendor ? "/" : "/MyAccount/ProfileInfo", true);
                    helper.ShowNotification(_notice, "Your profile created successfully.");
                }
                catch (Exception exc)
                {
                    helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
                }
                return;
            }

            helper.ShowNotification(_notice, "User profile already exists.", NotificationType.Error);
        }

        public async Task UpdateProfileAsync()
        {
            if (!isValidFields())
                return;

            if (userProfile != null)
            {
                try
                {
                    userProfile.Name = UserProfileVM.Name;
                    userProfile.Gender = UserProfileVM.Gender;
                    userProfile.UpdatedDate = helper.GetCurrentDateTime(TimeZoneOfCountry);
                    userProfile.ImageUrl = await MoveProfileImage();

                    await userProfileService.UpdateUserProfile(userProfile);
                    helper.ShowNotification(_notice, "Your profile updated successfully.");
                }
                catch (Exception exc)
                {
                    helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
                }
                return;
            }

            helper.ShowNotification(_notice, "User profile does not exists.", NotificationType.Error);
        }

        public async Task UploadProfileImage()
        {
            UserProfileVM.ImgUrl = await helperFunction.UploadProfileImage(UserProfileVM.file, CurrentUserGuid);
            isImageChange = true;
        }

        private async Task<string> MoveProfileImage()
        {
            if (isImageChange)
            {
                isImageChange = false;
                UserProfileVM.ImgUrl = await helperFunction.MoveProfileImage(UserProfileVM, CurrentUserGuid);
                navManager.NavigateTo("/MyAccount/UserProfile", true);
            }
            return UserProfileVM.ImgUrl;
        }
    }
}