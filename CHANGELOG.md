Change Log
==========

1.0.3
-----

### Fixed

* The `CopyTo` overload that specified a range used direct
  indexes into the source buffer instead of offsetting from
  `Head`

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
