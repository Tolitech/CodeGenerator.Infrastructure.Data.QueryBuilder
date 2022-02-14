using System;
using Xunit;
using Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.Domain.Entities;
using Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests.SqlServer;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.Tests
{
    public class SqlBuilderTest
    {
        public SqlBuilderTest()
        {
            SqlBuilderConfiguration.UseSqlServer();
        }

        [Fact(DisplayName = "SqlBuilder - Insert - Valid")]
        public void SqlBuilder_Insert_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("dbo", "Person")
                .AddColumns(person)
                .Build();

            string expected = "insert into [dbo].[Person] ([PersonId], [Name], [Age]) values (@PersonId, @Name, @Age);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - InsertWithIdentity - Valid")]
        public void SqlBuilder_InsertWithIdentity_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = null,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("Person")
                .AddColumns(person)
                .Identity("PersonId")
                .Build();

            string expected = "insert into [dbo].[Person] ([Name], [Age]) values (@Name, @Age); select cast(SCOPE_IDENTITY() as int);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - InsertWithRename - Valid")]
        public void SqlBuilder_InsertWithRename_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("dbo", "Person")
                .AddColumns(person)
                .AddColumn("Address")
                .AddColumns("City1", "State1", "State2", "State3", "State")
                .RenameColumn("City1", "City")
                .RemoveColumn("State1")
                .RemoveColumns("State2", "State3")
                .Build();

            string expected = "insert into [dbo].[Person] ([PersonId], [Name], [Age], [Address], [City], [State]) values (@PersonId, @Name, @Age, @Address, @City1, @State);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Update - Valid")]
        public void SqlBuilder_Update_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Update("dbo", "Person")
                .AddColumns(person)
                .RemoveColumn(nameof(person.PersonId))
                .Where()
                .AddColumn(nameof(person.PersonId))
                .Build();

            string expected = "update [dbo].[Person] set [Name] = @Name, [Age] = @Age where [PersonId] = @PersonId;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - UpdateWithRename - Valid")]
        public void SqlBuilder_UpdateWithRename_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Update("Person")
                .AddColumns(person)
                .AddColumn("Address")
                .AddColumns("City1", "State1", "State2", "State3", "State")
                .RenameColumn("City1", "City")
                .RemoveColumn("State1")
                .RemoveColumns("State2", "State3")
                .Where()
                .AddColumn(nameof(person.PersonId))
                .Build();

            string expected = "update [dbo].[Person] set [PersonId] = @PersonId, [Name] = @Name, [Age] = @Age, [Address] = @Address, [City] = @City1, [State] = @State where [PersonId] = @PersonId;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Delete - Valid")]
        public void SqlBuilder_Delete_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("dbo", "Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "delete from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - DeleteWithoutSchema - Valid")]
        public void SqlBuilder_DeleteWithoutSchema_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "delete from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Select - Valid")]
        public void SqlBuilder_Select_Valid()
        {
            string sql = new SqlBuilder()
                .Select("dbo", "Person")
                .AddColumns("PersonId", "Name")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select [PersonId], [Name] from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - SelectAsterisk - Valid")]
        public void SqlBuilder_SelectAsterisk_Valid()
        {
            string sql = new SqlBuilder()
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select * from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - SelectWithRename - Valid")]
        public void SqlBuilder_SelectWithRename_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Select("dbo", "Person")
                .AddColumns(person)
                .AddColumn("Address")
                .AddColumns("City1", "State1", "State2", "State3", "State")
                .RenameColumn("City1", "City")
                .RemoveColumn("State1")
                .RemoveColumns("State2", "State3")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select [PersonId], [Name], [Age], [Address], [City] as [City1], [State] from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Where - Valid")]
        public void SqlBuilder_Where_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Select("Person")
                .AddColumns(person)
                .AddColumn("Address")
                .AddColumns("City1", "State1", "State2", "State3", "State")
                .RenameColumn("City1", "City")
                .RemoveColumn("State1")
                .RemoveColumns("State2", "State3")
                .Where()
                .AddColumn("PersonId")
                .AddColumn("Address")
                .AddColumns("City1", "State1", "State2", "State3", "State")
                .RenameColumn("City1", "City")
                .RemoveColumn("State1")
                .RemoveColumns("State2", "State3")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select [PersonId], [Name], [Age], [Address], [City] as [City1], [State] from [dbo].[Person] where [PersonId] = @PersonId and [Address] = @Address and [City] = @City1 and [State] = @State and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - SelectWithValueObject - Valid")]
        public void SqlBuilder_SelectWithValueObject_Valid()
        {
            var person = new VipPersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18,
                PhoneNumber = new Domain.ValueObjects.PhoneNumberValueObject
                {
                    AreaCode = "1",
                    PhoneNumber = "1234"
                }
            };

            string sql = new SqlBuilder()
                .Select("dbo", "Person")
                .AddColumns(person)
                .Where()
                .AddColumn("PersonId")
                .Build();

            string expected = "select [AreaCode], [PhoneNumber], [PersonId], [Name], [Age] from [dbo].[Person] where [PersonId] = @PersonId;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Providers - Valid")]
        public void SqlBuilder_Providers_Valid()
        {
            SqlBuilderConfiguration.AddQueryBuilder("SqlServer");
            SqlBuilderConfiguration.AddQueryBuilder("Test");

            string sqlDefault = new SqlBuilder()
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string sqlSqlServer = new SqlBuilder("SqlServer")
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string sqlTest = new SqlBuilder("Test")
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            Assert.Equal(sqlDefault, sqlSqlServer);
            Assert.Equal(sqlDefault, sqlTest);
        }
    }
}
