using System;
using System.Collections.Generic;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class MemberFacets
    {
        private readonly IDatabaseProvider databaseProvider;

        public MemberFacets(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public IEnumerable<MemberFacet> ForMember(int memberId)
        {
            dynamic db = databaseProvider.GetDatabase();

            var query = db.MemberFacets.FindAllByMemberId(memberId)
                .Select(db.MemberFacets.ID, db.MemberFacets.FacetId, db.MemberFacets.FreeTextValue,
                        db.MemberFacets.MemberFacetValue.FacetValueId,
                        db.MemberFacets.MemberFacetValue.FacetValue.Value,
                        db.MemberFacets.MemberFacetValue.Id.As("MemberFacetValueId"),
                        db.MemberFacets.Facet.Type, db.MemberFacets.Facet.Name);

            var listCache = new Dictionary<int, MemberFacetList>();

            foreach (var row in query)
            {
                if (row.Type == FacetType.FreeText.ToString())
                {
                    MemberFacetFreeText facet = row;
                    facet.Facet = new Facet {Id = row.FacetId, Name = row.Name, Type = FacetType.FreeText};
                    yield return facet;
                }
                else if (row.Type == FacetType.List.ToString())
                {
                    MemberFacetList facet;
                    if (listCache.ContainsKey(row.Id))
                    {
                        facet = listCache[row.Id];
                    }
                    else
                    {
                        facet = row;
                        facet.Facet = new Facet { Id = row.FacetId, Name = row.Name, Type = FacetType.List };
                        facet.Values = new List<MemberFacetValue>();
                        listCache.Add(facet.Id, facet);
                    }
                    facet.Values.Add(new MemberFacetValue
                                         {
                                             Id = row.MemberFacetValueId,
                                             FacetValueId = row.MemberFacetValueId,
                                             MemberFacetId = row.Id
                                         });
                }
            }

            foreach (var memberFacetList in listCache.Values)
            {
                yield return memberFacetList;
            }
        }

        public MemberFacet Insert(MemberFacet facet)
        {
            var freeTextFacet = facet as MemberFacetFreeText;
            if (freeTextFacet != null)
            {
                return Insert(freeTextFacet);
            }

            var listFacet = facet as MemberFacetList;
            if (listFacet != null)
            {
                return Insert(listFacet);
            }

            throw new InvalidOperationException("Unknown MemberFacet type.");
        }

        public MemberFacetFreeText Insert(MemberFacetFreeText facet)
        {
            return databaseProvider.GetDatabase().MemberFacets.Insert(facet);
        }

        public MemberFacetList Insert(MemberFacetList facet)
        {
            using (var transaction = databaseProvider.GetDatabase().BeginTransaction())
            {
                try
                {
                    MemberFacetList inserted = transaction.MemberFacets.Insert(facet);

                    inserted.Values = new List<MemberFacetValue>();
                    foreach (var value in facet.Values)
                    {
                        value.MemberFacetId = inserted.Id;
                        MemberFacetValue insertedValue = transaction.MemberFacetValues.Insert(value);
                        inserted.Values.Add(insertedValue);
                    }
                    transaction.Commit();
                    return inserted;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
