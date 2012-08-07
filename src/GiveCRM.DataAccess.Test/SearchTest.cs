﻿namespace GiveCRM.DataAccess.Test
{
    using System;
    using System.Linq;
    using GiveCRM.Models.Search;
    using GiveCRM.Web.Models.Search;
    using NUnit.Framework;
    using Simple.Data;

    [TestFixture]
    [Ignore("Ignored until a new version of Simple.Data is available to fix issues with InMemoryAdapter.")]
    public class SearchTest
    {
        [Test]
        public void LocationOnCity()
        {
            var criteria = new[]
                               {
                                   new LocationSearchCriteria
                                       {
                                           InternalName = LocationSearchCriteria.City,
                                           SearchOperator = SearchOperator.EqualTo,
                                           Type = SearchFieldType.String,
                                           Value = "London"
                                       }
                               };

            var expr = new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileLocationCriteria(criteria, null);

            var reference = expr.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("city", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.Equal, expr.Type);
            Assert.AreEqual("London", expr.RightOperand);
        }

        [Test]
        public void LocationOnRegion()
        {
            var criteria = new[]
                               {
                                   new LocationSearchCriteria
                                       {
                                           InternalName = LocationSearchCriteria.Region,
                                           SearchOperator = SearchOperator.NotEqualTo,
                                           Type = SearchFieldType.String,
                                           Value = "Yorkshire"
                                       }
                               };

            var expr = new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileLocationCriteria(criteria, null);

            var reference = expr.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("region", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.NotEqual, expr.Type);
            Assert.AreEqual("Yorkshire", expr.RightOperand);
        }

        [Test]
        public void LocationOnPartialPostalCode()
        {
            var criteria = new[]
                               {
                                   new LocationSearchCriteria
                                       {
                                           InternalName = LocationSearchCriteria.PostalCode,
                                           SearchOperator = SearchOperator.StartsWith,
                                           Type = SearchFieldType.String,
                                           Value = "N1"
                                       }
                               };

            var expr = new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileLocationCriteria(criteria, null);

            var reference = expr.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("postalcode", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.Function, expr.Type);
            var function = expr.RightOperand as SimpleFunction;
            Assert.IsNotNull(function);
            Assert.AreEqual("like", function.Name.ToLowerInvariant());
            Assert.AreEqual(1, function.Args.Count);
            Assert.AreEqual("N1%", function.Args.First());
        }

        [Test]
        public void DonationOnIndividualDonation()
        {
            var criteria = new[]
                               {
                                   new DonationSearchCriteria
                                       {
                                           InternalName = DonationSearchCriteria.IndividualDonation,
                                           SearchOperator = SearchOperator.EqualTo,
                                           Type = SearchFieldType.Double,
                                           Value = 100m.ToString()
                                       }
                               };

            SimpleExpression expr = null;
            SimpleExpression having = null;
            new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileDonationCriteria(criteria, ref expr, ref having);

            Assert.IsNotNull(expr);
            Assert.IsNull(having);

            var reference = expr.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("Amount", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.Equal, expr.Type);
            Assert.AreEqual(100m, expr.RightOperand);
        }

        [Test]
        public void DonationOnTotalDonation()
        {
            var criteria = new[]
                               {
                                   new DonationSearchCriteria
                                       {
                                           InternalName = DonationSearchCriteria.TotalDonations,
                                           SearchOperator = SearchOperator.GreaterThan,
                                           Type = SearchFieldType.Double,
                                           Value = 100m.ToString()
                                       }
                               };

            SimpleExpression expr = null;
            SimpleExpression having = null;
            new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileDonationCriteria(criteria, ref expr, ref having);

            Assert.IsNull(expr);
            Assert.IsNotNull(having);

            var function = having.LeftOperand as FunctionReference;
            Assert.IsNotNull(function);
            var reference = function.Argument as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("Amount", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.GreaterThan, having.Type);
            Assert.AreEqual(100m, having.RightOperand);
        }

        [Test]
        public void DonationOnLastDonationDate()
        {
            var criteria = new[]
                               {
                                   new DonationSearchCriteria
                                       {
                                           InternalName = DonationSearchCriteria.LastDonationDate,
                                           SearchOperator = SearchOperator.LessThan,
                                           Type = SearchFieldType.Date,
                                           Value = "2011-01-01"
                                       }
                               };

            SimpleExpression expr = null;
            SimpleExpression having = null;
            new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileDonationCriteria(criteria, ref expr, ref having);

            Assert.IsNull(expr);
            Assert.IsNotNull(having);

            var function = having.LeftOperand as FunctionReference;
            Assert.IsNotNull(function);
            Assert.AreEqual("Max", function.Name);
            var reference = function.Argument as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("Date", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.LessThan, having.Type);
            Assert.AreEqual(new DateTime(2011,1,1), having.RightOperand);
        }

        [Test]
        public void Campaign()
        {
            var criteria = new[]
                               {
                                   new CampaignSearchCriteria
                                       {
                                           InternalName = CampaignSearchCriteria.DonatedToCampaign,
                                           SearchOperator = SearchOperator.StartsWith,
                                           Type = SearchFieldType.String,
                                           Value = "Christmas"
                                       }
                               };

            var expr = new SearchQueryService(new InMemorySingleTenantDatabaseProvider()).CompileCampaignCriteria(criteria, null);

            var reference = expr.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("Name", reference.GetName());
            Assert.AreEqual(SimpleExpressionType.Function, expr.Type);
            var function = expr.RightOperand as SimpleFunction;
            Assert.IsNotNull(function);
            Assert.AreEqual("like", function.Name.ToLowerInvariant());
            Assert.AreEqual(1, function.Args.Count);
            Assert.AreEqual("Christmas%", function.Args.First());
        }

        [Test]
        public void Facet()
        {
            var search = new SearchQueryService(new InMemorySingleTenantDatabaseProvider());
            var criteria = new[]
                               {
                                   new FacetSearchCriteria
                                       {
                                           InternalName = "freeTextFacet_1",
                                           DisplayName = "Test",
                                           FacetId = 1,
                                           SearchOperator = SearchOperator.StartsWith,
                                           Type = SearchFieldType.String,
                                           Value = "GiveCamp"
                                       }
                               };

            var expr = search.CompileFacetCriteria(criteria, null);

            Assert.AreEqual(SimpleExpressionType.And, expr.Type);

            var first = expr.LeftOperand as SimpleExpression;
            Assert.IsNotNull(first);
            var reference = first.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("FacetId", reference.GetName());
            Assert.AreEqual("freeTextFacet_1", reference.GetOwner().GetAlias());
            Assert.AreEqual(SimpleExpressionType.Equal, first.Type);
            Assert.AreEqual(1, first.RightOperand);

            var second = expr.RightOperand as SimpleExpression;
            Assert.IsNotNull(second);
            reference = second.LeftOperand as ObjectReference;
            Assert.IsNotNull(reference);
            Assert.AreEqual("FreeTextValue", reference.GetName());
            Assert.AreEqual("freeTextFacet_1", reference.GetOwner().GetAlias());
            var function = second.RightOperand as SimpleFunction;
            Assert.IsNotNull(function);
            Assert.AreEqual("like", function.Name.ToLowerInvariant());
            Assert.AreEqual(1, function.Args.Count);
            Assert.AreEqual("GiveCamp%", function.Args.First());
            Assert.AreEqual(SimpleExpressionType.Function, second.Type);
        }
    }
}
