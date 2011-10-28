using System;
using System.Collections.Generic;

using GiveCRM.DataAccess;
using GiveCRM.Models;
using GiveCRM.DummyDataGenerator.Generation;

namespace GiveCRM.DummyDataGenerator
{
    internal class Generator
    {
        private const int UpdateFromLoopFrequency = 100;

        private Campaign campaign;
        private List<Member> members;

        internal EventHandler<EventArgs<string>> Update;

        internal void GenerateMembers(int countToGenerate)
        {
            OnUpdate("Generating members");
            DateTime startTime = DateTime.Now;
            MemberGenerator generator = new MemberGenerator();
            
            if (members == null)
            {
                members = new List<Member>();
            }
            members.Clear();
            members.Capacity = countToGenerate;

            List<Member> newMembers = generator.Generate(countToGenerate);
            string generateMessaged = string.Format("{0} members generated", newMembers.Count);
            OnUpdate(generateMessaged);

            SaveMembers(newMembers);

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            string finalMessage = string.Format("{0} members saved in {1}", newMembers.Count, ShowDuration(elapsedTime));
            OnUpdate(finalMessage);
        }

        private void SaveMembers(IList<Member> newMembers)
        {
            Members membersDb = new Members();
            for (int index = 0; index < newMembers.Count; index++)
            {
                Member saved = membersDb.Insert(newMembers[index]);
                this.members.Add(saved);

                if (index % UpdateFromLoopFrequency == 0)
                {
                    string generateMessaged = string.Format("{0} members saved", index);
                    OnUpdate(generateMessaged);                    
                }
            }
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

        internal void GenerateDonations()
        {
            OnUpdate("Generating donations");
            DateTime startTime = DateTime.Now;
            DonationsGenerator generator = new DonationsGenerator(campaign, members);
            IList<Donation> newDonations = generator.Generate();
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
                handler(this, new EventArgs<string>(message));
            }
        }
    }
}
