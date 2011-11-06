using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.Web.Services.ExcelImport
{
    public class MemberFactory : IMemberFactory
    {
        public Member CreateMember(IDictionary<string, object> memberData)
        {
            return DictionaryToMember.ToMember(memberData);
        }

        internal static class DictionaryToMember
        {
            internal static Member ToMember(IDictionary<string, object> source)
            {
                var member = Activator.CreateInstance<Member>();
                if (source == null)
                {
                    return member;
                }

                foreach (var property in source)
                {
                    member.GetType().GetProperty(property.Key).SetValue(member, property.Value, null);
                }

                return member;
            }
        }
    }
}