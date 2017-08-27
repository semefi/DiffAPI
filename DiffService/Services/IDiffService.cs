using DiffAPI.ViewModel;

namespace DiffAPI.Services
{
    public interface IDiffService
    {
        /// <summary>
        /// Method to get the result containing the DIff-ed information
        /// </summary>
        /// <param name="left">Left part of the comparison.</param>
        /// <param name="right">Right part of the comparison.</param>
        /// <returns></returns>
        Result GetDifferences(string left, string right);
    }
}