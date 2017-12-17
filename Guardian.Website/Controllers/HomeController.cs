using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guardian.Common.Interfaces;
using Guardian.Core;
using Guardian.Website.EntityFramework;
using Guardian.Website.Models;

namespace Guardian.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Validate(Document document)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                IEnumerable<IValidation> validations = dbContext.Validations
                    .Where(i => i.ActiveFlag)
                    .ToList();
                IEnumerable<IValidationCondition> validationConditions = dbContext.ValidationConditions
                    .Where(i => i.ActiveFlag)
                    .ToList();

                Validator validator = new Validator();
                IEnumerable<ValidationError> validationResults = validator.Validate(document, validations,
                    validationConditions);
                return Json(validationResults, JsonRequestBehavior.AllowGet);
            }
        }
    }
}