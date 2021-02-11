using System;
using Xunit;

// When to use: when you want a clean test context for every test
// (sharing the setup and cleanup code, without sharing the object instance).

// xUnit.net creates a new instance of the test class for every test that is run, so any code which is placed into
// the constructor of the test class will be run for every single test. This makes the constructor a convenient
// place to put reusable context setup code where you want to share the code without sharing object instances
// (meaning, you get a clean copy of the context object(s) for every test that is run).

// This structure is sometimes called the "test class as context" pattern, since the test class itself is a
// self-contained definition of the context setup and cleanup code.

// Expectations List
//      Context: Empty Stack
//          Verify that Count is 0
//          Call Pop, Verify InvalidOperationException is thrown
//          Call Peek, Verify InvalidOperationException is thrown
//          Call Contains, Verify that it returns false
//
//      Context: Create a Stack, Push an Integer
//          Verify that Count is 1
//          Call Pop, Verify Count is 0
//          Call Peek, Verify Count is 1
//          Call Pop, Verify Pop returns the pushed integer
//          Call Peek, Verify Peek returns the pushed integer
//
//      Context: Create a Stack, Push Multiple Integers
//          Push 3 ints, verify that Count is 3
//          Push 3 ints 10, 20, 30, Call Pop three times, verify that they are removed 30, 20, 10
//          Create a Stack, Push(10), Push(20), Push(30), call Contains(20), verify that it returns true
//
//      Context: Create a Stack<string>
//          Push("Help"), call Pop, verify that what is returned from Pop equals "Help"

public class StackTests
{
    // You can even name the test classes after the setup context so that it's easier to remember what your starting point is:
    public class EmptyStack
    {
        Stack<int> stack;

        public EmptyStack()
        {
            stack = new Stack<int>();
        }

        [Fact]
        public void Count_ShouldReturnZero()
        {
            int count = stack.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void Contains_ShouldReturnFalse()
        {
            bool contains = stack.Contains(10);

            Assert.False(contains);
        }

        [Fact]
        public void Pop_ShouldThrowInvalidOperationException()
        {
            Exception exception = Record.Exception(() => stack.Pop());

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public void Peek_ShouldThrowInvalidOperationException()
        {
            Exception exception = Record.Exception(() => stack.Peek());

            Assert.IsType<InvalidOperationException>(exception);
        }
    }

    public class StackWithOneElement
    {
        Stack<int> stack;
        const int pushedValue = 42;


        public StackWithOneElement()
        {
            stack = new Stack<int>();
            stack.Push(pushedValue);
        }

        [Fact]
        public void Count_ShouldBeOne()
        {
            int count = stack.Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public void Pop_CountShouldBeZero()
        {
            stack.Pop();

            int count = stack.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void Peek_CountShouldBeOne()
        {
            stack.Peek();

            int count = stack.Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public void Pop_ShouldReturnPushedValue()
        {
            int actual = stack.Pop();

            Assert.Equal(pushedValue, actual);
        }

        [Fact]
        public void Peek_ShouldReturnPushedValue()
        {
            int actual = stack.Peek();

            Assert.Equal(pushedValue, actual);
        }
    }

    public class StackWithMultipleValues
    {
        Stack<int> stack;
        const int firstPushedValue = 42;
        const int secondPushedValue = 21;
        const int thirdPushedValue = 11;

        public StackWithMultipleValues()
        {
            stack = new Stack<int>();
            stack.Push(firstPushedValue);
            stack.Push(secondPushedValue);
            stack.Push(thirdPushedValue);
        }

        [Fact]
        public void Count_ShouldBeThree()
        {
            int count = stack.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public void Pop_VerifyLifoOrder()
        {
            Assert.Equal(thirdPushedValue, stack.Pop());
            Assert.Equal(secondPushedValue, stack.Pop());
            Assert.Equal(firstPushedValue, stack.Pop());
        }

        [Fact]
        public void Peek_ReturnsLastPushedValue()
        {
            int actual = stack.Peek();
            Assert.Equal(thirdPushedValue, actual);
        }

        [Fact]
        public void Contains_ReturnsTrue()
        {
            bool contains = stack.Contains(secondPushedValue);

            Assert.True(contains);
        }
    }

    public class StackWithStrings
    {
        [Fact]
        public void Pop_ShouldReturnPushedValue()
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("Help");

            string actual = stack.Pop();

            Assert.Equal("Help", actual);
        }
    }
}