namespace GiveCRM.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GiveCRM.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class DonationsServiceTests
    {
        [Test]
        public void GetTopDonations_ShouldReturnAListOfDonations_InDescendingOrderByAmount_WhenThereAreDonations()
        {
            var smallDonation = new Donation { Amount = 10m };
            var largeDonation = new Donation { Amount = 100m };

            var donations = new[] { smallDonation, largeDonation };

            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(donations);

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> topDonations = donationsService.GetTopDonations(5);

            CollectionAssert.AreEqual(new[] { largeDonation, smallDonation }, topDonations);
        }

        [Test]
        public void GetTopDonations_ShouldReturnAnEmptyListOfDonations_WhenThereAreNoDonations()
        {
            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(Enumerable.Empty<Donation>());

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> topDonations = donationsService.GetTopDonations(1);

            CollectionAssert.AreEqual(Enumerable.Empty<Donation>(), topDonations);
        }

        [Test]
        public void GetTopDonations_ShouldReturnTheSpecifiedNumberOfDonations_WhenThereAreDonations()
        {
            var smallDonation = new Donation { Amount = 10m };
            var largeDonation = new Donation { Amount = 100m };

            var donations = new[] { smallDonation, largeDonation };

            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(donations);

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> topDonations = donationsService.GetTopDonations(1);

            CollectionAssert.AreEqual(new[] { largeDonation }, topDonations);
        }

        [Test]
        public void GetLatestDonations_ShouldReturnAListOfDonations_InDescendingOrderByDate_WhenThereAreDonations()
        {
            var firstDonation = new Donation { Date = new DateTime(2011, 10, 21) };
            var secondDonation = new Donation { Date = new DateTime(2011, 10, 22) };

            var donations = new[] { firstDonation, secondDonation };

            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(donations);

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> latestDonations = donationsService.GetLatestDonations(5);

            CollectionAssert.AreEqual(new[] { secondDonation, firstDonation }, latestDonations);
        }

        [Test]
        public void GetLatestDonations_ShouldReturnAnEmptyListOfDonations_WhenThereAreNoDonations()
        {
            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(Enumerable.Empty<Donation>());

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> latestDonations = donationsService.GetLatestDonations(1);

            CollectionAssert.AreEqual(Enumerable.Empty<Donation>(), latestDonations);
        }

        [Test]
        public void GetLatestDonations_ShouldReturnTheSpecifiedNumberOfDonations_WhenThereAreDonations()
        {
            var firstDonation = new Donation { Date = new DateTime(2011, 10, 21) };
            var secondDonation = new Donation { Date = new DateTime(2011, 10, 22) };

            var donations = new[] { firstDonation, secondDonation };

            var donationsRepository = Substitute.For<IDonationRepository>();
            donationsRepository.GetAll().Returns(donations);

            var donationsService = new DonationsService(donationsRepository);

            IEnumerable<Donation> latestDonations = donationsService.GetLatestDonations(1);

            CollectionAssert.AreEqual(new[] { secondDonation }, latestDonations);
        }
    }
}
