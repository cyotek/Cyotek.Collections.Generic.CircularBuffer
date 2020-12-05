@ECHO OFF

SETLOCAL

CALL ..\..\..\build\set35vars.bat

SET BASENAME=Cyotek.Collections.Generic.CircularBuffer
SET RELDIR=src\bin\Release\
SET PRJFILE=src\%BASENAME%.csproj
SET DLLNAME=%BASENAME%.dll

IF EXIST %RELDIR%*.nupkg DEL /F %RELDIR%*.nupkg
IF EXIST %RELDIR%*.snupkg DEL /F %RELDIR%*.snupkg

dotnet build %PRJFILE% --configuration Release

CALL signcmd %RELDIR%net35\%DLLNAME%
CALL signcmd %RELDIR%net40\%DLLNAME%
CALL signcmd %RELDIR%net452\%DLLNAME%
CALL signcmd %RELDIR%net462\%DLLNAME%
CALL signcmd %RELDIR%net472\%DLLNAME%
CALL signcmd %RELDIR%net48\%DLLNAME%
CALL signcmd %RELDIR%netcoreapp2.1\%DLLNAME%
CALL signcmd %RELDIR%netcoreapp2.2\%DLLNAME%
CALL signcmd %RELDIR%netcoreapp3.1\%DLLNAME%
CALL signcmd %RELDIR%netstandard2.0\%DLLNAME%
CALL signcmd %RELDIR%netstandard2.1\%DLLNAME%

dotnet pack %PRJFILE% --configuration Release --no-build

CALL sign-package %RELDIR%*.nupkg
CALL sign-package %RELDIR%*.snupkg

ENDLOCAL
