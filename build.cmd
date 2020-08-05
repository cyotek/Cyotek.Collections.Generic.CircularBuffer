@ECHO OFF

SETLOCAL

CALL ..\..\..\build\set35vars.bat

SET RELDIR=src\bin\Release\
SET PRJFILE=src\Cyotek.Collections.Generic.CircularBuffer.csproj

IF EXIST %RELDIR%*.nupkg DEL /F %RELDIR%*.nupkg
IF EXIST %RELDIR%*.snupkg DEL /F %RELDIR%*.snupkg

dotnet build %PRJFILE% --configuration Release
CALL signcmd %RELDIR%net40\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%net452\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%net462\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%net472\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%net48\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%netcoreapp2.1\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%netcoreapp2.2\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%netcoreapp3.1\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%netstandard2.0\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd %RELDIR%netstandard2.1\Cyotek.Collections.Generic.CircularBuffer.dll
dotnet pack %PRJFILE% --configuration Release --no-build
CALL sign-package %RELDIR%*.nupkg
CALL sign-package %RELDIR%*.snupkg

ENDLOCAL
