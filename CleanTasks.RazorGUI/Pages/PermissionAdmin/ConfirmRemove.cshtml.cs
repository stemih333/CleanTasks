﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CleanTasks.RazorGUI.Exceptions;
using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanTasks.RazorGUI.Pages.PermissionAdmin
{
    public class ConfirmRemoveModel : BasePermissionAdminModel
    {
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string UserName { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? AreaPermissionId { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string AreaName { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string UserId { get; set; }
        public ConfirmRemoveModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiService userApiService) : base(todoAreaApiClient, appSessionHandler, userApiService)
        { }

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to show confirm page.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to remove area permission.");

            await TodoAreaApiClient.DeleteAreaPermission(AreaPermissionId);

            return RedirectToPage("/PermissionAdmin/Details", new { UserId, UserName });
        }
    }
}