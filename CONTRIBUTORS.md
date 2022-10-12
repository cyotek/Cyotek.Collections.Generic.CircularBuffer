# Contributors

I'm grateful to the following people who have helped improve
this library

## 1.2

* **[mmano](https://github.com/mmano)** for discovering and
  providing a correction for a crash that could occur when
  calling `PeekLast` after the buffer had wrapped
* **[almazik](https://github.com/almazik)** for discovering and
  providing a correction for a data corruption issue when
  changing the `Capacity` property on a populated buffer
* **[dennisverheijen](https://github.com/dennisverheijen)** for
  discovering a crash when using `Get` to retrieve the entire
  contents of the buffer

## 1.1

* **[vbfox](https://github.com/vbfox)** and
  **[x0nn](https://github.com/x0nn)** for adding support for
  .NET Standard and .NET Core

## 1.0.1

* **[DomasM](https://github.com/DomasM)** for contributing a fix
  for an `IndexOutOfRangeException` which would occur the next
  call to `Put` after previously putting an array the size of
  buffer.
