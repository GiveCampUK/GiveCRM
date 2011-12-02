using System.Linq;
using Simple.Data;
using GiveCRM.BusinessLogic;
using GiveCRM.DataAccess;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class CampaignRunGenerator
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");
        private readonly MemberService memberService = new MemberService(new Members(), new MemberSearchFilters(), new SearchQueryService());

        internal void GenerateCampaignRun(int campaignId)
        {
            var campaignMembers = this.memberService.SearchByCampaignId(campaignId);
            var memberCampaignMemberships = campaignMembers.Select(member => new {CampaignId = campaignId, MemberId = member.Id}).ToList();

            if (memberCampaignMemberships.Any())
            {
                db.CampaignRuns.Insert(memberCampaignMemberships);
            }
        }
    }
}