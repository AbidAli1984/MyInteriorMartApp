﻿using BOL.AUDITTRAIL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IAuditRepository
    {
        Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid);
        Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid);
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);

        Task<IEnumerable<Subscribes>> GetSubscriberByListingId(int listingId);
        Task<IEnumerable<Bookmarks>> GetBookmarksByListingId(int listingId);
        Task<IEnumerable<ListingLikeDislike>> GetLikesByListingId(int listingId);
    }
}
