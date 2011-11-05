using System;
using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic
{
    internal class FacetsService : IFacetsService
    {
        private readonly IRepository<Facet> _repository;

        public FacetsService(IRepository<Facet> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public IEnumerable<Facet> All()
        {
            var facets = _repository.GetAll();
            return facets;
        }

        public Facet Get(int id)
        {
            var facet = _repository.GetById(id);
            return facet;
        }

        public void Insert(Facet facet)
        {
            _repository.Insert(facet);             
        }

        public void Update(Facet facet)
        {
            _repository.Update(facet);
        }
    }

    
}