﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.What3words.Models;
using Nop.Plugin.Widgets.What3words.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.What3words.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class What3wordsController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly What3wordsHttpClient _what3WordsHttpClient;
        private readonly What3wordsSettings _what3WordsSettings;

        #endregion

        #region Ctor

        public What3wordsController(ILocalizationService localizationService,
            ILogger logger,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            IWorkContext workContext,
            What3wordsHttpClient what3WordsHttpClient,
            What3wordsSettings what3WordsSettings)
        {
            _localizationService = localizationService;
            _logger = logger;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
            _workContext = workContext;
            _what3WordsHttpClient = what3WordsHttpClient;
            _what3WordsSettings = what3WordsSettings;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                Enabled = _what3WordsSettings.Enabled
            };

            return View("~/Plugins/Widgets.What3words/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return await Configure();

            //request client API key if doesn't exist
            if (string.IsNullOrEmpty(_what3WordsSettings.ApiKey))
            {
                try
                {
                    var store = await _storeContext.GetCurrentStoreAsync();
                    var storeUrl = $"{store.Url?.TrimEnd('/')}/";
                    var apiKey = await _what3WordsHttpClient.RequestClientApiAsync(storeUrl);
                    _what3WordsSettings.ApiKey = apiKey;
                }
                catch (Exception exception)
                {
                    await _logger.ErrorAsync($"what3words error: {exception.Message}.", exception, await _workContext.GetCurrentCustomerAsync());
                    _notificationService
                        .ErrorNotification(await _localizationService.GetResourceAsync("Plugins.Widgets.What3words.Configuration.Failed"));
                    return await Configure();
                }
            }

            _what3WordsSettings.Enabled = model.Enabled;
            await _settingService.SaveSettingAsync(_what3WordsSettings);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }
    }

    #endregion
}