using System;
using System.Collections.Generic;
using System.Diagnostics;
using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.DummyDataGenerator.Generation;
using System.Linq;

namespace GiveCRM.DummyDataGenerator
{
/*
    internal class Generator
    {
        private const int UpdateFromLoopFrequency = 100;

        private Campaign campaign;
        private List<Member> members;

        internal Action<string> Update;

        public Generator()
        {
            members = new List<Member>();
        }

        internal void GenerateMembers(int countToGenerate)
        {
            OnUpdate("Generating members...");
            members.Clear();
            members.Capacity = countToGenerate;

            MemberGenerator generator = new MemberGenerator();
            List<Member> newMembers = generator.Generate(countToGenerate);
            string generateMessaged = string.Format("{0} members generated", newMembers.Count);
            OnUpdate(generateMessaged);

            SaveMembers(newMembers);
        }

        private void SaveMembers(ICollection<Member> newMembers)
        {
            OnUpdate("Saving members...");
            Members membersDb = new Members();
            int lastLoggedPercent = 0;
            Stopwatch timer = Stopwatch.StartNew();

            foreach (var memberInfo in newMembers.Select((member, index) => new {Member = member, Index = index}))
            {
                Member saved = membersDb.Insert(memberInfo.Member);
                this.members.Add(saved);

                int percentComplete = Convert.ToInt32((memberInfo.Index * 1.0 / newMembers.Count) * 100);

                if (percentComplete % 10 == 0 && lastLoggedPercent != percentComplete)
                {
                    string generatedMessage = string.Format("{0}% complete...", percentComplete);
                    OnUpdate(generatedMessage);
                    lastLoggedPercent = percentComplete;
                }
            }

            timer.Stop();
            string message = string.Format("{0} members saved in {1}", newMembers.Count, ShowDuration(timer.Elapsed));
            OnUpdate(message);
        }

        internal void LoadMembers()
        {
            OnUpdate("Loading members");
            DateTime startTime = DateTime.Now;

            Members membersDb = new Members();
            members = new List<Member>(membersDb.All());

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            string finalMessage = string.Format("{0} members loaded in {1}", members.Count, ShowDuration(elapsedTime));
            OnUpdate(finalMessage);
        }

        internal void GenerateCampaign()
        {
            OnUpdate("Generating campaign");
            CampaignGenerator generator = new CampaignGenerator();
            campaign = generator.Generate();

            Campaigns campaignDb = new Campaigns();
            campaign = campaignDb.Insert(campaign);

            string finalMessage = "Generated campaign " + campaign;
            OnUpdate(finalMessage);
        }

        internal void GenerateDonations(int minAmount, int maxAmount, int donationCountMax)
        {
            if ((members == null) || (members.Count == 0))
            {
                OnUpdate("Cannot generate donations before members have been loaded or generated");
                return;
            }

            OnUpdate("Generating donations");
            DateTime startTime = DateTime.Now;
            DonationsGenerator generator = new DonationsGenerator(campaign, members);
            IList<Donation> newDonations = generator.Generate(minAmount, maxAmount, donationCountMax);
            string generateMessaged = string.Format("{0} donations generated", newDonations.Count);
            OnUpdate(generateMessaged);

            SaveDonations(newDonations);

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            string finalMessage = string.Format("{0} donations inserted on campaign {1} in {2}", 
                newDonations.Count, campaign.Name, ShowDuration(elapsedTime));
            OnUpdate(finalMessage);

        }

        private void SaveDonations(IList<Donation> newDonations)
        {
            Donations donationDb = new Donations();

            for (int index = 0; index < newDonations.Count; index++)
            {
                donationDb.Insert(newDonations[index]);

                if (index % UpdateFromLoopFrequency == 0)
                {
                    string generateMessaged = string.Format("{0} donations saved", index);
                    OnUpdate(generateMessaged);
                }
            }
        }

        private string ShowDuration(TimeSpan timeSpan)
        {
            TimeSpan oneMinute = new TimeSpan(0, 1, 0);
            
            if (timeSpan >= oneMinute)
            {
                return timeSpan.ToString(@"m\:ss\.ff") + " minutes";
            }
            
            return timeSpan.ToString(@"s\.ff") + " seconds"; 
        }

        private void OnUpdate(string message)
        {
            var handler = Update;

            if (handler != null)
            {
                handler(message);
            }
        }
    }
 */
}
