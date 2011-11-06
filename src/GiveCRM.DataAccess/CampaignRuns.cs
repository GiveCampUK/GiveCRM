using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class CampaignRuns
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public void Commit(int campaignId)
        {
            var memberSearchFilterRepo = new MemberSearchFilters();
            var filters = memberSearchFilterRepo.ForCampaign(campaignId).Select(msf => msf.ToSearchCriteria());

            // Use IMemberService.Search(IEnumerable<SearchCriteria> criteria)
            var results = new Search().RunWithIdOnly(filters).Select(memberId => new {CampaignId = campaignId, MemberId = memberId});

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    transaction.Campaign.UpdateById(Id : campaignId, runOn : DateTime.Today);
                    transaction.CampaignRuns.Insert(results);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            
        }
    }
}
