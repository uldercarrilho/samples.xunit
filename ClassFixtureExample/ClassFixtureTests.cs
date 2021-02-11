using System;
using System.Data.SqlClient;
using Xunit;

// Can't get these tests to run? Make sure you've installed LocalDB, a feature in the (free) SQL Server Express.
// Note that you don't need the "instance" server of SQL Express, just the LocalDB feature.

// When to use: when you want to create a single test context and share it among all the tests in the class,
//              and have it cleaned up after all the tests in the class have finished.
//
// Sometimes test context creation and cleanup can be very expensive. If you were to run the creation and cleanup code
// during every test, it might make the tests slower than you want. You can use the class fixture feature of xUnit.net
// to share a single object instance among all tests in a test class.
//
// We already know that xUnit.net creates a new instance of the test class for every test. When using a class fixture,
// xUnit.net will ensure that the fixture instance will be created before any of the tests have run, and once all the
// tests have finished, it will clean up the fixture object by calling Dispose, if present.
//
// To use class fixtures, you need to take the following steps:
//
//  - Create the fixture class, and put the startup code in the fixture class constructor.
//  - If the fixture class needs to perform cleanup, implement IDisposable on the fixture class, and put the cleanup code in the Dispose() method.
//  - Add IClassFixture<> to the test class.
//  - If the test class needs access to the fixture instance, add it as a constructor argument, and it will be provided automatically.

public class ClassFixtureTests : IClassFixture<DatabaseFixture>
{
    DatabaseFixture database;

    public ClassFixtureTests(DatabaseFixture data)
    {
        database = data;
    }

    [Fact]
    public void ConnectionIsEstablished()
    {
        Assert.NotNull(database.Connection);
    }

    [Fact]
    public void FooUserWasInserted()
    {
        string sql = "SELECT COUNT(*) FROM Users WHERE ID = @id;";

        using (SqlCommand cmd = new SqlCommand(sql, database.Connection))
        {
            cmd.Parameters.AddWithValue("@id", database.FooUserID);

            int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

            Assert.Equal(1, rowCount);
        }
    }
}
