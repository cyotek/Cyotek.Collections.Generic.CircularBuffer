using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

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

      // act & assert
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
      target.Capacity.Should().
             Be(expectedCapacity);
      target.Size.Should().
             Be(expectedSize);
      target.ToArray().
             Should().
             Equal(expectedItems);
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
      target.Capacity.Should().
             Be(expected);
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
      target.Capacity.Should().
             Be(expected);
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
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      target.Contains(expected).
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeTrue();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             BeFalse();
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
      target.Capacity.Should().
             Be(expectedCapacity);
      target.AllowOverwrite.Should().
             BeTrue();
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
      target.Capacity.Should().
             Be(expectedCapacity);
      target.AllowOverwrite.Should().
             BeFalse();
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
      target.Capacity.Should().
             Be(expectedCapacity);
      target.AllowOverwrite.Should().
             BeTrue();
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             BeTrue();
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeTrue();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeTrue();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeTrue();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeTrue();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Equal(expected);
      target.Size.Should().
             Be(0);
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
      // ReSharper disable once LoopCanBeConvertedToQuery
      foreach (string value in target)
      {
        actual.Add(value);
      }

      // assert
      actual.Should().
             Equal(expected);
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             Equal(expected);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             Be(expected);
      target.Head.Should().
             Be(expectedHead);
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
      actual.Should().
             Be(expected);
      target.Contains("Alpha").
             Should().
             BeFalse();
      target.Contains("Beta").
             Should().
             BeTrue();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actualElements.Should().
                     Be(expectedElements);
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeFalse();
      target.Contains("Beta").
             Should().
             BeFalse();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Contains("Delta").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actualElements.Should().
                     Be(expectedElements);
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeFalse();
      target.Contains("Beta").
             Should().
             BeFalse();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actualElements.Should().
                     Be(expectedElements);
      actual.Should().
             Equal(expected);
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
      actual.Should().
             Equal(expected);
      target.Contains("Alpha").
             Should().
             BeFalse();
      target.Contains("Beta").
             Should().
             BeFalse();
      target.Contains("Gamma").
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actualElements.Should().
                     Be(expectedElements);
      actual.Should().
             Equal(expected);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             BeTrue();
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             BeTrue();
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
      actual.Should().
             BeFalse();
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
      actual.Should().
             Equal(expected);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             Be(expected);
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
      actual.Should().
             Be(expected);
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
      target.ToArray().
             Should().
             Equal(expected);
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
      target.Contains(expected1).
             Should().
             BeTrue();
      target.Contains(expected2).
             Should().
             BeTrue();
      target.Contains(expected3).
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      target.Contains(expected).
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      target.Contains(expected1).
             Should().
             BeFalse();
      target.Contains(expected2).
             Should().
             BeTrue();
      target.Contains(expected3).
             Should().
             BeTrue();
      target.Contains(expected4).
             Should().
             BeTrue();
      target.Head.Should().
             Be(expectedHead);
      target.Tail.Should().
             Be(expectedTail);
      target.Size.Should().
             Be(expectedSize);
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
      actual.Should().
             Be(expected);
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
      target.Head.Should().
             Be(expectedHead);
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
      target.Head.Should().
             Be(expected);
    }

    [Test]
    public void SkipWrapBufferTest()
    {
        // arrange
        CircularBuffer<byte> target;
        byte[] dataIn;

        dataIn = this.GenerateRandomData(100);
        var HL = dataIn.Length / 2;
        target = new CircularBuffer<byte>(dataIn.Length, true);
        target.Put(this.GenerateRandomData(HL));
        target.Put(dataIn);

        var expected = new byte[dataIn.Length];
        Buffer.BlockCopy(dataIn, 0, expected, HL, HL);
        Buffer.BlockCopy(dataIn, HL, expected, 0, HL);

        // act
        target.Skip(HL);



        var actual = target.ToArray();
        // assert
        actual.
                Should().
                Equal(expected);
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
      actual.Should().
             Equal(expected);
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
      actual.Should().
             Equal(expected);
    }

    [Test]
    public void PutWholeLengtWithoutCountToAllowOverwriteTest() {
        CircularBuffer<int> target;
        int[] actual;
        int[] expected;
        int bufSize = 3;
        target = new CircularBuffer<int>(bufSize, true);

        var values = new int[] { 4, 5, 6 };
        expected = values.Take(bufSize).ToArray();
        target.Put(values);

        actual = target.ToArray();


        actual.Should().Equal(expected);

        var val = 99;
        target.Put(val);
        val.Should().Equals(target.PeekLast());

    }

    [Test]
    public void PutWholeLengtWithCountToAllowOverwriteTest() {
        CircularBuffer<int> target;
        int[] actual;
        int[] expected;
        int bufSize = 3;
        target = new CircularBuffer<int>(bufSize, true);

        var values = new int[] { 4, 5, 6, 7, 8 };
        expected = values.Take(bufSize).ToArray();
        target.Put(values, 0, bufSize);

        actual = target.ToArray();


        actual.Should().Equal(expected);

        var val = 99;
        target.Put(val);
        val.Should().Equals(target.PeekLast());

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
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
