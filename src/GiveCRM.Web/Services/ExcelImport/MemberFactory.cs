using System.Collections.Generic;
using AutoMapper;
using GiveCRM.Models;

namespace GiveCRM.Web.Services.ExcelImport
{
    public class MemberFactory : IMemberFactory
    {
        #region Implementation of IMemberFactory

        public Member CreateMember(IDictionary<string, object> memberData)
        {
            Mapper.CreateMap<IDictionary<string, object>, Member>()
                  .ForAllMembers(opt => opt.ResolveUsing(member => member.Keys));

            return Mapper.Map<IDictionary<string, object>, Member>(memberData);
        }

        #endregion
    }
}