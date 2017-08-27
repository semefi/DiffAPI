namespace DiffAPI.ViewModel
{
    public class Difference
    {
        /// <summary>
        /// Offset of one difference
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Length of the difference
        /// </summary>
        public int Length { get; set; }

        public Difference(int offset, int length)
        {
            this.Offset = offset;
            this.Length = length;
        }
    }
}