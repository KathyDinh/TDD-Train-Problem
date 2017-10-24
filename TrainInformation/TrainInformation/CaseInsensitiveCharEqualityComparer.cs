using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainInformation
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
