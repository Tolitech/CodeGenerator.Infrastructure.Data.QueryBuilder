# Infrastructure.Data.QueryBuilder
Infrastructure Data Query Builder library used in projects created by the Code Generator tool. 

This project contains abstract classes for implementing and customizing query builder database providers. 

Tolitech Code Generator Tool: [http://www.tolitech.com.br](https://www.tolitech.com.br/)

Examples:
```
SqlBuilderConfiguration.UseSqlServer();
```

```
SqlBuilderConfiguration.AddQueryBuilder("SqlServer");
SqlBuilderConfiguration.AddQueryBuilder("PostgreSql");
SqlBuilderConfiguration.AddQueryBuilder("MySql");
```

```
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
```

```
var person = new PersonEntity()
{
    Name = "Person 1",
    Age = 18
};

string sql = new SqlBuilder()
    .Insert("Person")
    .AddColumns(person)
    .Identity("PersonId")
    .Build();

string expected = "insert into [dbo].[Person] ([Name], [Age]) values (@Name, @Age); select cast(SCOPE_IDENTITY() as int);";
```

```
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
```

```
string sql = new SqlBuilder()
    .Delete("dbo", "Person")
    .Where()
    .AddColumn("PersonId")
    .AddCondition("and [Age] > @Age")
    .Build();

string expected = "delete from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";
```

```
string sql = new SqlBuilder()
    .Select("dbo", "Person")
    .AddColumns("PersonId", "Name")
    .Where()
    .AddColumn("PersonId")
    .AddCondition("and [Age] > @Age")
    .Build();

string expected = "select [PersonId], [Name] from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";
```