namespace HelpReviews.Repositories;

using System.Collections.Generic;
using HelpReviews.Interfaces;

public class RestaurantRepository : IRepository<Restaurant, int>
{

    private readonly IDbConnection _db;

    public RestaurantRepository(IDbConnection db)
    {
        _db = db;
    }

    public Restaurant Create(Restaurant newData)
    {
        string sql = @"
       INSERT INTO restaurants
       (name, imgUrl, description, creatorId)
       VALUES
       (@name, @imgUrl, @description, @creatorId);

       SELECT
            r.*,
            a.*
       FROM restaurants r
       JOIN accounts a ON a.id = r.creatorId
       WHERE r.id = LAST_INSERT_ID()
       ;";
        Restaurant newRestaurant = _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) =>
        {
            restaurant.Creator = profile;
            return restaurant;
        }, newData).FirstOrDefault();
        return newRestaurant;
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Restaurant> Get()
    {
        string sql = @"
       SELECT
            restaurants.*,
            COUNT(reports.id) AS reportCount,
            accounts.*
        FROM restaurants
        LEFT JOIN reports ON  reports.restaurantId = restaurants.id
        JOIN accounts ON accounts.id = restaurants.creatorId
        GROUP BY (restaurants.id)
        ORDER BY (restaurants.visits) DESC
        ;";
        
        List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) =>
        {
            restaurant.Creator = profile;
            return restaurant;
        }).ToList();
        return restaurants;
    }

    public Restaurant GetById(int id)
    {
        string sql = @"
        SELECT
            restaurants.*,
            COUNT(reports.id) AS reportCount,
            accounts.*
        FROM restaurants
        JOIN accounts ON accounts.id = restaurants.creatorId
        LEFT JOIN reports ON reports.restaurantId = restaurants.id
        WHERE restaurants.id = @id
        GROUP BY (restaurants.id)
        ;";
        Restaurant restaurant = _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) =>
        {
            restaurant.Creator = profile;
            return restaurant;
        }, new { id }).FirstOrDefault();
        return restaurant;
    }

    public void Update(Restaurant updateData)
    {
        string sql = @"
        UPDATE restaurants
        SET
        name = @name,
        imgUrl = @imgUrl,
        description = @description,
        visits = @visits,
        isShutdown = @isShutdown
        WHERE id = @id
        ;";
        _db.Execute(sql, updateData);
    }
}
