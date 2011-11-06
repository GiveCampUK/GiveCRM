using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public class DonationsService : IDonationsService
    {
        private readonly IDonationRepository _repository;

        public DonationsService(IDonationRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public IEnumerable<Donation> GetTopDonations()
        {
            var donations = _repository.GetAll().OrderByDescending(d => d.Amount).Take(5);
            return donations;
        }

        public IEnumerable<Donation> GetLatestDonations()
        {
            var donations = _repository.GetAll().OrderByDescending(d => d.Date).Take(5);
            return donations;
        }

        public void QuickDonation(Donation donation)
        {
            _repository.Insert(donation);
        }
    }


}