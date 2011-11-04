﻿namespace GiveCRM.Web.Models.Members
{
    public class MemberSearchViewModel
    {
        public int? Page { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string Reference { get; set; }
        public PagedMemberListViewModel Results { get; set; }
        public string SearchButton { get; set; }
    }
}