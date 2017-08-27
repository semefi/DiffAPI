using DiffAPI.Models;
using DiffAPI.Repository.SQLServer;
using System;

namespace DiffAPI.Services
{
    public class ComparisonService : IComparisonService
    {
        private readonly IComparisonRepository _comparisonRepository;

        public ComparisonService()
        {
            //InMemory
            //_comparisonRepository = new InMemoryRepository();

            //SQL Server
            var connectionString = System.Configuration.ConfigurationManager.
                ConnectionStrings["Test"].ConnectionString;
            _comparisonRepository = new ComparisonRepository(connectionString);
        }

        public ComparisonService(IComparisonRepository repository)
        {
            _comparisonRepository = repository;
        }

        public void AddLeft(string id, string left)
        {
            if (string.IsNullOrWhiteSpace(left)) throw new ArgumentNullException("Left");
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Id");
            var newValue = new Comparison() { ComparisonId = id, Left = left };
            //We check if the id already exists
            var entity = _comparisonRepository.Get(id);
            if (entity != null)
            {
                //If It does exist, we update the value
                entity.Left = newValue.Left;
                _comparisonRepository.Update(entity);
            }
            else
            {
                _comparisonRepository.Insert(newValue);
            }

        }


        public void AddRight(string id, string right)
        {
            if (string.IsNullOrWhiteSpace(right)) throw new ArgumentNullException("Right cannot be null");
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Id cannot be null");
            var newValue = new Comparison() { ComparisonId = id, Right = right };
            //We check if the id already exists
            var entity = _comparisonRepository.Get(id);
            if (entity != null)
            {
                //If It does exist, we update the value
                entity.Right = newValue.Right;
                _comparisonRepository.Update(entity);
            }
            else
            {
                _comparisonRepository.Insert(newValue);
            }

        }

        public Comparison Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Id cannot be null");
            return _comparisonRepository.Get(id);
        }
    }
}