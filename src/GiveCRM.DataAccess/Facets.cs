using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.DataAccess
{
    public class Facets
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRM");

        public Facet Get(int id)
        {
            var record = _db.Facets.FindById(id);
            Facet facet = record;
            facet.Values = record.Values.ToList<FacetValue>();
            return facet;
        }

        public IEnumerable<Facet> All()
        {
            return _db.Facets.All().Cast<Facet>();
        }

        public Facet Insert(Facet facet)
        {
            if (facet.Values != null && facet.Values.Count > 0)
            {
                return InsertWithValues(facet);
            }
            return _db.Facets.Insert(facet);
        }

        private Facet InsertWithValues(Facet facet)
        {
            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    Facet inserted = transaction.Facets.Insert(facet);
                    foreach (var facetValue in facet.Values)
                    {
                        facetValue.FacetId = inserted.Id;
                    }
                    inserted.Values = transaction.FacetValues.Insert(facet.Values).ToList<FacetValue>();
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
