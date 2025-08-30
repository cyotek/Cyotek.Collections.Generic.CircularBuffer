# ![Icon][iconimg] CircularBuffer&lt;T> Class

[![Build status][ci]][ciimg]
[![NuGet][nugetimg]][nuget]
[![Donate][donateimg]][donate]

![Demo][demoimg]

The `CircularBuffer<T>` class is a data structure that uses a
single, fixed-size buffer that behaves as if it were connected
end-to-end. You can use it as collection of objects with
automatic overwrite support and no array resizing or
allocations. The design of the buffer allows it to be used as
both a first-in, first-out queue, or a first-in, last-out stack.

You can drop the class directly into your projects to use as-is,
or reference the assembly. A NuGet package is [also
available][nuget]. The class has no external dependencies aside from
a reference to `System.dll`.

View the [change log][8] for updates to this library.

## Overwrite support

By default, the contents of the buffer automatically wrap, so
for example if you create a buffer with a maximum capacity of
10, but then attempt to add 11 items, the oldest item in the
buffer will be automatically overwritten.

Alternatively, you can set the `AllowOverwrite` property to
`false`, in which case attempting to add that eleventh item
would throw an exception.

## Performance

The internal buffer of the class is created whenever the
`Capacity` property is set. Generally, this means it will be
created once for the lifetime of the class, unless for some
reason you want to dynamically manipulate the capacity.
Internally, `CircularBuffer<T>` has `Head` and `Tail` properties
which represent the start and end of the buffer, so as you `Put`
and `Get` items, these values will be adjusted accordingly. No
resizing of buffers or reallocation.

> **Note:** Calling the `Clear` method currently also
> reallocates the internal buffer rather than looping all the
> items and setting them to `default(T)`.

The `PeekLast(count)`, `GetLast(count)`, `Get(count)` and
`ToArray` methods will all create and return a new array. With
the exception of `ToArray` (as there is already `CopyTo`), the
other methods have overloads that allow you to specify an
existing array to be populated.

## Using the class

The `CircularBuffer<T>` mostly acts as a FIFO queue. You can use
the `Put` method to put one or more items into the buffer, and
then retrieve one or more items using one of the `Get` methods.
However, you can also use it as FILO stack by using the
`GetLast` method.

> **Note:** When you retrieve an item (or items), references to
> the items still remain in the internal buffer. The `Head` and
> `Size` properties are adjusted so that you'll never get that
> item again no matter what methods you call. I'm not sure yet
> whether that is an acceptable approach, or if I should reset
> the entry to `default(T)`.

To retrieve the next item without removing it from the buffer,
you can use the `Peek` method. Or, to retrieve (again without
removing) the last item in the buffer, you can use `PeekLast`.
To round off peeking, there is also a `PeekAt` method which can
retrieve an item from anywhere in the buffer.

Calling `Get`, `GetLast`, `Peek` or `PeekLast` on an empty
buffer will thrown an exception. You can use `IsEmpty` to check
if these actions will succeed. Similarly, calling `Put` on a
full buffer with overwriting disabled will also throw an
exception. You can use `IsFull` to check if this is the case.

The `Size` property allows you to see how many items you've
added to the buffer. The `Capacity` property returns the maximum
number of items the buffer can hold before the oldest items will
be overwritten.

The `ToArray` method will return all queued items, or you can
use `CopyTo` as a more advanced alternative.

The `CircularBuffer<T>` class implements `IEnumerable<T>` and
`IEnumerable`, so you can happily iterate over the items - this
won't remove them from the buffer. It also implements
`ICollection<T>` and `ICollection` although calling
`ICollection<T>.Remove` is not supported and will throw an
exception.

Finally, the `Clear` method will reset the buffer to an empty
state.

Although I don't think they'll be needed much in real-world use,
the `Head` property represents the internal index of the next
item to be read from the buffer. The `Tail` property represents
the index of the next item to be written.

## Examples

This first example creates a `CircularBuffer<T>`, adds four
items, then retrieves the first item. The comments describe how
the internal state of the buffer changes with each call.

```csharp
  CircularBuffer<string> target;
  string firstItem;
  string[] items;

  target = new CircularBuffer<string>(10); // Creates a buffer for storing up to 10 items
  target.Put("Alpha");                     // Head is 0, Tail is 1, Size is 1
  target.Put("Beta");                      // Head is 0, Tail is 2, Size is 2
  target.Put("Gamma");                     // Head is 0, Tail is 3, Size is 3
  target.Put("Delta");                     // Head is 0, Tail is 4, Size is 4

  firstItem = target.Get();                // firstItem is Alpha. Head is 1, Tail is 4, Size is 3
  items = target.ToArray();                // items are Beta, Gamma, Delta. Head, Tail and Size are unchanged.
```

This second example shows how the buffer will automatically
overwrite the oldest items when full.

```csharp
  CircularBuffer<string> target;
  string firstItem;
  string[] items;

  target = new CircularBuffer<string>(3);  // Creates a buffer for storing up to 3 items
  target.Put("Alpha");                     // Head is 0, Tail is 1, Size is 1
  target.Put("Beta");                      // Head is 0, Tail is 2, Size is 2
  target.Put("Gamma");                     // Head is 0, Tail is 3, Size is 3
  target.Put("Delta");                     // Head is 1, Tail is 1, Size is 3

  firstItem = target.Get();                // firstItem is Beta. Head is 2, Tail is 1, Size is 2
  items = target.ToArray();                // items are Gamma, Delta. Head, Tail and Size are unchanged.
```

This final example shows how the buffer is unchanged when
peeking.

```csharp
  CircularBuffer<string> target;
  string firstItem;
  string lastItem;

  target = new CircularBuffer<string>(10); // Creates a buffer for storing up to 10 items
  target.Put("Alpha");                     // Head is 0, Tail is 1, Size is 1
  target.Put("Beta");                      // Head is 0, Tail is 2, Size is 2
  target.Put("Gamma");                     // Head is 0, Tail is 3, Size is 3
  target.Put("Delta");                     // Head is 0, Tail is 4, Size is 4

  firstItem = target.Peek();               // firstItem is Alpha. Head, Tail and Size are unchanged.
  lastItem = target.PeekLast();            // lastItem is Delta. Head, Tail and Size are unchanged.
```

For more examples, see the test class `CircularBufferTests` as
this has tests which cover all the code paths. Except for
`ICollection.SyncRoot` anyway!

## Requirements

.NET Framework 2.0 or later.

Pre-built binaries are available via a signed [NuGet package][nuget]
containing the following targets.

* .NET 4.6.2
* .NET Standard 2.0
* .NET 8.0

Is there a target not on this list you'd like to see? Raise an
[issue][gitissue], or even better, a [pull request][gitpull].

## Acknowledgements

The `CircularBuffer<T>` class was originally taken from
[Circular Buffer for .NET][4], however I've fixed a number of
bugs and added a few improvements. Unfortunately it didn't occur
to me to keep a list of all the bugs I fixed.

Syntax-wise, I don't remember changing any method signatures so
they should work the same. I did rename the `AllowOverflow`
property to `AllowOverwrite` which seems to make more sense to
me.

The only thing the original has that this version does not is
localization support - the original version read exception
messages from a resource file, whereas here they are just string
literals.

See `CONTRIBUTORS.md` for further details of updates to the
`CircularBuffer<T>` class.

## License

The code is licensed under the New BSD License (BSD) as per the
original source this implementation is based upon. See
`LICENSE.txt` for details.

[ci]: https://ci.appveyor.com/api/projects/status/h7pwdahqmxajsyj7?svg=true
[ciimg]: https://ci.appveyor.com/project/cyotek/cyotek-collections-generic-circularbuffer
[nuget]: https://www.nuget.org/packages/Cyotek.CircularBuffer/
[nugetimg]: https://img.shields.io/nuget/v/Cyotek.CircularBuffer.svg
[donateimg]: https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif
[donate]: https://paypal.me/cyotek
[gitissue]: https://github.com/cyotek/Cyotek.Collections.Generic.CircularBuffer/issues
[gitpull]: https://github.com/cyotek/Cyotek.Collections.Generic.CircularBuffer/pulls
[demoimg]: res/demo.gif
[iconimg]: res/circularbuffer-32x32.png
[4]: http://circularbuffer.codeplex.com/
[8]: CHANGELOG.md
