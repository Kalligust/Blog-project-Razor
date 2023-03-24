using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.ViewModdels;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository=blogPostRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if(notificationJson != null)
            {
                ViewData["notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);    
            }
            BlogPosts =  (await blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
