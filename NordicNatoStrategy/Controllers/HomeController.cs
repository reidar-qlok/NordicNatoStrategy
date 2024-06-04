using Microsoft.AspNetCore.Mvc;
using NordicNatoStrategy.Models;
using NordicNatoStrategy.Strategy;
using System.Diagnostics;

namespace NordicNatoStrategy.Controllers
{
    public class HomeController : Controller
    {

        private readonly PersonalNumberValidatior _validator;
        public HomeController()
        {
            _validator = new PersonalNumberValidatior();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(string personalNumber, string validationType)
        {
            switch (validationType)
            {
                case "Swedish":
                    _validator.SetValidationStrategy(new SwedishPersonalNumberValidation());
                    break;
                case "Finnish":
                    _validator.SetValidationStrategy(new FinnishPersonalNumberValidation());
                    break;
                case "Norwegian":
                    _validator.SetValidationStrategy(new NorwegianPersonalNumberValidation());
                    break;
                default:
                    return BadRequest("Invalid validation type.");
            }

            bool isValid = _validator.ValidatePersonalNumber(personalNumber);
            return View("Result", isValid);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
