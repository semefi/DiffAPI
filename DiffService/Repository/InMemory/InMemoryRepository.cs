using DiffAPI.Models;
using DiffAPI.Repository.SQLServer;
using System;
using System.Collections.Generic;

namespace DiffAPI.Repository.InMemory
{
    public class InMemoryRepository : IComparisonRepository
    {
        //private static readonly IDictionary<string, string> _data = new Dictionary<string, string>();
        private static Lazy<Dictionary<string, Comparison>> _data;

        public InMemoryRepository()
        {
            if (_data == null) _data = new Lazy<Dictionary<string, Comparison>>(true);
        }

        public InMemoryRepository(Dictionary<string, Comparison> dictionary)
        {
            _data = new Lazy<Dictionary<string, Comparison>>(() => dictionary, true);
        }

        public void Insert(Comparison data)
        {
            if (!_data.Value.ContainsKey(data.ComparisonId))
            {
                _data.Value.Add(data.ComparisonId, data);
            }
        }

        public Comparison Get(string id)
        {
            Comparison comparision;
            _data.Value.TryGetValue(id, out comparision);
            return comparision;
        }

        public void Update(Comparison data)
        {
            if (_data.Value.ContainsKey(data.ComparisonId))
            {
                _data.Value[data.ComparisonId] = data;
            }
        }

    }
}