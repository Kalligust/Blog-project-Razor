using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models.Domain;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Blog
{
	public class DetailsModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPost BlogPost { get; set; }

		public DetailsModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository=blogPostRepository;
		}

		public async Task<IActionResult> OnGet(string urlHandle)
		{
			BlogPost = await blogPostRepository.GetAsync(urlHandle);
			return Page();
		}
	}
}
