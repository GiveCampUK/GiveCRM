namespace GiveCRM.Web.Models.Search
{
    public enum SearchOperator
    {
        EqualTo,
        NotEqualTo,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        StartsWith,
        EndsWith,
        Contains,
        AnyOf,
        AllOf
    }

    public enum SearchFieldType
    {
        Text,
        Number,
        Boolean,
        Date,
        SelectList
    }

    public class SearchCriteria
    {
        public string Field { get; set; }
        public SearchFieldType Type { get; set; }
        public SearchOperator Operator { get; set; }
        public string Value { get; set; }
        public string DatabaseTableName { get; set; }
        public string DatabaseColumnName { get; set; }
    }
}