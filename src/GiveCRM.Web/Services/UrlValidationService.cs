using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace GiveCRM.Web.Services
{
    public interface IUrlValidationService
    {
        bool IsRedirectable(Controller controller,string url);
    }

    public class UrlValidationService : IUrlValidationService
    {
        private IEnumerable<IAmAUrlValidationRule> rules;

        public UrlValidationService(IEnumerable<IAmAUrlValidationRule>rules)
        {
            this.rules = rules;
        }

        public bool IsRedirectable(Controller controller, string url)
        {
            var result = true;
            foreach(var rule in rules)
            {
                if(!rule.IsValid(controller,url))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }



    public interface IAmAUrlValidationRule
    {
        bool IsValid(Controller controller, string url);
    }

    public class IsLocal:IAmAUrlValidationRule
    {

        public bool IsValid(Controller controller, string url)
        {
            return controller.Url.IsLocalUrl(url);
        }
    }

    public class LengthIsGreaterThanOne:IAmAUrlValidationRule
    {

        public bool IsValid(Controller controller, string url)
        {
            return url.Length > 1;
        }
    }

    public class BeginsWithForwardSlash : IAmAUrlValidationRule
    {

        public bool IsValid(Controller controller, string url)
        {
            return url.StartsWith("/");
        }
    }

    public class DoesNotBeginWithDoubleForwardSlash : IAmAUrlValidationRule
    {

        public bool IsValid(Controller controller, string url)
        {
            return !url.StartsWith("//");
        }
    }

    public class DoesNotBeginWithForwardSlashBackslash : IAmAUrlValidationRule
    {

        public bool IsValid(Controller controller, string url)
        {
            return !url.StartsWith("/\\");
        }
    }
}