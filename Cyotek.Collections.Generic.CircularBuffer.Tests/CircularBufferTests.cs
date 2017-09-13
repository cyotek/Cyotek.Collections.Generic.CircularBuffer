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
    [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "The new capacity must be greater than or equal to the buffer size.\r\nParameter name: value\r\nActual value was 3.")]
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
      target.Capacity = expected;
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
      Assert.AreEqual(expectedCapacity, target.Capacity);
      Assert.AreEqual(expectedSize, target.Size);
      CollectionAssert.AreEqual(expectedItems, target.ToArray());
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
      Assert.AreEqual(expected, target.Capacity);
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
      Assert.AreEqual(expected, target.Capacity);
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
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.IsTrue(target.Contains(expected));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsTrue(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
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
      Assert.AreEqual(expected, actual);
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
      Assert.IsFalse(actual);
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
      Assert.IsFalse(actual);
    }

    [Test]
    [ExpectedException(typeof(NotSupportedException), ExpectedMessage = "Cannot remove items from collection.")]
    public void CollectionRemoveTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);
      target.Put("Alpha");
      target.Put("Beta");
      target.Put("Gamma");
      target.Put("Delta");

      // act
      ((ICollection<string>)target).Remove("Alpha");
    }

    [Test]
    [ExpectedException(typeof(ArgumentException), ExpectedMessage = "The buffer capacity must be greater than or equal to zero.\r\nParameter name: capacity")]
    public void ConstructorCapacityExceptionTest()
    {
      // act
      // ReSharper disable once ObjectCreationAsStatement
      new CircularBuffer<string>(-1);
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
      Assert.AreEqual(expectedCapacity, target.Capacity);
      Assert.IsTrue(target.AllowOverwrite);
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
      Assert.AreEqual(expectedCapacity, target.Capacity);
      Assert.IsFalse(target.AllowOverwrite);
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
      Assert.AreEqual(expectedCapacity, target.Capacity);
      Assert.IsTrue(target.AllowOverwrite);
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
      Assert.IsFalse(actual);
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
      Assert.IsFalse(actual);
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
      Assert.IsTrue(actual);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsTrue(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsTrue(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsTrue(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "The read count cannot be greater than the buffer size.\r\nParameter name: count\r\nActual value was 4.")]
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

      // act
      target.CopyTo(index, actual, offset, count);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsTrue(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.AreEqual(0, target.Size);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expected, actual);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer is empty.")]
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

      // act
      target.Get();
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
      CollectionAssert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
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
      actual1 = target.Contains("Alpha");
      actual2 = target.Contains("Beta");
      Assert.IsTrue(actual1);
      Assert.IsTrue(actual2);
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
      Assert.IsFalse(actual);
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
      Assert.AreEqual(expected, actual);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer is empty.")]
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

      // act
      target.GetLast();
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
      Assert.AreEqual(expectedHead, target.Head);
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
      Assert.AreEqual(expected, actual);
      Assert.IsFalse(target.Contains("Alpha"));
      Assert.IsTrue(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expectedElements, actualElements);
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsFalse(target.Contains("Alpha"));
      Assert.IsFalse(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.IsTrue(target.Contains("Delta"));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expectedElements, actualElements);
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsFalse(target.Contains("Alpha"));
      Assert.IsFalse(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expectedElements, actualElements);
      CollectionAssert.AreEqual(expected, actual);
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
      CollectionAssert.AreEqual(expected, actual);
      Assert.IsFalse(target.Contains("Alpha"));
      Assert.IsFalse(target.Contains("Beta"));
      Assert.IsTrue(target.Contains("Gamma"));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expectedElements, actualElements);
      CollectionAssert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
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
      Assert.IsTrue(actual);
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
      Assert.IsFalse(actual);
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
      Assert.IsFalse(actual);
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
      Assert.IsTrue(actual);
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
      Assert.IsFalse(actual);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer is empty.")]
    public void PeekArrayEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act
      target.Peek(2);
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
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer is empty.")]
    public void PeekEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act
      target.Peek();
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer is empty.")]
    public void PeekLastEmptyExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(10);

      // act
      target.PeekLast();
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expected, actual);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer does not have sufficient capacity to put new items.")]
    public void PutArrayExceptionTest()
    {
      // arrange
      CircularBuffer<byte> target;
      byte[] expected;

      expected = this.GenerateRandomData(100);

      target = new CircularBuffer<byte>(expected.Length, false);
      target.Put(byte.MaxValue);

      // act
      target.Put(expected);
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
      CollectionAssert.AreEqual(expected, target.ToArray());
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The buffer does not have sufficient capacity to put new items.")]
    public void PutBufferFullExceptionTest()
    {
      // arrange
      CircularBuffer<string> target;

      target = new CircularBuffer<string>(2, false);

      target.Put("Alpha");
      target.Put("Beta");

      // act
      target.Put("Gamma");
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
      Assert.IsTrue(target.Contains(expected1));
      Assert.IsTrue(target.Contains(expected2));
      Assert.IsTrue(target.Contains(expected3));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.IsTrue(target.Contains(expected));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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

      CollectionAssert.AreEqual(expected, actual);
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

      CollectionAssert.AreEqual(expected, actual);
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
      Assert.IsFalse(target.Contains(expected1));
      Assert.IsTrue(target.Contains(expected2));
      Assert.IsTrue(target.Contains(expected3));
      Assert.IsTrue(target.Contains(expected4));
      Assert.AreEqual(expectedHead, target.Head);
      Assert.AreEqual(expectedTail, target.Tail);
      Assert.AreEqual(expectedSize, target.Size);
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
      Assert.AreEqual(expected, actual);
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
      Assert.AreEqual(expectedHead, target.Head);
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
      Assert.AreEqual(expected, target.Head);
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
      CollectionAssert.AreEqual(expected, actual);
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
      CollectionAssert.AreEqual(expected, actual);
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
      CollectionAssert.AreEqual(expected, actual);
    }

    #endregion

    #region Test Helpers

    private Random _random;

    [TestFixtureTearDown]
    public void CleanUp()
    {
      _random = null;
    }

    [TestFixtureSetUp]
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
  }
}
