using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers;

public abstract class ApiBaseController : ControllerBase
{
   public int UserId => !User.Identity.IsAuthenticated
       ? 0
       : Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}