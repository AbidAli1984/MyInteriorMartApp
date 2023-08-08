﻿using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.ComponentModels.Listings;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingResults
    {

        [Inject]
        private IListingService listingService { get; set; }

        [Parameter]
        public string url { get; set; }

        [Parameter]
        public string level { get; set; }

        public IList<ListingResultVM> ListingResultVM = new List<ListingResultVM>();

        public int TotalPages { get; set; }

        // Begin: Get All Category Banner
        public int Banner1Count { get; set; }
        public int Banner2Count { get; set; }
        public int Banner3Count { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await PopulateListFLVM();
            await GetCategoryBannerListAsync();
            if(ListingResultVM.Count > 0)
            {
                TotalPages = ListingResultVM.Count / 10;

                if (ListingResultVM.Count % 10 > 0)
                    TotalPages++;
            }
        }

        protected override async Task OnAfterRenderAsync(bool render)
        {
            if (render)
            {
                await jsRuntime.InvokeVoidAsync("InitializeCarousel");
            }
        }

        public async Task PopulateListFLVM()
        {
            ListingResultVM = await listingService.GetListings(url, level);
            await Task.Delay(50);
        }


        public IEnumerable<CategoryBanner> CategoryBannerList { get; set; }
        public async Task GetCategoryBannerListAsync()
        {
            var thirdCat = await categoriesContext.ThirdCategory
                .Where(i => i.URL == url)
                .FirstOrDefaultAsync();

            CategoryBannerList = await listingContext.CategoryBanner
                .Where(i => i.ThirdCategoryID == thirdCat.ThirdCategoryID)
                .OrderBy(i => i.Priority)
                .ToListAsync();

            Banner1Count = CategoryBannerList.Where(i => i.Placement == "banner-1").Count();

            Banner2Count = CategoryBannerList.Where(i => i.Placement == "banner-2").Count();

            Banner3Count = CategoryBannerList.Where(i => i.Placement == "banner-3").Count();
        }
    }
}
