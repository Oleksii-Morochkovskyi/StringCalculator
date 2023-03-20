using System.Runtime.CompilerServices;
using NUnit.Framework;
using Moq;
using System;
using StringCalculator;

namespace StringCalculator.Tests
{
    public class ConsolePerformerTests
    {
        private readonly ConsoleWriter printer;
        
        [SetUp]
        public void Setup()
        {
            var writerMock = new Mock<ConsoleWriter>();
        }

        //[Test]
        public void Print_ShouldReturnString_IfStringIsNotEmpty()
        {
            Assert.Pass();
        }
    }
}