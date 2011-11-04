using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic.ExcelImport
{
    internal class MemberFactory : IMemberFactory
    {
        #region Implementation of IMemberFactory

        public Member CreateMember(IDictionary<string, object> memberData)
        {
            return DictionaryToMember.ToMember(memberData);
        }

        #endregion

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