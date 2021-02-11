using System;
using Xunit;

namespace TheoryExamples
{
    public class CalculatorTests
    {
        // Facts are tests which are always true. They test invariant conditions.
        [Fact]
        public void FactTest()
        {
            Assert.True(true);
        }

        // Theories are tests which are only true for a particular set of data.
        [Theory]
        [InlineData(1,1)]
        public void TheoryTest(int value, int result)
        {
            Assert.Equal(result, value);
        }
        
        [Theory]
        [InlineData(-1,0,-1)]
        [InlineData(0,0,0)]
        [InlineData(1,0,1)]
        public void Add_NToZero_ReturnsN(int addend1, int addend2, int result)
        {
            // arrange
            var calculator = new Calculator();
            // act
            var actual = calculator.Add(addend1, addend2);
            // assert
            Assert.Equal(result, actual);
        }

        [Theory(DisplayName = "Subtract only positive numbers")]
        [InlineData(3,1,2)]
        public void Subtract_NaturalNumber_ReturnsAMinusB(int minuend, int subtrahend, int result)
        {
            // arrange
            var calculator = new Calculator();
            // act
            var actual = calculator.Subtract(minuend, subtrahend);
            // assert
            Assert.Equal(result, actual);
        }

        [Theory(Timeout = 1000)]
        [InlineData(0, -1, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 2, 0)]
        public void Multiply_NWithZero_ReturnsAlwaysZero(int multiplier, int multiplicand, int result)
        {
            // arrange
            var calculator = new Calculator();
            // act
            var actual = calculator.Multiply(multiplier, multiplicand);
            // assert
            Assert.Equal(result, actual);
        }

        [Fact(Skip="Skip this by now")]
        [Trait("key", "value")]
        public void Divide_ByZero_ThrowException()
        {
            // arrange
            var calculator = new Calculator();
            // act
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(1, 0));
        }
    }
}