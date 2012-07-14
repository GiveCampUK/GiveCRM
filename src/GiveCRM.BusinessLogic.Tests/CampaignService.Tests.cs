namespace GiveCRM.BusinessLogic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class TestCampaignService
    {
        [Test]
        public void GetAllOpen_ShouldReturnAListOfOpenCampaigns_WhenThereAreOpenCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = new[]
                                                          {
                                                              new Campaign { IsClosed = "N" },
                                                              new Campaign { IsClosed = "N" }
                                                          };
            var campaignRepository = GetCampaignRepositoryForGetAllOpen(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var openCampaigns = campaignService.GetAllOpen();

            CollectionAssert.AreEqual(expectedCampaigns, openCampaigns);
        }

        [Test]
        public void GetAllOpen_ShouldReturnAnEmptyList_WhenThereAreNoOpenCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = Enumerable.Empty<Campaign>();
            var campaignRepository = GetCampaignRepositoryForGetAllOpen(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var openCampaigns = campaignService.GetAllOpen();

            CollectionAssert.AreEqual(expectedCampaigns, openCampaigns);
        }

        [Test]
        public void GetAllOpen_ShouldReturnAnEmptyList_WhenThereAreNoCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = Enumerable.Empty<Campaign>();
            var campaignRepository = GetCampaignRepositoryForGetAllOpen(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var openCampaigns = campaignService.GetAllOpen();

            CollectionAssert.AreEqual(expectedCampaigns, openCampaigns);
        }

        [Test]
        public void GetAllClosed_ShouldReturnAListOfClosedCampaigns_WhenThereAreClosedCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = new[]
                                                          {
                                                              new Campaign { IsClosed = "Y" },
                                                              new Campaign { IsClosed = "Y" }
                                                          };
            var campaignRepository = GetCampaignRepositoryForGetAllClosed(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var closedCampaigns = campaignService.GetAllClosed();

            CollectionAssert.AreEqual(expectedCampaigns, closedCampaigns);
        }

        [Test]
        public void GetAllClosed_ShouldReturnAnEmptyList_WhenThereAreNoClosedCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = Enumerable.Empty<Campaign>();
            var campaignRepository = GetCampaignRepositoryForGetAllClosed(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var closedCampaigns = campaignService.GetAllClosed();

            CollectionAssert.AreEqual(expectedCampaigns, closedCampaigns);
        }

        [Test]
        public void GetAllClosed_ShouldReturnAnEmptyList_WhenThereAreNoCampaigns()
        {
            IEnumerable<Campaign> expectedCampaigns = Enumerable.Empty<Campaign>();
            var campaignRepository = GetCampaignRepositoryForGetAllClosed(expectedCampaigns);
            CampaignService campaignService = GetCampaignService(campaignRepository);

            var closedCampaigns = campaignService.GetAllClosed();

            CollectionAssert.AreEqual(expectedCampaigns, closedCampaigns);
        }

        [Test]
        public void Get_ShouldReturnTheCampaignWithTheSpecifedId()
        {
            const int expectedCampaignId = 4;
            var campaignRepository = GetCampaignRepositoryForGetById(new Campaign { Id = expectedCampaignId });
            var campaignService = GetCampaignService(campaignRepository);

            Campaign actualCampaign = campaignService.Get(expectedCampaignId);

            Assert.AreEqual(expectedCampaignId, actualCampaign.Id);
        }

        [Test]
        public void Insert_ShouldReturnTheInsertedCampaignWithANewId()
        {
            const int expectedCampaignId = 20;
            var insertedCampaign = new Campaign { Name = "Test Campaign", IsClosed = "N" };
            var expectedCampaign = new Campaign { Id = expectedCampaignId, Name = "Test Campaign", IsClosed = "N" };

            var campaignRepository = GetCampaignRepositoryForInsert(insertedCampaign, expectedCampaign);
            var campaignService = GetCampaignService(campaignRepository);

            Campaign actualCampaign = campaignService.Insert(insertedCampaign);

            Assert.AreEqual(expectedCampaignId, actualCampaign.Id);
        }

        private static ICampaignRepository GetCampaignRepositoryForInsert(Campaign insertedCampaign, Campaign expectedCampaign)
        {
            var campaignRepository = Substitute.For<ICampaignRepository>();
            campaignRepository.Insert(insertedCampaign).Returns(expectedCampaign);
            return campaignRepository;
        }

        private static ICampaignRepository GetCampaignRepositoryForGetById(Campaign expectedCampaign)
        {
            var campaignRepository = Substitute.For<ICampaignRepository>();
            campaignRepository.GetById(4).Returns(expectedCampaign);
            return campaignRepository;
        }

        private CampaignService GetCampaignService(ICampaignRepository campaignRepository)
        {
            var memberSearchFilterRepository = Substitute.For<IMemberSearchFilterRepository>();
            var memberService = Substitute.For<IMemberService>();

            return new CampaignService(campaignRepository, memberSearchFilterRepository, memberService);
        }

        private static ICampaignRepository GetCampaignRepositoryForGetAllOpen(IEnumerable<Campaign> expectedCampaigns)
        {
            var campaignRepository = Substitute.For<ICampaignRepository>();
            campaignRepository.GetAllOpen().Returns(expectedCampaigns);
            return campaignRepository;
        }

        private static ICampaignRepository GetCampaignRepositoryForGetAllClosed(IEnumerable<Campaign> expectedCampaigns)
        {
            var campaignRepository = Substitute.For<ICampaignRepository>();
            campaignRepository.GetAllClosed().Returns(expectedCampaigns);
            return campaignRepository;
        }
    }
}
