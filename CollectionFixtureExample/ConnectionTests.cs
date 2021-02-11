using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

// Can't get these tests to run? Make sure you've installed LocalDB, a feature in the (free) SQL Server Express.
// Note that you don't need the "instance" server of SQL Express, just the LocalDB feature.

// When to use: when you want to create a single test context and share it among tests in several test classes,
//              and have it cleaned up after all the tests in the test classes have finished.
//
// Sometimes you will want to share a fixture object among multiple test classes. The database example used for class
// fixtures is a great example: you may want to initialize a database with a set of test data, and then leave that test
// data in place for use by multiple test classes. You can use the collection fixture feature of xUnit.net to share a
// single object instance among tests in several test class.
//
// To use collection fixtures, you need to take the following steps:
//
// - Create the fixture class, and put the startup code in the fixture class constructor.
// - If the fixture class needs to perform cleanup, implement IDisposable on the fixture class,
//      and put the cleanup code in the Dispose() method.
// - Create the collection definition class, decorating it with the [CollectionDefinition] attribute, giving it a
//      unique name that will identify the test collection.
// - Add ICollectionFixture<> to the collection definition class.
// - Add the [Collection] attribute to all the test classes that will be part of the collection, using the unique name
//      you provided to the test collection definition class's [CollectionDefinition] attribute.
// - If the test classes need access to the fixture instance, add it as a constructor argument, and it will be provided automatically.

// xUnit.net treats collection fixtures in much the same way as class fixtures, except that the lifetime of a collection
// fixture object is longer: it is created before any tests are run in any of the test classes in the collection, and
// will not be cleaned up until all test classes in the collection have finished running.
//
// Test collections can also be decorated with IClassFixture<>. xUnit.net treats this as though each individual
// test class in the test collection were decorated with the class fixture.
//
// Test collections also influence the way xUnit.net runs tests when running them in parallel.
//
// Important note: Fixtures can be shared across assemblies, but collection definitions must be in the same assembly as the test that uses them.

[Collection("DatabaseCollection")]
public class ConnectionTests 
{
    DatabaseFixture database;

    public ConnectionTests(DatabaseFixture data)
    {
        database = data;
    }

    [Fact]
    public void ConnectionIsEstablished()
    {
        Assert.NotNull(database.Connection);
    }
}

