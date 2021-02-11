using System.Threading;
using Xunit;

// When we say "Parallelism in Test Frameworks", what we mean specifically is how a test framework may choose to support
// running tests within a single assembly in parallel with one another.
//
// How does xUnit.net decide which tests can run against each other in parallel?
// It uses a concept called test collections to make that decision.

namespace ParallelismExamples
{
    // By default, each test class is a unique test collection.
    // Tests within the same test class will not run in parallel against each other.
    public class ParallelizedClass1Tests
    {
        [Fact]
        public void Test1()
        {
            Thread.Sleep(2000);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
        }
    }

    public class ParallelizedClass2Tests
    {
        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }
    }
    
    // If we need to indicate that multiple test classes should not be run in parallel against one another, then we place
    // them into the same test collection. This is simply a matter of decorating each test class with an attribute that
    // places them into the same uniquely named test collection:

    [Collection("MyCollection")]
    public class ParallelizedClass3Tests
    {
        [Fact]
        public void Test3()
        {
            Thread.Sleep(3000);
        }
    }

    [Collection("MyCollection")]
    public class ParallelizedClass4Tests
    {
        [Fact]
        public void Test4()
        {
            Thread.Sleep(5000);
        }
    }    
}