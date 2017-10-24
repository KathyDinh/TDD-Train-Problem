﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainInformation
{
    internal class CaseInsensitiveCharComparer: IEqualityComparer<char>
    {
        public bool Equals(char x, char y)
        {
            return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
        }

        public int GetHashCode(char obj)
        {
            return obj.GetHashCode();
        }
    }
}
