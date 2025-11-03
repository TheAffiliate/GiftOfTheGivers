using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using GiftOfTheGivers_ST10239864.Models;

namespace GiftOfTheGivers_ST10239864.UnitTests
{
    //Using 'internal' access modifier to make these classes available 
    // only within the UnitTests project.

    internal class MockUserManager : UserManager<ApplicationUser>
    {
        public MockUserManager(IUserStore<ApplicationUser> store)
            : base(store,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<ApplicationUser>>().Object,
                  new List<IUserValidator<ApplicationUser>>().AsEnumerable(),
                  new List<IPasswordValidator<ApplicationUser>>().AsEnumerable(),
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }
    }

    internal class MockSignInManager : SignInManager<ApplicationUser>
    {
        public MockSignInManager(UserManager<ApplicationUser> userManager)
            : base(userManager,
                  new Mock<IHttpContextAccessor>().Object,
                  new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                  new Mock<IAuthenticationSchemeProvider>().Object,
                  new Mock<IUserConfirmation<ApplicationUser>>().Object)
        { }
    }

    internal class MockRoleManager : RoleManager<IdentityRole>
    {
        public MockRoleManager(IRoleStore<IdentityRole> store)
            : base(store,
                  new List<IRoleValidator<IdentityRole>>().AsEnumerable(),
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
        { }
    }
}