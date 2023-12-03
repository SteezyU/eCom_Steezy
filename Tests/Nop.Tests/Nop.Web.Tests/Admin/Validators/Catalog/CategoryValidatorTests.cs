﻿using FluentValidation.TestHelper;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Validators.Catalog;
using NUnit.Framework;

namespace Nop.Tests.Nop.Web.Tests.Admin.Validators.Catalog
{
    [TestFixture]
    public class CategoryValidatorTests : BaseNopTest
    {
        private CategoryValidator _validator;

        [OneTimeSetUp]
        public void Setup()
        {
            _validator = GetService<CategoryValidator>();
        }

        [Test]
        public void ShouldHaveErrorWhenPageSizeOptionsHasDuplicateItems()
        {
            var model = new CategoryModel
            {
                PageSizeOptions = "1, 2, 3, 5, 2"
            };
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.PageSizeOptions);
        }

        [Test]
        public void ShouldNotHaveErrorWhenPageSizeOptionsHasNotDuplicateItems()
        {
            var model = new CategoryModel
            {
                PageSizeOptions = "1, 2, 3, 5, 9"
            };
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.PageSizeOptions);
        }

        [Test]
        public void ShouldNotHaveErrorWhenPageSizeOptionsIsNullOrEmpty()
        {
            var model = new CategoryModel
            {
                PageSizeOptions = null
            };
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.PageSizeOptions);
            model.PageSizeOptions = string.Empty;
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.PageSizeOptions);
        }
    }
}