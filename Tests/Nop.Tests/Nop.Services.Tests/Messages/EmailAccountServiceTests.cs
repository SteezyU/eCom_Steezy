﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Messages;
using Nop.Services.Messages;
using NUnit.Framework;

namespace Nop.Tests.Nop.Services.Tests.Messages
{
    [TestFixture]
    public class EmailAccountServiceTests : BaseNopTest
    {
        private IEmailAccountService _emailAccountService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _emailAccountService = GetService<IEmailAccountService>();
        }

        [Test]
        public async Task TestCrud()
        {
            var insertItem = new EmailAccount
            {
                Email = "test@test.com",
                DisplayName = "Test name",
                Host = "smtp.test.com",
                Port = 25,
                Username = "test_user",
                Password = "test_password",
                EnableSsl = false,
                UseDefaultCredentials = false
            };

            var updateItem = new EmailAccount
            {
                Email = "test@test.com",
                DisplayName = "Test name",
                Host = "smtp.test.com",
                Port = 430,
                Username = "test_user",
                Password = "test_password",
                EnableSsl = true,
                UseDefaultCredentials = true
            };

            await TestCrud(insertItem, _emailAccountService.InsertEmailAccountAsync, updateItem, _emailAccountService.UpdateEmailAccountAsync, _emailAccountService.GetEmailAccountByIdAsync, (item, other) => item.UseDefaultCredentials.Equals(other.UseDefaultCredentials) && item.Port.Equals(other.Port) && item.EnableSsl.Equals(other.EnableSsl), _emailAccountService.DeleteEmailAccountAsync);
        }

    }
}
