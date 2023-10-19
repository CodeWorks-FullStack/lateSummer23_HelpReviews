namespace HelpReviews.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
    public int Visits { get; set; }
    public int ReportCount { get; set; }
    public bool? IsShutdown { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
}