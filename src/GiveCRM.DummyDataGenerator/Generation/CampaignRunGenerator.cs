namespace GiveCRM.DummyDataGenerator.Generation
{
    using System.Linq;
    using GiveCRM.BusinessLogic;
    using GiveCRM.DataAccess;

    internal class CampaignRunGenerator
    {
        private readonly IDatabaseProvider databaseProvider;
        private readonly MemberService memberService;

        public CampaignRunGenerator(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
            memberService = new MemberService(new Members(databaseProvider), new MemberSearchFilters(databaseProvider),
                                              new SearchQueryService(databaseProvider));
        }

        internal void GenerateCampaignRun(int campaignId)
        {
            var campaignMembers = this.memberService.SearchByCampaignId(campaignId);
            var memberCampaignMemberships = campaignMembers.Select(member => new {CampaignId = campaignId, MemberId = member.Id}).ToList();

            if (memberCampaignMemberships.Any())
            {
                databaseProvider.GetDatabase().CampaignRuns.Insert(memberCampaignMemberships);
            }
        }
    }
}