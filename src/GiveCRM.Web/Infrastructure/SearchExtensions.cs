namespace GiveCRM.Web.Infrastructure
{
    using System.Text.RegularExpressions;
    using System.Threading;
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Properties;

    internal static class SearchExtensions
    {
        internal static string ToFriendlyDisplayString(this SearchCriteria searchCriteria)
        {
            return string.Format("{0} {1} {2}", searchCriteria.DisplayName, searchCriteria.SearchOperator.ToFriendlyDisplayString(), searchCriteria.Value);
        }

        internal static string ToFriendlyDisplayString(this SearchOperator searchOperator)
        {
            string searchOperatorStr = searchOperator.ToString();
            string displayText = Resources.ResourceManager.GetString(searchOperatorStr);

            if (displayText == null)
            {
                // split on capital letters and add spaces - purely as a fallback in case the enum value is not localised
                Regex capitalLetterMatch = new Regex("\\B[A-Z]", RegexOptions.Compiled);
                displayText = capitalLetterMatch.Replace(searchOperatorStr, " $&").ToLower(Thread.CurrentThread.CurrentCulture);
            }

            return displayText;
        }
    }
}
