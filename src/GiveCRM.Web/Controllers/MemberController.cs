using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.Web.Models.Members;

namespace GiveCRM.Web.Controllers
{
    public class MemberController : Controller
    {
        private const int MaxResults = 25;

        private Members _membersDb = new Members();
        private Donations _donationsDb = new Donations();
        private Campaigns _campaignsDb = new Campaigns();

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Add()
        {
            return View(new Member() { PhoneNumbers = new List<PhoneNumber>() });
        }

        public ActionResult Import()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = _membersDb.Get(id); 
            if(model.PhoneNumbers == null) 
               model.PhoneNumbers = new List<PhoneNumber>(); 
            return View(viewName: "Add", model: model);
        }

        public ActionResult Delete(int id)
        {
            var member = _membersDb.Get(id);

            member.AddressLine1 = "deleted";
            member.AddressLine2 = "deleted";
            member.EmailAddress = "deleted";
            member.FirstName = "deleted";
            member.LastName = "deleted";
            
            member.Archived = true;

            _membersDb.Update(member);

            return RedirectToAction("Index");
        }

        public ActionResult Donate(int id)
        {
            ViewBag.MemberName = GetFormattedName(_membersDb.Get(id));

            ViewBag.Campaigns = _campaignsDb.AllOpen().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return View(new Donation { MemberId = id });
        }

        public ActionResult SaveDonation(Donation donation)
        {
            _donationsDb.Insert(donation);

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string name, string postcode, string reference)
        {
            var results = _membersDb
                .All()
                .Where(member => 
                    !member.Archived &&
                    (name == string.Empty || NameSearch(member, name.ToLower())) &&
                    (postcode == string.Empty || PostcodeSearch(member, postcode.ToLower())) &&
                    (reference == string.Empty || ReferenceSearch(member, reference.ToLower())));

            return View(new MemberSearchViewModel { Results = results.Take(MaxResults), AreMore = results.Count() > MaxResults });
        }

        public ActionResult TopDonors()
        {
            var members = _membersDb.All().OrderByDescending(m => m.TotalDonations).Take(5);

            return View("MembersList", members);
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

        private string GetFormattedName(Member member)
        {
            return string.Format("{0} {1} {2}", member.Salutation, member.FirstName, member.LastName);
        }

        #endregion
    }
}
