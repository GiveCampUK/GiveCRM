using System.Collections.Generic;
using GiveCRM.Models;
using GiveCRM.Models.Search;

namespace GiveCRM.BusinessLogic
{
    public interface IMemberService
    {
        IEnumerable<Member> All();
        Member Get(int id);
        void Update(Member member);
        void Insert(Member member);
        void Delete(Member member);
        void Save(Member member); 
        IEnumerable<Member> Search(string name, string postcode, string reference);
        IEnumerable<Member> Search(string criteria);
        IEnumerable<Member> Search(IEnumerable<SearchCriteria> criteria);
        IEnumerable<Member> SearchByCampaignId(int campaignId);
        IEnumerable<Member> FromCampaignRun(int campaignId);
    }
}