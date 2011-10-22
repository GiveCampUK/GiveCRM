using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Member;

namespace GiveCRM.Web.Controllers
{
    public class MemberController : Controller
    {
        private const int MaxResults = 25;

        private Members _membersDb = new Members();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View(new Member());
        }

        public ActionResult Import()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(viewName: "Add", model: _membersDb.Get(id));
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Donate(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Save(Member member)
        {
            if (member.Id == 0)
            {
                _membersDb.Insert(member);
            }
            else
            {
                _membersDb.Update(member);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Search(string name, string postcode, string reference)
        {
            var results = _membersDb
                .All()
                .Where(member => 
                    (name == string.Empty || NameSearch(member, name.ToLower())) &&
                    (postcode == string.Empty || PostcodeSearch(member, postcode.ToLower())) &&
                    (reference == string.Empty || ReferenceSearch(member, reference.ToLower())));

            return View(new MemberSearchViewModel { Results = results.Take(MaxResults), AreMore = results.Count() > MaxResults });
        }
        
        #region helper methods

        // todo: move these out!

        private bool PostcodeSearch(Member member, string criteria)
        {
            return member.PostalCode == null ? false : member.PostalCode.ToLower().Replace(" ", "").Contains(criteria.Replace(" ", ""));
        }

        private bool ReferenceSearch(Member member, string criteria)
        {
            // TODO: We think member is missing a reference field
            return member.Reference.ToLower().Contains(criteria);
        }

        private bool NameSearch(Member member, string criteria)
        {
            return GetForenameSurname(member).Contains(criteria) || GetSurnameForename(member).Contains(criteria) || GetInitialSurname(member).Contains(criteria);
        }

        private string GetForenameSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName, member.LastName).ToLower();
        }

        private string GetSurnameForename(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.LastName, member.FirstName).ToLower();
        }

        private string GetInitialSurname(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName.Substring(0, 1), member.LastName).ToLower();
        }

        #endregion
    }
}
