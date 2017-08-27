namespace DiffAPI.Models
{
    public class Comparison
    {
        /// <summary>
        /// Comparison Id
        /// </summary>
        public string ComparisonId { get; set; }

        /// <summary>
        /// Left part to be Diff-ed
        /// </summary>
        public string Left { get; set; }

        /// <summary>
        /// Right part to be Diff-ed
        /// </summary>
        public string Right { get; set; }

    }
}