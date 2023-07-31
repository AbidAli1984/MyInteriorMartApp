﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class PaymentMode
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IListingService listingService { get; set; }
        [Inject]
        Helper helper { get; set; }

        PaymentModeVM PaymentModeVM { get; set; } = new PaymentModeVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public bool IsWorkingHourExist { get; set; }

 
        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");
                    else if (listing.Steps < Constants.WorkingHourComplete) //Checking if prev steps compeleted
                        helper.NavigateToPageByStep(listing.Steps, navManager);

                    ListingId = listing.ListingID;
                    var paymentMode = await listingService.GetPaymentModeByListingId(ListingId);
                    if (paymentMode != null)
                    {
                        IsWorkingHourExist = true;
                        PaymentModeVM.SetViewModel(paymentMode);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task ToggleAllAsync()
        {
            PaymentModeVM.selectOrUnselectAll();
            await Task.Delay(1);
        }

        public async Task AddOrUpdatePaymentMode()
        {
            if (!PaymentModeVM.isValid())
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Please select at least one payment mode.");
                return;
            }

            try
            {
                buttonBusy = true;
                var paymentMode = await listingService.GetPaymentModeByListingId(ListingId);
                bool recordNotFound = paymentMode == null;

                if (recordNotFound)
                    paymentMode = new BOL.LISTING.PaymentMode();

                PaymentModeVM.SetPaymentMode(paymentMode);

                if (recordNotFound)
                {
                    paymentMode.ListingID = ListingId;
                    paymentMode.OwnerGuid = CurrentUserGuid;
                    paymentMode.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(paymentMode);
                }
                else
                {
                    await listingService.UpdateAsync(paymentMode);
                }

                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                if (listing.Steps < Constants.PaymentModeComplete)
                {
                    listing.Steps = Constants.PaymentModeComplete;
                    await listingService.UpdateAsync(listing);
                }

                //navManager.NavigateTo("/MyAccount/Listing/PaymentMode");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
