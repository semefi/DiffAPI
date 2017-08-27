using DiffAPI.ViewModel;
using System.Collections.Generic;

namespace DiffAPI.Services
{
    public class DiffService : IDiffService
    {
        public Result GetDifferences(string left, string right)
        {
            //First we try the best scenario
            if (left.Equals(right)) return new Result(true, true, null);
            //Second we try the fastest wrong scenario
            if (left.Length != right.Length) return new Result(false, false, null);
            //At least the complicated one
            var offset = 0;
            var length = 0;
            var differences = new List<Difference>();
            for (var index = 0; index < left.Length; index++)
            {
                var areEqualChars = left[index] == right[index];
                var isLengthZero = length == 0;
                //If are the same and there's not a pending difference (isLengthZero == true) then continue
                if (areEqualChars && isLengthZero) continue;
                //If aren't the same and is the first char difference, we capture the offset and increase the length difference
                if (!areEqualChars && isLengthZero)
                {
                    offset = index;
                    length++;
                }
                //If the difference has more than 1 character length
                else if (!areEqualChars && !isLengthZero)
                {
                    length++;
                }
                //If this char are equal but there is a pending difference to save
                else
                {
                    differences.Add(new Difference(offset, length));
                    offset = 0;
                    length = 0;
                }
            }
            //if there is a pending difference, because the different is at the end of the strings
            if (length > 0) differences.Add(new Difference(offset, length));
            return new Result(differences.Count == 0, true, differences);
        }

    }
}