﻿using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class StateDropDown
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetCitiesByStateId(ChangeEventArgs events)
        {
            ListWizAddressVM.IsStateChange = true;
            ListWizAddressVM.StateId = Convert.ToInt32(events.Value.ToString());
            await helperFunction.GetCitiesByStateId(ListWizAddressVM);
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (isAllowToGetData)
            {
                await helperFunction.GetStatesByCountryId(ListWizAddressVM);
                StateHasChanged();
            }
        }

        private bool isAllowToGetData
        {
            get { return ListWizAddressVM != null && ListWizAddressVM.CountryId > 0 && ListWizAddressVM.IsFirstLoad; }
        }
    }
}
