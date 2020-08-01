# Change Log

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
