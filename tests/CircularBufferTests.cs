using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cyotek.Collections.Generic.CircularBuffer.Tests
{
  [TestFixture]
  public class CircularBufferTests
  {
    #region  Tests

    [Test]
    public void CapacityExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;

      expected = 3;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      // act
      Assert.That(() => target.Capacity = expected, Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void CapacityExistingItemsTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedCapacity;
      string[] expectedItems;
      int expectedSize;

      expectedCapacity = 10;
      expectedSize = 2;
      expectedItems = new[]
                      {
                        "Alpha",
                        "Beta"
                      };

      target = new CircularBuffer<string>(3);
      target.Put("Alpha");
      target.Put("Beta");

      // act
      target.Capacity = expectedCapacity;

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Capacity, Is.EqualTo(expectedCapacity));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
        Assert.That(target.ToArray(), Is.EqualTo(expectedItems));
      }
    }

    [Test]
    public void CapacitySmallerTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;

      expected = 3;

      target = new CircularBuffer<string>(10);

      // act
      target.Capacity = expected;

      // assert
      Assert.That(target.Capacity, Is.EqualTo(expected));
    }

    [Test]
    public void CapacityTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;

      expected = 10;

      target = new CircularBuffer<string>(3);

      // act
      target.Capacity = expected;

      // assert
      Assert.That(target.Capacity, Is.EqualTo(expected));
    }

    [Test]
    public void ClearTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;

      expectedHead = 0;
      expectedSize = 0;
      expectedTail = 0;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.Clear();

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void CollectionAddTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string expected;

      target = new CircularBuffer<string>(10);

      expected = "Alpha";
      expectedHead = 0;
      expectedSize = 1;
      expectedTail = 1;

      // act
      ((ICollection<string>)target).Add(expected);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Contains(expected), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void CollectionCopyToTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedHead;
      string[] expected;
      string[] actual;
      int offset;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Zeta",
                   "Alpha",
                   "Beta",
                   "Gamma"
                 };
      actual = new[]
               {
                 "Zeta",
                 null,
                 null,
                 null
               };

      expectedHead = 0;
      offset = 1;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      ((ICollection)target).CopyTo(actual, offset);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void CollectionCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;
      int actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = 3;

      // act
      actual = ((ICollection)target).Count;

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CollectionIsReadOnlyTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      // act
      actual = ((ICollection<string>)target).IsReadOnly;

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void CollectionIsSynchronizedTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      // act
      actual = ((ICollection)target).IsSynchronized;

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void CollectionRemoveTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      // act & assert
      Assert.That(() => ((ICollection<string>)target).Remove("Alpha"), Throws.TypeOf<NotSupportedException>());
    }

    [Test]
    public void ConstructorCapacityExceptionTest()
    {
      // act & assert
      Assert.That(() => new CircularBuffer<string>(-1), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void ConstructorWithCapacityAndDefaultOverflowTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedCapacity;

      expectedCapacity = 10;

      // act
      target = new CircularBuffer<string>(expectedCapacity);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Capacity, Is.EqualTo(expectedCapacity));
        Assert.That(target.AllowOverwrite, Is.True);
      }
    }

    [Test]
    public void ConstructorWithoutOverflowTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedCapacity;

      expectedCapacity = 10;

      // act
      target = new CircularBuffer<string>(expectedCapacity, false);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Capacity, Is.EqualTo(expectedCapacity));
        Assert.That(target.AllowOverwrite, Is.False);
      }
    }

    [Test]
    public void ConstructorWithOverflowTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedCapacity;

      expectedCapacity = 10;

      // act
      target = new CircularBuffer<string>(expectedCapacity, true);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Capacity, Is.EqualTo(expectedCapacity));
        Assert.That(target.AllowOverwrite, Is.True);
      }
    }

    [Test]
    public void ContainsAfterGetTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      target.Get();

      // act
      actual = target.Contains("Alpha");

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void ContainsNegativeTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.Contains("Delta");

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void ContainsTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.Contains("Alpha");

      // assert
      Assert.That(actual, Is.True);
    }

    [Test]
    public void CopyToArrayWithOffsetAndCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedHead;
      string[] expected;
      string[] actual;
      int offset;
      int count;
      int index;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Zeta",
                   "Alpha",
                   "Beta",
                   "Eta"
                 };
      actual = new[]
               {
                 "Zeta",
                 null,
                 null,
                 "Eta"
               };

      expectedHead = 0;
      index = 0;
      offset = 1;
      count = 2;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.CopyTo(index, actual, offset, count);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void CopyToArrayWithOffsetTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedHead;
      string[] expected;
      string[] actual;
      int offset;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Zeta",
                   "Alpha",
                   "Beta",
                   "Gamma"
                 };
      actual = new[]
               {
                 "Zeta",
                 null,
                 null,
                 null
               };

      expectedHead = 0;
      offset = 1;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.CopyTo(actual, offset);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void CopyToArrayWithStartingIndexOffsetAndCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedHead;
      string[] expected;
      string[] actual;
      int offset;
      int count;
      int index;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Zeta",
                   "Beta",
                   "Gamma",
                   "Eta"
                 };
      actual = new[]
               {
                 "Zeta",
                 null,
                 null,
                 "Eta"
               };

      expectedHead = 0;
      index = 1;
      offset = 1;
      count = 2;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.CopyTo(index, actual, offset, count);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void CopyToExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;
      string[] actual;
      int offset;
      int count;
      int index;

      target = new CircularBuffer<string>(10);
      actual = new string[target.Capacity];

      index = 0;
      offset = 0;
      count = 4;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act & assert
      Assert.That(() => target.CopyTo(index, actual, offset, count), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void CopyToTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedHead;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Alpha",
                   "Beta",
                   "Gamma"
                 };
      expectedHead = 0;

      actual = new string[3];

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.CopyTo(actual);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void EmptyBufferTest()
    {
      // arrange
      CircularBuffer<byte> target;
      byte[] expected;
      byte[] actual;

      expected = this.GenerateRandomData(100);

      target = new CircularBuffer<byte>(expected.Length);
      target.Put(expected);

      actual = new byte[target.Size];

      // act
      target.Get(actual);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Size, Is.EqualTo(0));
      }
    }

    [Test]
    public void EnumeratorAfterGetTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      List<string> expected;
      List<string> actual;

      target = new CircularBuffer<string>(10);

      expected = new List<string>(new[]
                                  {
                                    "Beta",
                                    "Gamma"
                                  });
      expectedHead = 1;
      expectedSize = 2;
      expectedTail = 3;

      actual = new List<string>();

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Get();

      // act
      foreach (string value in target)
      {
        actual.Add(value);
      }

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GenericCollectionCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;
      int actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = 3;

      // act
      actual = ((ICollection<string>)target).Count;

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Get();
      target.Get();
      target.Get();

      // act & assert
      Assert.That(() => target.Get(), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void GetEnumeratorTest()
    {
      // arrange
      CircularBuffer<string> target;
      List<string> expected;
      List<string> actual;
      IEnumerator<string> enumerator;

      target = new CircularBuffer<string>(3);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      expected = new List<string>(new[]
                                  {
                                    "Beta",
                                    "Gamma",
                                    "Delta"
                                  });
      actual = new List<string>();

      // act
      enumerator = target.GetEnumerator();
      while (enumerator.MoveNext())
      {
        actual.Add(enumerator.Current);
      }

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLast_handles_wrapped_tail()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(3);

      expected = "Gamma";

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.GetLast();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLast_should_decrease_size()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;
      int actual;

      target = new CircularBuffer<string>(10);

      expected = 2;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.GetLast();

      // assert
      actual = target.Size;
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLast_should_decrease_tail()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;
      int actual;

      target = new CircularBuffer<string>(10);

      expected = 2;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.GetLast();

      // assert
      actual = target.Tail;
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLast_should_not_affect_existing_items()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual1;
      bool actual2;

      target = new CircularBuffer<string>(10);

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.GetLast();

      // assert
      using (Assert.EnterMultipleScope())
      {
        actual1 = target.Contains("Alpha");
        actual2 = target.Contains("Beta");
        Assert.That(actual1, Is.True);
        Assert.That(actual2, Is.True);
      }
    }

    [Test]
    public void GetLast_should_remove_item()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      target.GetLast();

      // assert
      actual = target.Contains("Gamma");
      Assert.That(actual, Is.False);
    }

    [Test]
    public void GetLast_should_return_last_added_item()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(10);

      expected = "Gamma";

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.GetLast();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetLast_throws_exception_if_buffer_empty()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Get();
      target.Get();
      target.Get();

      // act & assert
      Assert.That(() => target.GetLast(), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void GetNextTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Get();

      expected = "Beta";

      // act
      actual = target.Get();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetResetHeadAtCapacityTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;
      int expectedHead;

      target = new CircularBuffer<string>(3);

      expected = "Gamma";
      expectedHead = 0;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      target.Get();
      target.Get();

      // act
      actual = target.Get();

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Head, Is.EqualTo(expectedHead));
      }
    }

    [Test]
    public void GetTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string expected;
      string actual;

      target = new CircularBuffer<string>(10);

      expected = "Alpha";
      expectedHead = 1;
      expectedSize = 2;
      expectedTail = 3;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.Get();

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.False);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GetWithArrayAndOffsetTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      int expectedElements;
      int actualElements;
      string[] expected;
      string[] actual;
      int offset;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   null,
                   "Alpha",
                   "Beta"
                 };
      expectedHead = 2;
      expectedSize = 2;
      expectedTail = 4;
      expectedElements = 2;

      offset = 1;

      actual = new string[3];

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      // act
      actualElements = target.Get(actual, offset, expectedElements);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actualElements, Is.EqualTo(expectedElements));
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.False);
        Assert.That(target.Contains("Beta"), Is.False);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Contains("Delta"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GetWithArrayTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      int expectedElements;
      int actualElements;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Alpha",
                   "Beta"
                 };
      expectedHead = 2;
      expectedSize = 1;
      expectedTail = 3;
      expectedElements = 2;

      actual = new string[expectedElements];

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actualElements = target.Get(actual);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actualElements, Is.EqualTo(expectedElements));
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.False);
        Assert.That(target.Contains("Beta"), Is.False);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GetWithArrayWrapBufferTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedElements;
      int actualElements;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(4);

      expected = new[]
                 {
                   "Beta",
                   "Gamma",
                   "Delta",
                   "Epsilon"
                 };
      expectedElements = 4;

      actual = new string[expectedElements];

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");
      target.Put("Epsilon");

      // act
      actualElements = target.Get(actual);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actualElements, Is.EqualTo(expectedElements));
        Assert.That(actual, Is.EqualTo(expected));
      }
    }

    [Test]
    public void GetWithCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Alpha",
                   "Beta"
                 };
      expectedHead = 2;
      expectedSize = 1;
      expectedTail = 3;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.Get(2);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.False);
        Assert.That(target.Contains("Beta"), Is.False);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GetLastWithCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Beta",
                   "Gamma"
                 };
      expectedHead = 0;
      expectedSize = 1;
      expectedTail = 1;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.GetLast(2);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.False);
        Assert.That(target.Contains("Gamma"), Is.False);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void PeekLastWithCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Beta",
                   "Gamma"
                 };
      expectedHead = 0;
      expectedSize = 3;
      expectedTail = 3;

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.PeekLast(2);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actual, Is.EqualTo(expected));
        Assert.That(target.Contains("Alpha"), Is.True);
        Assert.That(target.Contains("Beta"), Is.True);
        Assert.That(target.Contains("Gamma"), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void GetWithTooLargeCountTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedElements;
      int actualElements;
      string[] expected;
      string[] actual;
      int offset;
      int count;

      target = new CircularBuffer<string>(10);

      expected = new[]
                 {
                   "Alpha",
                   "Beta",
                   "Gamma",
                   "Delta",
                   null,
                   null,
                   null,
                   null
                 };
      expectedElements = 4;

      offset = 0;
      count = 8;

      actual = new string[count];

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      // act
      actualElements = target.Get(actual, offset, count);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(actualElements, Is.EqualTo(expectedElements));
        Assert.That(actual, Is.EqualTo(expected));
      }
    }

    [Test]
    public void GetWrapBufferTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(3);

      expected = "Delta";

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");
      target.Put("Epsilon");
      target.Put("Zeta");

      // act
      actual = target.Get();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void IsEmptyNegativeTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      // act
      actual = target.IsEmpty;

      // assert
      Assert.That(actual, Is.True);
    }

    [Test]
    public void IsEmptyTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      target.Put("Alpha");

      // act
      actual = target.IsEmpty;

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void IsFullNegativeTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(10);

      target.Put("Alpha");

      // act
      actual = target.IsFull;

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void IsFullTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(3, false);

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.IsFull;

      // assert
      Assert.That(actual, Is.True);
    }

    [Test]
    public void IsFullWrapTest()
    {
      // arrange
      CircularBuffer<string> target;
      bool actual;

      target = new CircularBuffer<string>(3, true);

      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.IsFull;

      // assert
      Assert.That(actual, Is.False);
    }

    [Test]
    public void Issue20Test()
    {
      var buffer = new CircularBuffer<string>(5);
      buffer.Put(new[] { "a", "b", "c", "d", "e" });
      using (Assert.EnterMultipleScope())
      {
        Assert.That(buffer.PeekLast(2), Is.EqualTo(new[] { "d", "e" }));
        Assert.That(buffer.Peek(2), Is.EqualTo(new[] { "a", "b" }));
      }
      buffer.Put(new[] { "f", "g" });
      using (Assert.EnterMultipleScope())
      {
        Assert.That(buffer.PeekLast(3), Is.EqualTo(new[] { "e", "f", "g" }));
        Assert.That(buffer.Peek(3), Is.EqualTo(new[] { "c", "d", "e" }));
      }
    }

    [Test]
    public void PeekArrayEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act & assert
      Assert.That(() => target.Peek(2), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void PeekArrayTest()
    {
      // arrange
      CircularBuffer<string> target;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = new[]
                 {
                   "Alpha",
                   "Beta"
                 };

      // act
      actual = target.Peek(2);

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0, "Alpha")]
    [TestCase(1, "Beta")]
    [TestCase(2, "Gamma")]
    public void PeekAtTestCases(int index, string expected)
    {
      // arrange
      CircularBuffer<string> target;
      string actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act
      actual = target.PeekAt(index);

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PeekAtWrapTest()
    {
      // arrange
      CircularBuffer<string> target;
      string actual;
      string expected;

      target = new CircularBuffer<string>(2);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = "Gamma";

      // act
      actual = target.PeekAt(1);

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(-1)]
    [TestCase(3)]
    public void PeekAtEmptyExceptionTestCases(int index)
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      // act & assert
      Assert.That(() => target.PeekAt(index), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void PeekAtAboveBoundsExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act & assert
      Assert.That(() => target.PeekAt(-1), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void PeekEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act & assert
      Assert.That(() => target.Peek(), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void PeekLastEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act & assert
      Assert.That(() => target.PeekLast(), Throws.TypeOf<InvalidOperationException>());
    }


    [Test]
    public void PeekLastTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = "Gamma";

      // act
      actual = target.PeekLast();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PeekLastWrapBufferTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(3);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = "Gamma";

      // act
      actual = target.PeekLast();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PeekTest()
    {
      // arrange
      CircularBuffer<string> target;
      string expected;
      string actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = "Alpha";

      // act
      actual = target.Peek();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PutArrayExceptionTest()
    {
      // arrange
      CircularBuffer<byte> target;
      byte[] expected;

      expected = this.GenerateRandomData(100);

      target = new CircularBuffer<byte>(expected.Length, false);
      target.Put(byte.MaxValue);

      // act & assert
      Assert.That(() => target.Put(expected), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void PutArrayTest()
    {
      // arrange
      CircularBuffer<byte> target;
      byte[] expected;

      expected = this.GenerateRandomData(100);

      target = new CircularBuffer<byte>(expected.Length);

      // act
      target.Put(expected);

      // assert
      Assert.That(target.ToArray(), Is.EqualTo(expected));
    }

    [Test]
    public void PutBufferFullExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(2, false);

      target.Put("Alpha");
      target.Put("Beta");

      // act & assert
      Assert.That(() => target.Put("Gamma"), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void PutMultipleTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string expected1;
      string expected2;
      string expected3;

      target = new CircularBuffer<string>(10);

      expected1 = "Alpha";
      expected2 = "Beta";
      expected3 = "Gamma";
      expectedHead = 0;
      expectedSize = 3;
      expectedTail = 3;

      // act
      target.Put(expected1);
      target.Put(expected2);
      target.Put(expected3);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Contains(expected1), Is.True);
        Assert.That(target.Contains(expected2), Is.True);
        Assert.That(target.Contains(expected3), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void PutTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string expected;

      target = new CircularBuffer<string>(10);

      expected = "Alpha";
      expectedHead = 0;
      expectedSize = 1;
      expectedTail = 1;

      // act
      target.Put(expected);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Contains(expected), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void PutWholeLengtWithCountToAllowOverwriteTest()
    {
      CircularBuffer<int> target;
      int[] actual;
      int[] expected;
      int bufSize = 3;
      target = new CircularBuffer<int>(bufSize, true);

      int[] values = new int[]
                     {
                       4,
                       5,
                       6,
                       7,
                       8
                     };
      expected = values.Take(bufSize).ToArray();
      target.Put(values, 0, bufSize);

      actual = target.ToArray();

      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PutWholeLengtWithoutCountToAllowOverwriteTest()
    {
      CircularBuffer<int> target;
      int[] actual;
      int[] expected;
      int bufSize = 3;
      target = new CircularBuffer<int>(bufSize, true);

      int[] values = new int[]
                     {
                       4,
                       5,
                       6
                     };
      expected = values.Take(bufSize).ToArray();
      target.Put(values);

      actual = target.ToArray();

      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void PutWrapBufferTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expectedSize;
      int expectedHead;
      int expectedTail;
      string expected1;
      string expected2;
      string expected3;
      string expected4;

      target = new CircularBuffer<string>(3);

      expected1 = "Alpha";
      expected2 = "Beta";
      expected3 = "Gamma";
      expected4 = "Delta";
      expectedHead = 1;
      expectedSize = 3;
      expectedTail = 1;

      // act
      target.Put(expected1);
      target.Put(expected2);
      target.Put(expected3);
      target.Put(expected4);

      // assert
      using (Assert.EnterMultipleScope())
      {
        Assert.That(target.Contains(expected1), Is.False);
        Assert.That(target.Contains(expected2), Is.True);
        Assert.That(target.Contains(expected3), Is.True);
        Assert.That(target.Contains(expected4), Is.True);
        Assert.That(target.Head, Is.EqualTo(expectedHead));
        Assert.That(target.Tail, Is.EqualTo(expectedTail));
        Assert.That(target.Size, Is.EqualTo(expectedSize));
      }
    }

    [Test]
    public void SizeTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;
      int actual;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");

      expected = 3;

      // act
      actual = target.Size;

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void WrappedBufferResizeTest()
    {
      // arrange
      CircularBuffer<string> target;
      string[] expected;
      string[] actual;

      target = new CircularBuffer<string>(4, true);
      target.Put("1");
      target.Put("2");
      target.Put("3");
      target.Put("4");
      target.Put("5"); //ensuring buffer is wrapped
      target.Put("6");

      expected = new[] {"3", "4", "5", "6"};

      // act
      target.Capacity = 6;
      actual = target.ToArray();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SkipOverCapacityTest()
    {
      // arrange
      CircularBuffer<byte> target;
      int expectedHead;

      target = new CircularBuffer<byte>(100, true);
      target.Skip(75);
      expectedHead = 25;

      // act
      target.Skip(50);

      // assert
      Assert.That(target.Head, Is.EqualTo(expectedHead));
    }

    [Test]
    public void SkipTest()
    {
      // arrange
      CircularBuffer<string> target;
      int expected;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      expected = 2;

      // act
      target.Skip(2);

      // assert
      Assert.That(target.Head, Is.EqualTo(expected));
    }

    [Test]
    public void SkipWrapBufferTest()
    {
      // arrange
      CircularBuffer<byte> target;
      byte[] dataIn;

      dataIn = this.GenerateRandomData(100);
      int HL = dataIn.Length / 2;
      target = new CircularBuffer<byte>(dataIn.Length, true);
      target.Put(this.GenerateRandomData(HL));
      target.Put(dataIn);

      byte[] expected = new byte[dataIn.Length];
      Buffer.BlockCopy(dataIn, 0, expected, HL, HL);
      Buffer.BlockCopy(dataIn, HL, expected, 0, HL);

      // act
      target.Skip(HL);

      byte[] actual = target.ToArray();
      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToArrayTest()
    {
      // arrange
      CircularBuffer<string> target;
      string[] actual;
      string[] expected;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      expected = new[]
                 {
                   "Alpha",
                   "Beta",
                   "Gamma",
                   "Delta"
                 };

      // act
      actual = target.ToArray();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToArrayWrapBufferTest()
    {
      // arrange
      CircularBuffer<string> target;
      string[] actual;
      string[] expected;

      target = new CircularBuffer<string>(3);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      expected = new[]
                 {
                   "Beta",
                   "Gamma",
                   "Delta"
                 };

      // act
      actual = target.ToArray();

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    #endregion

    #region Test Helpers

    private Random _random;

    [OneTimeTearDown]
    public void CleanUp()
    {
      _random = null;
    }

    [OneTimeSetUp]
    public void Setup()
    {
      _random = new Random();
    }

    protected byte[] GenerateRandomData(int length)
    {
      byte[] result;

      result = new byte[length];
      _random.NextBytes(result);

      return result;
    }

    #endregion

    [Test]
    public void CopyToRangeWrappedTest()
    {
      // arrange
      CircularBuffer<int> target;
      int[] expected;
      int[] actual;

      target = new CircularBuffer<int>(10);
      for (int i = 0; i < 16; i++)
      {
        target.Put(i);
      }

      expected = new[]
      {
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15
      };

      actual = new int[10];

      // act
      target.CopyTo(0, actual, 0, target.Size);

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CopyToRangeOffsetTest()
    {
      // arrange
      CircularBuffer<int> target;
      int[] expected;
      int[] actual;

      target = new CircularBuffer<int>(10);
      for (int i = 0; i < 16; i++)
      {
        target.Put(i);
      }

      expected = new[]
      {
        12,
        13,
        14,
        15,
        6,
        7,
        8,
        9,
        10,
        11,
      };

      actual = new int[10];

      // act
      target.CopyTo(6, actual, 0, target.Size);

      // assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetArrayWrapTest()
    {
      // arrange
      CircularBuffer<byte> target;
      int expected;

      // derived from https://github.com/cyotek/Cyotek.Collections.Generic.CircularBuffer/issues/17#issue-1084534453

      target = new CircularBuffer<byte>(10, false);
      target.Put(new byte[target.Capacity]);

      expected = 0;

      // act
      while (!target.IsEmpty)
      {
        target.Get(1);
      }

      // assert
      Assert.That(target.Head, Is.EqualTo(expected));
    }
  }
}
