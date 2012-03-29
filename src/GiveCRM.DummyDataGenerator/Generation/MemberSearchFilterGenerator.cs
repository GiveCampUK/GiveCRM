using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.BusinessLogic;
using GiveCRM.DataAccess;
using GiveCRM.DummyDataGenerator.Data;
using GiveCRM.Models;
using GiveCRM.Models.Search;
using GiveCRM.Web.Models.Search;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class MemberSearchFilterGenerator
    {
        private readonly RandomSource random = new RandomSource();
        private readonly MemberSearchFilters searchFilters = new MemberSearchFilters();
        private readonly SearchService searchRepo = new SearchService(new Facets(new DatabaseProvider()));

        private readonly Dictionary<SearchFieldType, IList<SearchOperator>> fieldTypeToOperatorMap;

        public MemberSearchFilterGenerator()
        {
            fieldTypeToOperatorMap = new Dictionary<SearchFieldType, IList<SearchOperator>>();

            var allTypesHave = new[]
                                   {
                                            SearchOperator.EqualTo, 
                                            SearchOperator.NotEqualTo,
                                            SearchOperator.StartsWith, 
                                            SearchOperator.EndsWith, 
                                            SearchOperator.Contains
                                   };

            var continuousTypesHave = new[]
                                          {
                                                SearchOperator.EqualTo, 
                                                SearchOperator.NotEqualTo,
                                                SearchOperator.LessThan, 
                                                SearchOperator.LessThanOrEqualTo,
                                                SearchOperator.GreaterThan, 
                                                SearchOperator.GreaterThanOrEqualTo
                                          };

            fieldTypeToOperatorMap.Add(SearchFieldType.Bool, allTypesHave);
            fieldTypeToOperatorMap.Add(SearchFieldType.Date, continuousTypesHave);
            fieldTypeToOperatorMap.Add(SearchFieldType.Double, continuousTypesHave);
            fieldTypeToOperatorMap.Add(SearchFieldType.Int, continuousTypesHave);
            fieldTypeToOperatorMap.Add(SearchFieldType.String, allTypesHave);
            fieldTypeToOperatorMap.Add(SearchFieldType.SelectList, allTypesHave);
        }

        internal void GenerateMemberSearchFilters(int campaignId)
        {
            var emptySearchCriteria = searchRepo.GetEmptySearchCriteria().ToList();

            for (int i = 0; i < random.NextInt(1, 3); i++)
            {
                var criteria = random.PickFromList(emptySearchCriteria);
                emptySearchCriteria.Remove(criteria);

                var criterionInfo = GenerateCriterion(criteria);
                var searchFilter = new MemberSearchFilter
                                       {
                                                   CampaignId = campaignId,
                                                   DisplayName = criteria.DisplayName,
                                                   InternalName = criteria.InternalName,
                                                   FilterType = criterionInfo.FilterType,
                                                   SearchOperator = criterionInfo.Operator,
                                                   Value = criterionInfo.Value
                                       };

                searchFilters.Insert(searchFilter);
            }
        }

        private CriterionData GenerateCriterion(SearchCriteria criterion)
        {
            var operators = fieldTypeToOperatorMap[criterion.Type];
            var searchOperator = random.PickFromList(operators);
            var value = GenerateValue(criterion);

            return new CriterionData
                       {
                            FilterType = (int) criterion.Type,
                            Operator = (int) searchOperator,
                            Value = value
                       };
        }

        private string GenerateValue(SearchCriteria criterion)
        {
            switch (criterion.Type)
            {
                case SearchFieldType.Bool:
                    return random.Bool().ToString();
                case SearchFieldType.Double:
                case SearchFieldType.Int:
                    return random.NextInt(1, 1000).ToString();
                case SearchFieldType.SelectList:
                case SearchFieldType.String:
                    return GenerateStringValue(criterion);
                case SearchFieldType.Date:
                    return random.NextDateTime().ToString("u");
                default:
                    throw new NotSupportedException("Unsupported search criterion type: " + criterion.Type);
            }
        }

        private string GenerateStringValue(SearchCriteria criterion)
        {
            switch (criterion.InternalName)
            {
                case LocationSearchCriteria.City:
                    return random.PickFromList(TownData.Data).Town;
                case LocationSearchCriteria.Region:
                    return random.PickFromList(TownData.Data).Region;
                case LocationSearchCriteria.PostalCode:
                    return new AddressGenerator().RandomPostalCode();
                case CampaignSearchCriteria.DonatedToCampaign:
                    //TODO: work out what this represents
                    return "random";
                default:
                    throw new NotSupportedException("Unsupported search criterion: " + criterion.InternalName);
            }
        }
    }
}