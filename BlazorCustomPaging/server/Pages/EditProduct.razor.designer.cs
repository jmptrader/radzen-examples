﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCustomPaging.Models.Sample;
using Microsoft.EntityFrameworkCore;

namespace BlazorCustomPaging.Pages
{
    public partial class EditProductComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SampleService Sample { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        BlazorCustomPaging.Models.Sample.Product _product;
        protected BlazorCustomPaging.Models.Sample.Product product
        {
            get
            {
                return _product;
            }
            set
            {
                if(!object.Equals(_product, value))
                {
                    _product = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            var sampleGetProductByIdResult = await Sample.GetProductById(Id);
            product = sampleGetProductByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCustomPaging.Models.Sample.Product args)
        {
            try
            {
                var sampleUpdateProductResult = await Sample.UpdateProduct(Id, product);
                DialogService.Close(product);
            }
            catch (Exception sampleUpdateProductException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Product");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
