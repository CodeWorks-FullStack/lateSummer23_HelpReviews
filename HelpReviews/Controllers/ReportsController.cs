using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportsService _reportsService;
    private readonly Auth0Provider _auth;

    public ReportsController(ReportsService reportsService, Auth0Provider auth)
    {
        _reportsService = reportsService;
        _auth = auth;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Report>> CreateReport([FromBody] Report reportData){
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            reportData.CreatorId = userInfo.Id;
            Report report = _reportsService.CreateReport(reportData);
            return Ok(report);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
