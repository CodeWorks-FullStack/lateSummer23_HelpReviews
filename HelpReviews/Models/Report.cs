using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpReviews.Models;

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    // NOTE this makes it so there is ALWAYS a picture, even if they didn't provide one
    public string PictureOfDisgust { get; set; } = "https://images.unsplash.com/photo-1583511655802-41f2ccc2cc8f?auto=format&fit=crop&q=60&w=800&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8eXVja3l8ZW58MHx8MHx8fDA%3D&h=300";
    public string CreatorId { get; set; }
    public int RestaurantId { get; set; }

    public Profile Creator { get; set; }

    public Restaurant Restaurant { get; set; }
}
