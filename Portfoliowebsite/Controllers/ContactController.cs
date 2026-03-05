using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index() => View(new ContactModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactRequest(ContactModel model, CancellationToken ct)
        {
            // Honeypot check
            if (!string.IsNullOrEmpty(model.Website))
            {
                return BadRequest("Ongeldige aanvraag");
            }

            if (!ModelState.IsValid)
                return View("Index", model);

            try
            {
                await _email.SendAsync(model.Name, model.Email, model.Subject, model.Message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Er ging iets mis bij het versturen. Probeer het later opnieuw.");
                return View("Index", model);
            }

            TempData["ThanksName"] = model.Name;
            TempData["ThanksEmail"] = model.Email;
            TempData["ThanksMessage"] = model.Message;

            return RedirectToAction(nameof(Thanks));
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
