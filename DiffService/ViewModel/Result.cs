using System.Collections.Generic;

namespace DiffAPI.ViewModel
{
    public class Result
    {
        /// <summary>
        /// Field that indicates if the comparison result is Equal
        /// </summary>
        public bool AreEqual { get; set; }

        /// <summary>
        /// Field that indicates if the comparison result are of same size
        /// </summary>
        public bool AreSameSize { get; set; }

        /// <summary>
        /// Field that indicates if the comparison result has differences
        /// </summary>
        public ICollection<Difference> Differences { get; set; }

        public Result(bool areEqual, bool sameSize, ICollection<Difference> differences)
        {
            this.AreEqual = areEqual;
            this.AreSameSize = sameSize;
            this.Differences = differences ?? new List<Difference>();

        }
    }
}