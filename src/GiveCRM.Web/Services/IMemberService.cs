using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> All();
        Member Get(int id);
        void Update(Member member);
        void Insert(Member member);
        IEnumerable<Member> Search(string name, string postcode, string reference);
        IEnumerable<Member> Search(string criteria);
        IEnumerable<Member> FromCampaignRun(int campaignId);
    }
}