using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

    public class IndexModel(IUserFacade userFacade) : PageModel
    {
        public UserDto UserDto { get; set; }

        public async Task OnGet()
        {
            var user = await userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            UserDto = user;
        }
    }

