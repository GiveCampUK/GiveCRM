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
}