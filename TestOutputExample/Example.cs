using System;
using Xunit;
using Xunit.Abstractions;

public class Example
{
    ITestOutputHelper output;

    // Unit tests have access to a special interface which replaces previous usage of Console and similar mechanisms: ITestOutputHelper.
    // In order to take advantage of this, just add a constructor argument for this interface, and stash it so you can use it in the unit test.
    public Example(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void TestThis()
    {
        output.WriteLine("I'm inside the test!");
    }
}