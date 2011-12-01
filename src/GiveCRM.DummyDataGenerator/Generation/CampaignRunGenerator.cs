using System;
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
            //TODO: remove the try/catch once the conversion error is fixed
            try
            {
                var campaignMembers = this.memberService.SearchByCampaignId(campaignId);
                var memberCampaignMemberships = campaignMembers.Select(member => new {CampaignId = campaignId, MemberId = member.Id});
                db.CampaignRuns.Insert(memberCampaignMemberships);
            }
            catch (Exception)
            {
                // this is a temporary fix until the simple data error is fixed ("Conversion failed when converting date and/or time from character string.")
            }
        }
    }
}