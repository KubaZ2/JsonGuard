using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonGuard.Test;

[JsonGuard]
public partial class NonNullablePropertyClass
{
    public string SomeProperty { get; set; }
}

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial class NullablePropertyClass
{
    public string? SomeProperty { get; set; }
}
#pragma warning restore JG0003 // No non-nullable reference type properties

[JsonGuard]
public partial class NonNullablePropertyGenericClass<T> where T : class
{
    public T SomeProperty { get; set; }
}

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial class NullablePropertyGenericClass<T> where T : class
{
    public T? SomeProperty { get; set; }
}
#pragma warning restore JG0003 // No non-nullable reference type properties

[JsonGuard]
public partial class NonNullablePropertyNullableGenericClass<T> where T : class?
{
    public T SomeProperty { get; set; }
}

public static partial class ContainingClass
{
    [JsonGuard]
    public partial class NonNullablePropertyClass
    {
        public string SomeProperty { get; set; }
    }
}

public partial class GenericContainingClass<T> where T : class
{
    [JsonGuard]
    public partial class NonNullablePropertyClass
    {
        public T SomeProperty { get; set; }
    }
}

[JsonGuard]
public partial class ManyNonNullablePropertiesClass
{
    public string SomeProperty1 { get; set; }

    public string SomeProperty2 { get; set; }
}

[JsonGuard]
public partial record NonNullablePropertyRecord(string SomeProperty);

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial record NullablePropertyRecord(string? SomeProperty);
#pragma warning restore JG0003 // No non-nullable reference type properties

[JsonGuard]
public partial struct NonNullablePropertyStruct
{
    public string SomeProperty { get; set; }
}

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial struct NullablePropertyStruct
{
    public string? SomeProperty { get; set; }
}
#pragma warning restore JG0003 // No non-nullable reference type properties

[JsonGuard]
public partial record struct NonNullablePropertyRecordStruct(string SomeProperty);

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial record struct NullablePropertyRecordStruct(string? SomeProperty);
#pragma warning restore JG0003 // No non-nullable reference type properties

public partial interface IContainingInterface
{
    [JsonGuard]
    public partial class NonNullablePropertyClass
    {
        public string SomeProperty { get; set; }
    }
}

public partial struct ContainingStruct
{
    [JsonGuard]
    public partial class NonNullablePropertyClass
    {
        public string SomeProperty { get; set; }
    }
}

public partial class VirtualPropertyBaseClass
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public virtual string SomeProperty { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}

[JsonGuard]
public partial class VirtualPropertyDerivedClass : VirtualPropertyBaseClass
{
    public override string SomeProperty
    {
        get
        {
            VirtualPropertyDerivedClassCounter.Increment();
            return base.SomeProperty;
        }
        set
        {
            base.SomeProperty = value;
        }
    }
}

public static class VirtualPropertyDerivedClassCounter
{
    private static int _count;

    public static int Count => _count;

    public static void Increment()
    {
        Interlocked.Increment(ref _count);
    }

    public static void Reset()
    {
        Interlocked.Exchange(ref _count, 0);
    }
}

public abstract class AbstractPropertyBaseClass
{
    public abstract string SomeProperty { get; set; }
}

[JsonGuard]
public partial class AbstractPropertyDerivedClass : AbstractPropertyBaseClass
{
    public override string SomeProperty
    {
        get
        {
            AbstractPropertyDerivedClassCounter.Increment();
            return field;
        }
        set;
    }
}

public static class AbstractPropertyDerivedClassCounter
{
    private static int _count;

    public static int Count => _count;

    public static void Increment()
    {
        Interlocked.Increment(ref _count);
    }

    public static void Reset()
    {
        Interlocked.Exchange(ref _count, 0);
    }
}

#pragma warning disable JG0003 // No non-nullable reference type properties
[JsonGuard]
public partial class EmptyClass;
#pragma warning restore JG0003 // No non-nullable reference type properties

public class ParentClass
{
    public ChildClass? Child { get; set; }
}

[JsonGuard]
public partial class ChildClass
{
    public string SomeProperty { get; set; }
}

[TestClass]
public sealed class Test
{
    [TestMethod]
    public void TestMissingNonNullableProperty()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNullableProperty()
    {
        var obj = JsonSerializer.Deserialize<NullablePropertyClass>("{}"u8);

        Assert.IsNotNull(obj);
        Assert.IsNull(obj.SomeProperty);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyGeneric()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyGenericClass<string>>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNullablePropertyGeneric()
    {
        var obj = JsonSerializer.Deserialize<NullablePropertyGenericClass<string>>("{}"u8);

        Assert.IsNotNull(obj);
        Assert.IsNull(obj.SomeProperty);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyNullableGeneric()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyNullableGenericClass<string>>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyNested()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ContainingClass.NonNullablePropertyClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyNestedGeneric()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<GenericContainingClass<string>.NonNullablePropertyClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingManyNonNullableProperties()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ManyNonNullablePropertiesClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty1' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingManyNonNullablePropertiesWithFirstPresent()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ManyNonNullablePropertiesClass>("""{"SomeProperty1":"value1"}"""u8));

        Assert.AreEqual("The member 'SomeProperty2' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingManyNonNullablePropertiesWithSecondPresent()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ManyNonNullablePropertiesClass>("""{"SomeProperty2":"value2"}"""u8));

        Assert.AreEqual("The member 'SomeProperty1' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingManyNonNullablePropertiesWithBothPresent()
    {
        var obj = JsonSerializer.Deserialize<ManyNonNullablePropertiesClass>("""{"SomeProperty1":"value1","SomeProperty2":"value2"}"""u8);

        Assert.IsNotNull(obj);
        Assert.AreEqual("value1", obj.SomeProperty1);
        Assert.AreEqual("value2", obj.SomeProperty2);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyRecord()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyRecord>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNullablePropertyRecord()
    {
        var obj = JsonSerializer.Deserialize<NullablePropertyRecord>("{}"u8);

        Assert.IsNotNull(obj);
        Assert.IsNull(obj.SomeProperty);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyStruct()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyStruct>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNullablePropertyStruct()
    {
        var obj = JsonSerializer.Deserialize<NullablePropertyStruct>("{}"u8);

        Assert.IsNull(obj.SomeProperty);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyRecordStruct()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<NonNullablePropertyRecordStruct>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNullablePropertyRecordStruct()
    {
        var obj = JsonSerializer.Deserialize<NullablePropertyRecordStruct>("{}"u8);

        Assert.IsNull(obj.SomeProperty);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyNestedInInterface()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<IContainingInterface.NonNullablePropertyClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [TestMethod]
    public void TestMissingNonNullablePropertyNestedInStruct()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ContainingStruct.NonNullablePropertyClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [DoNotParallelize]
    [TestMethod]
    public void TestMissingNonNullablePropertyInVirtualPropertyDerivedClass()
    {
        VirtualPropertyDerivedClassCounter.Reset();

        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<VirtualPropertyDerivedClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [DoNotParallelize]
    [TestMethod]
    public void TestPresentNonNullablePropertyInVirtualPropertyDerivedClassWithGet()
    {
        VirtualPropertyDerivedClassCounter.Reset();

        var obj = JsonSerializer.Deserialize<VirtualPropertyDerivedClass>("""{"SomeProperty":"value"}"""u8);

        Assert.IsNotNull(obj);
        Assert.AreEqual(1, VirtualPropertyDerivedClassCounter.Count);
        Assert.AreEqual("value", obj.SomeProperty);
    }

    [DoNotParallelize]
    [TestMethod]
    public void TestMissingNonNullablePropertyInAbstractPropertyDerivedClass()
    {
        AbstractPropertyDerivedClassCounter.Reset();

        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<AbstractPropertyDerivedClass>("{}"u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }

    [DoNotParallelize]
    [TestMethod]
    public void TestPresentNonNullablePropertyInAbstractPropertyDerivedClassWithGet()
    {
        AbstractPropertyDerivedClassCounter.Reset();

        var obj = JsonSerializer.Deserialize<AbstractPropertyDerivedClass>("""{"SomeProperty":"value"}"""u8);

        Assert.IsNotNull(obj);
        Assert.AreEqual(1, AbstractPropertyDerivedClassCounter.Count);
        Assert.AreEqual("value", obj.SomeProperty);
    }

    [TestMethod]
    public void TestEmptyClassIsIJsonOnDeserialized()
    {
        Assert.Contains(typeof(IJsonOnDeserialized), typeof(EmptyClass).GetInterfaces());
    }

    [TestMethod]
    public void TestNestedClassGetsGurded()
    {
        var exception = Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<ParentClass>("""{"Child":{}}"""u8));

        Assert.AreEqual("The member 'SomeProperty' cannot be null.", exception.Message);
    }
}
