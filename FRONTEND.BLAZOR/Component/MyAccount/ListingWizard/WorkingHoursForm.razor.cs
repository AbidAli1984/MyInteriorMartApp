﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Component.MyAccount.ListingWizard
{
    public partial class WorkingHoursForm
    {
        [Parameter] public int MenuId { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        WorkingHoursVM WorkingHoursVM { get; set; } = new WorkingHoursVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsWorkingHourExist { get; set; }
        public string url;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                url = Constants.getListingUrl(MenuId);
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    helper.NavigateToPageByStep(listing, Constants.SpecialisationComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    var workingHour = await listingService.GetWorkingHoursByListingId(ListingId);
                    if (workingHour != null)
                    {
                        IsWorkingHourExist = true;
                        WorkingHoursVM.SetViewModel(workingHour);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task CopyToAll()
        {
            if (!WorkingHoursVM.IsCopiedToAll())
            {
                helper.ShowNotification(_notice, "Please Select Monday From & Monday To Timing First.", NotificationType.Info);
            }
            await Task.Delay(1);
        }

        public async Task CreateWorkingHoursAsync()
        {
            if (!WorkingHoursVM.isValid())
            {
                helper.ShowNotification(_notice, "From and To Timings are Compulsory for Monday To Friday.", NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                var workingHour = await listingService.GetWorkingHoursByListingId(ListingId);
                bool recordNotFound = workingHour == null;

                if (recordNotFound)
                    workingHour = new BOL.LISTING.WorkingHours();

                WorkingHoursVM.SetContextModel(workingHour);

                if (recordNotFound)
                {
                    workingHour.ListingID = ListingId;
                    workingHour.OwnerGuid = CurrentUserGuid;
                    workingHour.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(workingHour);
                }
                else
                {
                    await listingService.UpdateAsync(workingHour);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.WorkingHourComplete, Steps);
                navManager.NavigateTo($"{url}/PaymentMode");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
