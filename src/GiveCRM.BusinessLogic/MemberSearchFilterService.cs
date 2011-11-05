using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class MemberSearchFilterService : IMemberSearchFilterService
    {
        private readonly IMemberSearchFilterRepository _repository;

        public MemberSearchFilterService(IMemberSearchFilterRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<MemberSearchFilter> ForCampaign(int id)
        {
            return _repository.GetByCampaignId(id);
        }

        public void Insert(MemberSearchFilter memberSearchFilter)
        {
            _repository.Insert(memberSearchFilter);
        }

        public void Delete(int id)
        {
            _repository.DeleteById(id);
        }

    }
}