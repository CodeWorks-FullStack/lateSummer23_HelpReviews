

namespace HelpReviews.Services;

public class RestaurantsService
{
    private readonly RestaurantRepository _repo;

    public RestaurantsService(RestaurantRepository repo)
    {
        _repo = repo;
    }


    internal Restaurant Create(Restaurant restaurantData)
    {
        return _repo.Create(restaurantData);
    }


    internal List<Restaurant> Get(string userId)
    {
        List<Restaurant> restaurants = _repo.Get();
        restaurants = restaurants.FindAll(restaurant => restaurant.IsShutdown == false || restaurant.CreatorId == userId);
        // NOTE ^^ filter out and only return the restaurants that are not shutdown OR return them all if I am the creator....if I am the creator of the shutdown restaurant
        return restaurants;
    }

    internal Restaurant GetById(int restaurantId, string userId, bool increaseVisits = false)
    {
        Restaurant restaurant = _repo.GetById(restaurantId);
        if (restaurant == null) throw new Exception($"Invalid id {restaurantId}");

        if( restaurant.IsShutdown == true && restaurant.CreatorId != userId) throw new Exception($"No {restaurant.Name} for you!");
        // NOTE increase visits is passed from the controller to increase the visits of the restaurant
        // the or keeps the restaurant creator from inflating their own visit count
        if(increaseVisits && restaurant.CreatorId != userId){
        this.IncreaseVisits(restaurant);
        }
        return restaurant;
    }


    internal Restaurant Edit(Restaurant updateData, int restaurantId, string userId)
    {
        Restaurant original = this.GetById(restaurantId, userId);
        if (original.CreatorId != userId) throw new Exception("Unauthorized to edit");
        original.Name = updateData.Name ?? original.Name;
        original.IsShutdown = updateData.IsShutdown ?? original.IsShutdown;

        _repo.Update(original);

        return original;

    }

    internal void IncreaseVisits( Restaurant restaurant){
        restaurant.Visits++;
        _repo.Update(restaurant);
    }


}