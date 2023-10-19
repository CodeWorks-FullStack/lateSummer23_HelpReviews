using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpReviews.Interfaces;

namespace HelpReviews.Repositories;

public class ReportsRepository : IRepository<Report, int>
{
    private readonly IDbConnection _db;

    public ReportsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Report Create(Report newData)
    {
      string sql = @"
      INSERT INTO reports
      (title, body, pictureOfDisgust, creatorId, restaurantId)
      VALUES
      (@title, @body, @pictureOfDisgust, @creatorId, @restaurantId);

      SELECT
        reports.*,
        accounts.*
      FROM reports
      JOIN accounts ON accounts.id = reports.creatorId
      WHERE reports.id = LAST_INSERT_ID()
      ;";
      Report report = _db.Query<Report, Profile, Report>(sql, (report, profile)=>{
        report.Creator = profile;
        return report;
      }, newData).FirstOrDefault();
      return report;
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Report> Get()
    {
        throw new NotImplementedException();
    }

    public Report GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Report updateData)
    {
        throw new NotImplementedException();
    }

    internal List<Report> GetRestaurantReports(int restaurantId)
    {
      string sql = @"
        SELECT
          reports.*,
          repCreator.*,
          restaurants.*,
          restCreator.*
        FROM reports
          JOIN accounts repCreator ON repCreator.id = reports.creatorId
          JOIN restaurants ON restaurants.id = reports.restaurantId
          JOIN accounts restCreator ON restCreator.id = restaurants.creatorId
        WHERE reports.restaurantId = @restaurantId
      ;";

      List<Report> reports = _db.Query<Report, Profile, Restaurant, Profile, Report>(sql,(report, repCreator, restaurant, restCreator)=>{
        report.Creator = repCreator;
        restaurant.Creator = restCreator;
        report.Restaurant = restaurant;
        
        return report;
      }, new{restaurantId}).ToList();
      return reports;
    }
}
