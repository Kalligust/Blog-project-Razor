using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.ViewModdels;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Admin.Blogs
{
	public class EditModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		[BindProperty]
		public BlogPost BlogPost { get; set; }

		[BindProperty]
		public IFormFile FeaturedImage { get; set; }

		[BindProperty]
		public string Tags { get; set; }

		public EditModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository=blogPostRepository;
		}
		public async Task OnGet(Guid id)
		{
			BlogPost =  await blogPostRepository.GetAsync(id);

			if (BlogPost != null && BlogPost.Tags != null)
			{
				Tags = string.Join(",", BlogPost.Tags.Select(x => x.Name));
			}
		}

		public async Task<IActionResult> OnPostEdit()
		{
			try
			{
				BlogPost.Tags = new List<Tag>(Tags.Split(",").Select(x => new Tag() { Name = x.Trim() }));

				await blogPostRepository.UpdateAsync(BlogPost);
				ViewData["Notification"] = new Notification
				{
					Message = "Record updated successfully",
					Type = Enums.NotificationType.Success
				};
			}
			catch (Exception ex)
			{
				ViewData["Notification"] = new Notification
				{
					Message = "Something went wrong",
					Type = Enums.NotificationType.Error
				};
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDelete()
		{
			var wasDeleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
			if (wasDeleted)
			{
				var notification = new Notification
				{
					Message = "Blogpost was successfully deleted",
					Type = Enums.NotificationType.Success
				};

				TempData["Notification"] = JsonSerializer.Serialize(notification);

				return RedirectToPage("/Admin/Blogs/List");
			}
			return Page();
		}
	}


}
