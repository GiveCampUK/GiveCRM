using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    public class CampaignService: ICampaignService
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly IMemberSearchFilterRepository memberSearchFilterRepository;
        private readonly IMemberService memberService;

        public CampaignService(ICampaignRepository campaignRepository, IMemberSearchFilterRepository memberSearchFilterRepository, IMemberService memberService)
        {
            if (campaignRepository == null)
            {
                throw new ArgumentNullException("campaignRepository");
            }

            if (memberSearchFilterRepository == null)
            {
                throw new ArgumentNullException("memberSearchFilterRepository");
            }

            if (memberService == null)
            {
                throw new ArgumentNullException("memberService");
            }

            this.campaignRepository = campaignRepository;
            this.memberSearchFilterRepository = memberSearchFilterRepository;
            this.memberService = memberService;
        }

        public IEnumerable<Campaign> GetAllOpen()
        {
            return campaignRepository.GetAllOpen();
        }

        public IEnumerable<Campaign> GetAllClosed()
        {
            return campaignRepository.GetAllClosed();
        }

        public Campaign Get(int id)
        {
            return campaignRepository.GetById(id);
        }

        public Campaign Insert(Campaign campaign)
        {
            return campaignRepository.Insert(campaign);
        }

        public void Update(Campaign campaign)
        {
            campaignRepository.Update(campaign);
        }

        public void Commit(int campaignId)
        {
            var filters = memberSearchFilterRepository.GetByCampaignId(campaignId).Select(msf => msf.ToSearchCriteria());
            var filteredMembers = memberService.Search(filters);
            
        }
    }
}