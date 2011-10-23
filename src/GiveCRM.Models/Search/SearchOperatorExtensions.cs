using System.Text.RegularExpressions;
using System.Threading;

namespace GiveCRM.Models.Search
{
    internal static class SearchOperatorExtensions
    {
        internal static string ToFriendlyDisplayString(this SearchOperator searchOperator)
        {
            string searchOperatorStr = searchOperator.ToString();
            string displayText = searchOperatorStr;

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
