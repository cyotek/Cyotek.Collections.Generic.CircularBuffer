# Change Log

## 2.0.0

### Removed

* Removed obsolete build targets: net35, net40, net452, net472,
  netcoreapp2.1, netcoreapp2.2, netcoreapp3.1, netstandard2.1

## 1.2.2

### Fixed

* After overwriting items, `PeekLast` could crash (#20)

## 1.2.1

### Fixed

* Changing the `Capacity` property with a populated buffer lead
  to data corruption (#18)

## 1.2.0

### Fixed

* Calling `Get` overloads that returned array didn't always wrap
  `Head` properly (#17)

## 1.1.0-alpha6

### Added

* Reinstated .NET 3.5 support

## 1.1.0-alpha5

### Added

* NuGet package is now signed

## 1.1.0-alpha4

### Fixed

* The `CopyTo` overload that specified a range crashed if an
  starting offset was used that pushed it beyond the capacity of
  the buffer

## 1.1.0-alpha3

### Fixed

* The `CopyTo` overload that specified a range used direct
  indexes into the source buffer instead of offsetting from
  `Head`

## 1.1.0-alpha2

### Added

* Added `PeekAt` method to allow items to be retrieved, but not
  removed, from anywhere in the buffer
* Added `GetLast` overloads that allow multiple items to be
  retrieved and removed with a single call
* Added `PeekLast` overloads that allow multiple items to be
  retrieved, but not removed with a single call
* Added several new build targets, including .NET Standard and
  .NET Core, in addition to several legacy Framework

### Changed

* Converted project to SDK style
* Build process now uses `dotnet.exe`

### Removed

* The `Size` property is no longer writable

## 1.1.0-alpha

### Added

* Added `GetLast` method to complement `PeekLast`, allowing the
  class to be used as a last-in, first-out stack without
  reflection hacks.

## 1.0.1

### Changed

* Added `AssemblyInformationalVersion` attribute so NuGet "just
  works"

### Fixed

* After putting array of the same size as buffer in it, next
  single value put throws `IndexOutOfRangeException`.

## 1.0.0

* Initial release
