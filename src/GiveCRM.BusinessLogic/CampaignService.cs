using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class CampaignService: ICampaignService
    {
        private readonly ICampaignRepository _repository;

        public CampaignService(ICampaignRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public IEnumerable<Campaign> AllOpen()
        {
            return _repository.GetAllOpen();
        }

        public IEnumerable<Campaign> AllClosed()
        {
            return _repository.GetAllClosed();
        }

        public Campaign Get(int id)
        {
            return _repository.GetById(id);
        }

        public Campaign Insert(Campaign campaign)
        {
            return _repository.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            _repository.Update(campaign);
        }

    }
}