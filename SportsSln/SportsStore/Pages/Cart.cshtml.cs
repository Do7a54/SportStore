using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;
namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository repository;
        public CartModel(IStoreRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;    // Added Page 219
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
           // Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();  // Modefied Page 219
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }   // Added Page 219

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.ProductID == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }   // Added Page 232 ----efines an HTML form. The handler method that will receive the request is specified with the asp-page-handler tag helper attribut

        //public IActionResult OnPost(long productId, string returnUrl)
        //{
        //    Product? product = repository.Products
        //    .FirstOrDefault(p => p.ProductID == productId);
        //    if (product != null)
        //    {
        //        Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        //        Cart.AddItem(product, 1);
        //        HttpContext.Session.SetJson("cart", Cart);
        //    }
        //    return RedirectToPage(new { returnUrl = returnUrl });
        //}
    }
}