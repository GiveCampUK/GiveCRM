namespace GiveCRM.DataAccess
{
    using System;
    using System.Linq;

    using Simple.Data;

    public class CampaignRuns
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        public void Commit(int campaignId)
        {
            var memberSearchFilterRepo = new MemberSearchFilters();
            var filters = memberSearchFilterRepo.ForCampaign(campaignId).Select(msf => msf.ToSearchCriteria());
            var results = new Search().RunWithIdOnly(filters).Select(memberId => new {CampaignId = campaignId, MemberId = memberId});

            using (var transaction = db.BeginTransaction())
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
