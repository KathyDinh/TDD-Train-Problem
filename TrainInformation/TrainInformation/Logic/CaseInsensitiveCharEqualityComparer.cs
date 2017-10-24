using System.Collections.Generic;

namespace TrainInformation.Logic
{
    internal class CaseInsensitiveCharEqualityComparer: IEqualityComparer<char>
    {
        public bool Equals(char x, char y)
        {
            return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
        }

        public int GetHashCode(char obj)
        {
            //this is to ensure that the dictionary using this comparer will add key correctly
            return char.ToUpperInvariant(obj).GetHashCode();
        }
    }
}
