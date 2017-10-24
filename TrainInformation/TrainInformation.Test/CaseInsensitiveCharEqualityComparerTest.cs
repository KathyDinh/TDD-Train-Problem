using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TrainInformation.Test
{
    [TestFixture]
    class CaseInsensitiveCharEqualityComparerTest
    {
        [TestCase('A', 'a')]
        [TestCase('C', 'c')]
        [TestCase('x', 'X')]
        public void Equals_ShouldReturnTrueForSameCharacterOfDifferentCase(char oneChar, char anotherChar)
        {
            var target = new CaseInsensitiveCharEqualityComparer();
            var actual = target.Equals(oneChar, anotherChar);

            Assert.That(actual, Is.True);
        }

        [TestCase('k', 'l')]
        [TestCase('4', 'Z')]
        [TestCase('M', 'N')]
        public void Equals_ShouldReturnFalseForDifferentCharacters(char oneChar, char anotherChar)
        {
            var target = new CaseInsensitiveCharEqualityComparer();
            var actual = target.Equals(oneChar, anotherChar);

            Assert.That(actual, Is.False);
        }

    }
}
