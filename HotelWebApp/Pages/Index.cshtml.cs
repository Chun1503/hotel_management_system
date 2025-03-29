using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string SearchQuery { get; set; }

        [BindProperty]
        public string Location { get; set; }

        [BindProperty]
        public string SubscribeEmail { get; set; }

        public List<Category> Categories { get; set; }
        public List<Feature> Features { get; set; }
        public List<ExploreItem> ExploreItems { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Statistic> Statistics { get; set; }
        public List<BlogPost> BlogPosts { get; set; }

        public void OnGet()
        {
            InitializeData();
        }

        public IActionResult OnPostSearch()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Process search and redirect to search results
            return RedirectToPage("/SearchResults", new
            {
                query = SearchQuery,
                location = Location
            });
        }

        public IActionResult OnPostSubscribe()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Process subscription (save to database, send email, etc.)
            TempData["SubscribeSuccess"] = "Thank you for subscribing!";
            return RedirectToPage();
        }

        private void InitializeData()
        {
            // Sample data initialization
            Categories = new List<Category>
            {
                new Category { Name = "resturent", Icon = "flaticon-restaurant", ListingCount = 150 },
                new Category { Name = "destination", Icon = "flaticon-travel", ListingCount = 214 },
                new Category { Name = "hotels", Icon = "flaticon-building", ListingCount = 185 },
                new Category { Name = "healthcaree", Icon = "flaticon-pills", ListingCount = 200 },
                new Category { Name = "automotion", Icon = "flaticon-transport", ListingCount = 120 }
            };

            Features = new List<Feature>
            {
                new Feature {
                    Title = "choose <span> what to</span> do",
                    Icon = "flaticon-lightbulb-idea",
                    Description = "Lorem ipsum dolor sit amet, consecte adipisicing elit, sed do eiusmod tempor incididunt ut laboremagna aliqua.",
                    Link = "#"
                },
                new Feature {
                    Title = "find <span> what you want</span>",
                    Icon = "flaticon-networking",
                    Description = "Lorem ipsum dolor sit amet, consecte adipisicing elit, sed do eiusmod tempor incididunt ut laboremagna aliqua.",
                    Link = "#"
                },
                new Feature {
                    Title = "explore <span> amazing</span> place",
                    Icon = "flaticon-location-on-road",
                    Description = "Lorem ipsum dolor sit amet, consecte adipisicing elit, sed do eiusmod tempor incididunt ut laboremagna aliqua.",
                    Link = "#"
                }
            };

            ExploreItems = new List<ExploreItem>
            {
                new ExploreItem {
                    Title = "tommy helfinger bar",
                    Image = "e1.jpg",
                    Tag = "best rated",
                    Theme = 1,
                    Rating = "5.0",
                    RatingCount = "10",
                    PriceRange = "5$-300$",
                    Category = "resturent",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = false,
                    Link = "#"
                },
                new ExploreItem {
                    Title = "swim and dine resort",
                    Image = "e2.jpg",
                    Tag = "featured",
                    Theme = 2,
                    Rating = "4.5",
                    RatingCount = "8",
                    PriceRange = "50$-500$",
                    Category = "hotel",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = true,
                    Link = "#"
                },
                new ExploreItem {
                    Title = "europe tour",
                    Image = "e3.jpg",
                    Tag = "best rated",
                    Theme = 3,
                    Rating = "5.0",
                    RatingCount = "15",
                    PriceRange = "5k$-10k$",
                    Category = "destination",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = false,
                    Link = "#"
                },
                new ExploreItem {
                    Title = "banglow with swiming pool",
                    Image = "e4.jpg",
                    Tag = "most view",
                    Theme = 4,
                    Rating = "5.0",
                    RatingCount = "10",
                    PriceRange = "10k$-15k$",
                    Category = "real estate",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = false,
                    Link = "#"
                },
                new ExploreItem {
                    Title = "vintage car expo",
                    Image = "e5.jpg",
                    Tag = "featured",
                    Theme = 2,
                    Rating = "4.2",
                    RatingCount = "8",
                    PriceRange = "500$-1200$",
                    Category = "automotion",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = true,
                    Link = "#"
                },
                new ExploreItem {
                    Title = "thailand tour",
                    Image = "e6.jpg",
                    Tag = "best rated",
                    Theme = 5,
                    Rating = "5.0",
                    RatingCount = "15",
                    PriceRange = "5k$-10k$",
                    Category = "destination",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua....",
                    IsOpen = false,
                    Link = "#"
                }
            };

            Testimonials = new List<Testimonial>
            {
                new Testimonial {
                    Name = "Tom Leakar",
                    Image = "c1.png",
                    Location = "london, UK",
                    Rating = 5,
                    Comment = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis eaque."
                },
                new Testimonial {
                    Name = "monirul islam",
                    Image = "c2.png",
                    Location = "london, UK",
                    Rating = 5,
                    Comment = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis eaque."
                },
                new Testimonial {
                    Name = "Shohrab Hossain",
                    Image = "c3.png",
                    Location = "london, UK",
                    Rating = 5,
                    Comment = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis eaque."
                },
                new Testimonial {
                    Name = "Tom Leakar",
                    Image = "c4.png",
                    Location = "london, UK",
                    Rating = 5,
                    Comment = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis eaque."
                }
            };

            Statistics = new List<Statistic>
            {
                new Statistic { Label = "listings", Value = "90", Unit = "K+" },
                new Statistic { Label = "listing categories", Value = "40", Unit = "k+" },
                new Statistic { Label = "visitors", Value = "65", Unit = "k+" },
                new Statistic { Label = "happy clients", Value = "50", Unit = "k+" }
            };

            BlogPosts = new List<BlogPost>
            {
                new BlogPost {
                    Title = "How to find your Desired Place more quickly",
                    Image = "b1.jpg",
                    Author = "admin",
                    Date = "march 2018",
                    Summary = "Lorem ipsum dolor sit amet, consectetur de adipisicing elit, sed do eiusmod tempore incididunt ut labore et dolore magna.",
                    Link = "#"
                },
                new BlogPost {
                    Title = "How to find your Desired Place more quickly",
                    Image = "b2.jpg",
                    Author = "admin",
                    Date = "march 2018",
                    Summary = "Lorem ipsum dolor sit amet, consectetur de adipisicing elit, sed do eiusmod tempore incididunt ut labore et dolore magna.",
                    Link = "#"
                },
                new BlogPost {
                    Title = "How to find your Desired Place more quickly",
                    Image = "b3.jpg",
                    Author = "admin",
                    Date = "march 2018",
                    Summary = "Lorem ipsum dolor sit amet, consectetur de adipisicing elit, sed do eiusmod tempore incididunt ut labore et dolore magna.",
                    Link = "#"
                }
            };
        }
    }

    // Model classes
    public class Category
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int ListingCount { get; set; }
    }

    public class Feature
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public class ExploreItem
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public int Theme { get; set; }
        public string Rating { get; set; }
        public string RatingCount { get; set; }
        public string PriceRange { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public string Link { get; set; }
    }

    public class Testimonial
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public class Statistic
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class BlogPost
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Summary { get; set; }
        public string Link { get; set; }
    }
}
