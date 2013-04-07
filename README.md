xunitTypedClassData
===================

Typed Class Data Attribute for xUnit Test Framework, allowing to use typed data (classes, structs, anonymous types or dynamic types) as a source for xUnit's data theories, instead of using untyped object arrays.

Iñaki Elcoro 
http://ielcoro.azurewebsites.net

##Usage

### Using class data as parameter for data theories

Using this typed data model:

```csharp
public class FooBar
{
    public int Foo { get; set; }
    public string Bar { get; set; }
}
```

Create a data provider for theories, as IEnumerable<FooBar>:

```csharp
public class ClassTypeData : IEnumerable<FooBar>
{

    public IEnumerator<FooBar> GetEnumerator()
    {
        yield return new FooBar() { Foo = 1, Bar = "bar" };
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
```

Use as a typed parameter on tests:

```csharp
using Xunit.Extensions;

[Theory, TypedClassData(typeof(ClassTypeData))]
public void ClassTypeTest(FooBar classData)
{
  Assert.Equal(1, classData.Foo);
	Assert.Equal("bar", classData.Bar);
}
```

### Using anonymous types and dynamic parameters for data theories

Create a data provider for theories, that projects an anonymous object:

```csharp
public class AnonymousTypeData : IEnumerable<object>
{

    public IEnumerator<object> GetEnumerator()
    {
        yield return new { Foo = 1, Bar = "bar" };
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
```

Use it as dynamic object on tests:

```csharp
using Xunit.Extensions;

[Theory, TypedClassData(typeof(AnonymousTypeData))]
public void ClassTypeTest(dynamic data)
{
	Assert.Equal(1, data.Foo);
	Assert.Equal("bar", data.Bar);
}
```

You can mix and match dynamic, class or anonymous data the way bests fits your tests cases!
