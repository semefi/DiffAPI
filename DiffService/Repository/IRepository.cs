namespace DiffAPI.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Method to Insert data on the repository.
        /// </summary>
        /// <param name="data"></param>
        void Insert(T data);
        /// <summary>
        /// Method to Get data from the repository by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);
        /// <summary>
        /// Method to update data on the repository
        /// </summary>
        /// <param name="data"></param>
        void Update(T data);
    }
}
