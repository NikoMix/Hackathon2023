using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Hackathon2023.Utilities;

namespace Hackathon2023.Controllers;

[ApiController]
public class ScreenSharingController : ControllerBase
{
    /// <summary>
    /// Changes screen sharing role.
    /// </summary>
    /// <param name="callLegId">
    /// The call leg identifier.
    /// </param>
    /// <param name="changeRoleBody">
    /// The role to change to.
    /// </param>
    /// <returns>
    /// The <see cref="HttpResponseMessage"/>.
    /// </returns>
    [HttpPost]
    [Route(HttpRouteConstants.CallRoute + "/" + HttpRouteConstants.OnChangeRoleRoute)]
    public async Task<IActionResult> ChangeScreenSharingRoleAsync(string callLegId, [FromBody] ChangeRoleBody changeRoleBody)
    {
        try
        {
            // await Bot.Instance.ChangeSharingRoleAsync(callLegId, changeRoleBody.Role).ConfigureAwait(false);
            // return this.Request.CreateResponse(HttpStatusCode.OK);

            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
            //return e.InspectExceptionAndReturnResponse();
        }
    }

    /// <summary>
    /// Request body content to update screen sharing role.
    /// </summary>
    public class ChangeRoleBody
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role to change to.
        /// </value>
        public ScreenSharingRole Role { get; set; }
    }
}

