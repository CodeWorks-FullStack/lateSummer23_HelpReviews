using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpReviews.Services;

public class ReportsService
{
    private readonly ReportsRepository _repo;
    private readonly RestaurantsService _restaurantService;

    public ReportsService(ReportsRepository repo, RestaurantsService restaurantService)
    {
        _repo = repo;
        _restaurantService = restaurantService;
    }

    internal List<Report> GetRestaurantReports(int restaurantId, string userId)
    {
      Restaurant restaurant = _restaurantService.GetById(restaurantId, userId);
      // NOTE becaouse our service already handles the logic of returning the restaurant information if you're the owner / is it shutdown, we don't need to perform any extra logic
      List<Report> reports = _repo.GetRestaurantReports(restaurantId);
      return reports;
    }
}
