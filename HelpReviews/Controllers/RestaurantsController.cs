namespace HelpReviews.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantsService _restaurantsService;
    private readonly ReportsService _reportsService;

    private readonly Auth0Provider _auth;

    public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth, ReportsService reportsService)
    {
        _restaurantsService = restaurantsService;
        _reportsService = reportsService;
        _auth = auth;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurantData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            restaurantData.CreatorId = userInfo.Id;
            Restaurant newRestaurant = _restaurantsService.Create(restaurantData);
            return Ok(newRestaurant);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Restaurant>>> Get()
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Restaurant> restaurants = _restaurantsService.Get(userInfo?.Id);
            return Ok(restaurants);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult<Restaurant>> GetById(int restaurantId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Restaurant restaurant = _restaurantsService.GetById(restaurantId, userInfo?.Id);
            return Ok(restaurant);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{restaurantId}/reports")]
    public async Task<ActionResult<List<Report>>> GetRestaurantReports(int restaurantId){
        try
        {
            Account userInfo =await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Report> reports = _reportsService.GetRestaurantReports(restaurantId, userInfo?.Id);
            return Ok(reports);
        }   
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{restaurantId}")]
    public async Task<ActionResult<Restaurant>> Edit([FromBody] Restaurant updateData, int restaurantId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Restaurant edited = _restaurantsService.Edit(updateData, restaurantId, userInfo.Id);
            return Ok(edited);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}