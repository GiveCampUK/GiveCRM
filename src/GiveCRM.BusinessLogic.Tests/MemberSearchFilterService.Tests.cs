using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.BusinessLogic.Tests
{
    [TestFixture]
    public class MemberSearchFilterServiceTests
    {
        private readonly MemberSearchFilter citySearchFilter = new MemberSearchFilter { Id = 1, CampaignId = 1, DisplayName = "City" };
        
        [Test]
        public void ForCampaign_ShouldReturnTheMemberSearchFiltersForTheSpecifiedCampaign_WhenThereAreSome()
        {
            const int campaignId = 1;

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            memberSearchFilterRepository.GetByCampaignId(campaignId).Returns(new[] { citySearchFilter });
            
            var memberSearchFilterService = new MemberSearchFilterService(memberSearchFilterRepository);

            var campaignFilters = memberSearchFilterService.ForCampaign(campaignId);

            CollectionAssert.AreEqual(new[] { citySearchFilter }, campaignFilters);
        }

        [Test]
        public void ForCampaign_ShouldReturnAnEmptyList_WhenThereAreNoMemberSearchFiltersForTheSpecifiedCampaign()
        {
            const int campaignId = 3;

            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            memberSearchFilterRepository.GetByCampaignId(campaignId).Returns(Enumerable.Empty<MemberSearchFilter>());

            var memberSearchFilterService = new MemberSearchFilterService(memberSearchFilterRepository);

            var campaignFilters = memberSearchFilterService.ForCampaign(campaignId);

            CollectionAssert.AreEqual(Enumerable.Empty<MemberSearchFilter>(), campaignFilters);
        }
    }
}
