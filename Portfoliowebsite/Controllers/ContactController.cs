using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        const int MINIMUM_COMPLETION_SECONDS = 2;
        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index()
        {
            HttpContext.Session.SetString("ContactFormStart", DateTime.UtcNow.Ticks.ToString());
            return View(new ContactModel());
        }

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

            if (!FormSubmittedInNormalTime())
            {
                return BadRequest("Inzending tegengehouden door spampreventie");
            }

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

        private bool FormSubmittedInNormalTime()
        {
            var ticks = HttpContext.Session.GetString("ContactFormStart");

            if (ticks != null)
            {
                var start = new DateTime(long.Parse(ticks), DateTimeKind.Utc);
                var elapsed = DateTime.UtcNow - start;

                if (elapsed.TotalSeconds < MINIMUM_COMPLETION_SECONDS)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
