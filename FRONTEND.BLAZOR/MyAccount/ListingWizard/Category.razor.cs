﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using DAL.Models;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Category
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IListingService listingService { get; set; }
        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public Helper helper { get; set; }

        public CategoryVM CategoryVM { get; set; } = new CategoryVM();
        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public bool IsCategoryExist { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");
                    else if (listing.Steps < Constants.AddressComplete) //Checkig if prev steps compeleted
                        helper.NavigateToPageByStep(listing.Steps, navManager);

                    ListingId = listing.ListingID;
                    CategoryVM.FirstCategories = await categoryService.GetFirstCategoriesAsync();
                    var category = await listingService.GetCategoryByListingId(ListingId);
                    if (category != null)
                    {
                        IsCategoryExist = true;
                        CategoryVM.FirstCategoryID = category.FirstCategoryID;
                        CategoryVM.SecondCategoryID = category.SecondCategoryID;
                        CategoryVM.ThirdCategory = category.ThirdCategories;
                        CategoryVM.FourthCategory = category.FourthCategories;
                        CategoryVM.FifthCategory = category.FifthCategories;
                        CategoryVM.SixthCategory = category.SixthCategories;

                        await GetSecondCategoryIdByFirstCategoryId(null);
                        await GetOtherCategoriesBySecondCategoryId(null);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task GetSecondCategoryIdByFirstCategoryId(ChangeEventArgs events)
        {
            try
            {
                await categoryService.GetSecCategoriesByFirstCategoryId(CategoryVM, events);
                StateHasChanged();
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task GetOtherCategoriesBySecondCategoryId(ChangeEventArgs events)
        {
            try
            {
                if (events != null)
                {
                    int.TryParse(events.Value.ToString(), out int secondCatId);
                    CategoryVM.SecondCategoryID = secondCatId;
                }
                await categoryService.GetOtherCategoriesBySeconCategoryId(CategoryVM);
                await Task.Delay(500);
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task SelectAllCategories()
        {
            categoryService.MarkAllCategoriesSelected(CategoryVM);
            await Task.Delay(1);
        }

        public async Task CreateListingCategory()
        {
            if (!CategoryVM.isValid())
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"First Category and Second Category must not be blank.");
                return;
            }

            try
            {
                buttonBusy = true;
                var category = await listingService.GetCategoryByListingId(ListingId);
                bool recordNotFound = category == null;

                if (recordNotFound)
                    category = new BOL.LISTING.Categories();

                categoryService.GetOtherCategoriesToUpdate(CategoryVM);
                category.FirstCategoryID = CategoryVM.FirstCategoryID;
                category.SecondCategoryID = CategoryVM.SecondCategoryID;
                category.ThirdCategories = CategoryVM.ThirdCategory;
                category.FourthCategories = CategoryVM.FourthCategory;
                category.FifthCategories = CategoryVM.FifthCategory;
                category.SixthCategories = CategoryVM.SixthCategory;

                if (recordNotFound)
                {
                    category.ListingID = ListingId;
                    category.OwnerGuid = CurrentUserGuid;
                    category.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(category);
                }
                else
                {
                    await listingService.UpdateAsync(category);
                }

                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                if (listing.Steps < Constants.CategoryComplete)
                {
                    listing.Steps = Constants.CategoryComplete;
                    await listingService.UpdateAsync(listing);
                }

                navManager.NavigateTo($"/MyAccount/Listing/Specialisation");
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
