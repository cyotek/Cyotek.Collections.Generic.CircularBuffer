Change Log
==========

1.1.0-alpha
-----------
### Added
* Added `GetLast` method to complement `PeekLast`, allowing the class to be used as a last-in, first-out stack without reflection hacks.

1.0.1
-----
### Changed
* Added `AssemblyInformationalVersion` attribute so nuget "just works"

### Fixed
* After putting array of the same size as buffer in it, next single value
put throws `IndexOutOfRangeException`.

1.0.0
-----

* Initial release
