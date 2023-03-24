using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.ViewModdels;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Admin.Blogs
{
	public class AddModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		[BindProperty]
		public AddBlogPost AddBlogPostRequest { get; set; }

		[BindProperty]
		public IFormFile FeaturedImage { get; set; }

		[BindProperty]
		public string Tags { get; set; }

		public AddModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository=blogPostRepository;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			var blogPost = new BlogPost()
			{
				Heading = AddBlogPostRequest.Heading,
				PageTitle= AddBlogPostRequest.PageTitle,
				Content= AddBlogPostRequest.Content,
				ShortDescription= AddBlogPostRequest.ShortDescription,
				FeaturedImageUrl= AddBlogPostRequest.FeaturedImageUrl,
				UrlHandle= AddBlogPostRequest.UrlHandle,
				PublishedDate= AddBlogPostRequest.PublishedDate,
				Author= AddBlogPostRequest.Author,
				Visible= AddBlogPostRequest.Visible,
				Tags = new List<Tag>(Tags.Split(",").Select(x => new Tag() { Name = x.Trim() }))
			};

			await blogPostRepository.AddAsync(blogPost);

			var notification = new Notification
			{
				Message = "Blog Post successfully Added",
				Type = Enums.NotificationType.Success
			};

			TempData["Notification"] = JsonSerializer.Serialize(notification);


			return RedirectToPage("/Admin/blogs/list");
		}
	}
}
