﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using DAL.Models;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Component.MyAccount.ListingWizard
{
    public partial class CategoryForm
    {
        [Parameter] public int MenuId { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] public IListingService listingService { get; set; }
        [Inject] public ICategoryService categoryService { get; set; }
        [Inject] public IUserService userService { get; set; }
        [Inject] public Helper helper { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        public CategoryVM CategoryVM { get; set; } = new CategoryVM();
        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsCategoryExist { get; set; }
        public string url;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                url = Constants.getListingUrl(MenuId);
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    helper.NavigateToPageByStep(listing, Constants.AddressComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    CategoryVM.FirstCategories = await categoryService.GetFirstCategoriesAsync();
                    var category = await listingService.GetCategoryByListingId(ListingId);
                    if (category != null)
                    {
                        IsCategoryExist = true;
                        CategoryVM.SetViewModel(category);

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
                helper.ShowNotification(_notice, $"First Category and Second Category must not be blank.", NotificationType.Info);
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
                CategoryVM.SetContextModel(category);

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

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.CategoryComplete, Steps);
                navManager.NavigateTo($"{url}/Specialisation");
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
