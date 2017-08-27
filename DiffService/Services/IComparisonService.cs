using DiffAPI.Models;

namespace DiffAPI.Services
{
    public interface IComparisonService
    {
        /// <summary>
        /// Method to add the left side to be Diff-ed
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <param name="left">Value for the left side</param>
        void AddLeft(string id, string left);
        /// <summary>
        /// Method to add the right side to be Diff-ed
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <param name="right">Value for the right side</param>
        void AddRight(string id, string right);
        /// <summary>
        /// Method to get the Diff-ed information of a Comparison
        /// </summary>
        /// <param name="id">Comparison Id</param>
        /// <returns>A Comparison object with the Diff-ed information.</returns>
        Comparison Get(string id);
    }
}