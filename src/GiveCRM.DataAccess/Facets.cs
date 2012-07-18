namespace GiveCRM.DataAccess
{
    using System.Collections.Generic;
    using GiveCRM.BusinessLogic;
    using GiveCRM.Infrastructure;
    using GiveCRM.Models;

    public class Facets : IFacetRepository
    {
        private readonly IDatabaseProvider databaseProvider;

        public Facets(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public Facet GetById(int id)
        {
            var record = databaseProvider.GetDatabase().Facets.FindById(id);
            Facet facet = record;
            facet.Values = record.FacetValues.ToList<FacetValue>();
            return facet;
        }

        public IEnumerable<Facet> GetAll()
        {
            dynamic db = databaseProvider.GetDatabase();
            var query = db.Facets.All()
                .Select(
                    db.Facets.Id,
                    db.Facets.Type,
                    db.Facets.Name,
                    db.Facets.FacetValues.Id.As("FacetValueId"),
                    db.Facets.FacetValues.FacetId,
                    db.Facets.FacetValues.Value.As("FacetValueValue"))
                .OrderBy(db.Facets.Id);

            Facet facet = null;

            foreach (var row in query)
            {
                if (facet == null)
                {
                    facet = row;
                }
                if (row.FacetId == facet.Id)
                {
                    if (facet.Values == null)
                    {
                        facet.Values = new List<FacetValue>();
                    }
                    facet.Values.Add(new FacetValue { FacetId = row.Id, Id = row.FacetValueId, Value = row.FacetValueValue });
                }
                else
                {
                    yield return facet;
                    facet = null;
                }
            }

            if (facet != null)
            {
                yield return facet;
            }
        }

        public Facet Insert(Facet facet)
        {
            if (facet.Values != null && facet.Values.Count > 0)
            {
                return InsertWithValues(facet);
            }
            var record = databaseProvider.GetDatabase().Facets.Insert(facet);
            return record;
        }

        /// <summary>
        /// Deletes the <see cref="Facet"/> identified by the specified identifier.  
        /// </summary>
        /// <param name="id">The identifier of the Facet to delete.</param>
        public void DeleteById(int id)
        {
            databaseProvider.GetDatabase().Facets.DeleteById(id);
        }

        public void Update(Facet facet)
        {
            if (facet.Values != null && facet.Values.Count > 0)
            {
                UpdateWithValues(facet);
            }
            else
            {
                databaseProvider.GetDatabase().Facets.UpdateById(facet);
            }
        }

        private void UpdateWithValues(Facet facet)
        {
            using (var transaction = TransactionScopeFactory.Create())
            {
                var database = databaseProvider.GetDatabase();
                database.Facets.UpdateById(facet);
                foreach (var facetValue in facet.Values)
                {
                    if (facetValue.Id == 0)
                    {
                        facetValue.FacetId = facet.Id;
                        database.FacetValues.Insert(facetValue);
                    }
                    else if (string.IsNullOrWhiteSpace(facetValue.Value))
                    {
                        database.FacetValues.DeleteById(facetValue.Id);
                    }
                    else
                    {
                        database.FacetValues.UpdateById(facetValue);
                    }
                }

                transaction.Complete();
            }
        }

        private Facet InsertWithValues(Facet facet)
        {
            using (var transaction = TransactionScopeFactory.Create())
            {
                var database = databaseProvider.GetDatabase();
                Facet inserted = database.Facets.Insert(facet);
                foreach (var facetValue in facet.Values)
                {
                    facetValue.FacetId = inserted.Id;
                }
                inserted.Values = database.FacetValues.Insert(facet.Values).ToList<FacetValue>();

                transaction.Complete();
                return inserted;
            }
        }

        public IEnumerable<Facet> GetAllFreeText()
        {
            return databaseProvider.GetDatabase().Facets.FindAllByType(FacetType.FreeText.ToString()).Cast<Facet>();
        }
    }
}
