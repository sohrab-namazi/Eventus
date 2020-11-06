﻿using ArsamBackend.Controllers;
using ArsamBackend.Services;
using ArsamBackend.Utilities;
using Eventus.UnitTest.MockedServices;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventus.UnitTest.TestUtilities
{
    public abstract class TestBase
    {
        #region mock
        public Mock<FakeUserManager> mockUserManager = new Mock<FakeUserManager>();
        public Mock<FakeSignInManager> mockSigninManager = new Mock<FakeSignInManager>();
        public Mock<ILogger<AccountController>> mockLogger = new Mock<ILogger<AccountController>>();
        public Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
        public Mock<IDataProtectionProvider> mockDPProvider = new Mock<IDataProtectionProvider>();
        public Mock<IJWTHandler> mockJWTHandler = new Mock<IJWTHandler>();
        public Mock<DataProtectionPurposeStrings> mockDPPurposeStrings = new Mock<DataProtectionPurposeStrings>();
        public Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
        public Mock<HttpContext> mockContext = new Mock<HttpContext>();
        public Mock<IUrlHelper> mockUrl = new Mock<IUrlHelper>();
        public JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion mock
    }
}