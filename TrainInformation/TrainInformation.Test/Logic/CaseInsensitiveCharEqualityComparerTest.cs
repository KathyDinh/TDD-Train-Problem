﻿using NUnit.Framework;
using TrainInformation.Logic;

namespace TrainInformation.Test.Logic
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

        [TestCase('r', 'R')]
        [TestCase('V', 'V')]
        [TestCase('0', '0')]
        public void GetHashCode_ShouldReturnHashCodeOfUpperCase(char oneChar, char upperCaseChar)
        {
            var expected = upperCaseChar.GetHashCode();

            var target = new CaseInsensitiveCharEqualityComparer();
            var actual = target.GetHashCode(oneChar);

            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}
