@ECHO OFF

SETLOCAL

CALL %CTKBLDROOT%\setupEnv.cmd

SET BASENAME=Cyotek.Collections.Generic.CircularBuffer
SET RELDIR=src\bin\Release\
SET PRJFILE=src\%BASENAME%.csproj
SET DLLNAME=%BASENAME%.dll

IF EXIST %RELDIR%*.nupkg DEL /F %RELDIR%*.nupkg
IF EXIST %RELDIR%*.snupkg DEL /F %RELDIR%*.snupkg

dotnet build %PRJFILE% --configuration Release

CALL signcmd %RELDIR%net462\%DLLNAME%
CALL signcmd %RELDIR%net8.0\%DLLNAME%
CALL signcmd %RELDIR%netstandard2.0\%DLLNAME%

dotnet pack %PRJFILE% --configuration Release --no-build

CALL sign-package %RELDIR%*.nupkg
CALL sign-package %RELDIR%*.snupkg

ENDLOCAL
