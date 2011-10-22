using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;

namespace GiveCRM.Web.Controllers
{
    public class MemberController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name, string postcode, string reference)
        {
            var membersDb = new Members();

            var results = membersDb
                .All()
                .Where(member => 
                    NameSearch(member, name) || 
                    PostcodeSearch(member, postcode) || 
                    ReferenceSearch(member, reference));

            return View(results);
        }


        #region helper methods

        // todo: move these out!

        private bool PostcodeSearch(Member member, string criteria)
        {
            return member.PostalCode.Replace(" ", "").Contains(criteria.Replace(" ", ""));
        }

        private bool ReferenceSearch(Member member, string criteria)
        {
            // TODO: We think member is missing a reference field
            return member.Id.ToString().Contains(criteria);
        }

        private bool NameSearch(Member member, string criteria)
        {
            return GetForenameSurname(member).Contains(criteria) || GetSurnameForename(member).Contains(criteria) || GetInitialSurname(member).Contains(criteria);
        }

        private string GetForenameSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName, member.LastName);
        }

        private string GetSurnameForename(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.LastName, member.FirstName);
        }

        private string GetInitialSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName.Substring(0, 1), member.LastName);
        }

        #endregion
    }
}
