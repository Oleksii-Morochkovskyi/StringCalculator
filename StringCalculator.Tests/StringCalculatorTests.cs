using System;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTest
{
    public class StringCalculatorTests
    {
        private StringCalculatorWorker _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new StringCalculatorWorker();
        }

        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            var value = "";

            var result = _calculator.Add(value);

            Assert.That(result == 0);
        }

        [Test]
        public void Add_OneNumberString_ReturnsThisNumber()
        {
            var value = "1";

            var result = _calculator.Add(value);

            Assert.That(result == int.Parse(value));
        }
        [Test]
        public void Add_StringWithTwoNumbersAndSeparator_ReturnsTheirSum()
        {
            var value = "1,2";

            var result = _calculator.Add(value);

            Assert.That(result == 3);
        }

        [Test]
        public void Add_FewNumbersSeparatedByComma_ReturnsTheirSum()
        {
            var value = "1,2,3";

            var result = _calculator.Add(value);

            Assert.That(result == 6);
        }


        [Test]
        public void Add_NumbersSeparatedByNewLine_ReturnsTheirSum()
        {
            var value = @"1\n2";

            var result = _calculator.Add(value);

            Assert.That(result == 3);
        }

        [Test]
        public void Add_StringWithChangedDelimeter_ReturnsSum()
        {
            var value = "//;1;2;3";

            var result = _calculator.Add(value);

            Assert.That(result == 6);
        }

        [Test]
        public void Add_ContainsNegativeNumbers_ThrowsExeption()
        {
            var value = "1,-2,4";

            Assert.Throws<Exception>(() => _calculator.Add(value));
        }

        [Test]
        public void Add_NumberHigherThen1000_IgnoresNumbersHigherThen1000()
        {
            var value = "1005,5";

            var result = _calculator.Add(value);

            Assert.That(result == 5);
        }

        [Test]
        public void Add_StringWithNewMultipleCharDelimeter_ReturnsSumOfNumbers()
        {
            var value = @"//[***]\n1***2,3";

            var result = _calculator.Add(value);

            Assert.That(result == 6);
        }

        [Test]
        public void Add_StringWithFewNewMultipleCharDelimeters_ReturnsSumOfNumbers()
        {
            var value = @"//[***][+]\n1***2+3";

            var result = _calculator.Add(value);

            Assert.That(result == 6);
        }
    }
}