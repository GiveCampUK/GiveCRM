using System;
using System.Collections.Generic;

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

        public void Update(Facet facet)
        {
            if (facet.Values != null && facet.Values.Count > 0)
            {
                UpdateWithValues(facet);
            }
            else
            {
                _db.Facets.UpdateById(facet);
            }
        }

        private void UpdateWithValues(Facet facet)
        {
            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    transaction.Facets.UpdateById(facet);
                    foreach (var facetValue in facet.Values)
                    {
                        if (facetValue.Id == 0)
                        {
                            facetValue.FacetId = facet.Id;
                            transaction.FacetValues.Insert(facetValue);
                        }
                        else
                        {
                            transaction.FacetValues.UpdateById(facetValue);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
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
