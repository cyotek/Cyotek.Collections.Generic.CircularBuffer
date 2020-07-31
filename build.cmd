@ECHO OFF

SETLOCAL

CALL ..\..\..\build\set35vars.bat

REM %msbuildexe% Cyotek.Collections.Generic.CircularBuffer.sln /p:Configuration=Release /verbosity:minimal /nologo /t:Clean,Build
REM CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\Cyotek.Collections.Generic.CircularBuffer.dll

dotnet build Cyotek.Collections.Generic.CircularBuffer\Cyotek.Collections.Generic.CircularBuffer.csproj --configuration Release
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\net40\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\net452\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\net462\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\net472\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\net48\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\netcoreapp2.1\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\netcoreapp2.2\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\netcoreapp3.1\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\netstandard2.0\Cyotek.Collections.Generic.CircularBuffer.dll
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\netstandard2.1\Cyotek.Collections.Generic.CircularBuffer.dll
dotnet pack Cyotek.Collections.Generic.CircularBuffer/Cyotek.Collections.Generic.CircularBuffer.csproj --configuration Release --no-build

REM PUSHD

REM CD releases

REM NUGET pack ..\Cyotek.Collections.Generic.CircularBuffer\Cyotek.Collections.Generic.CircularBuffer.csproj -Prop Configuration=Release

REM %zipexe% a -bd -tZip Cyotek.Collections.Generic.CircularBuffer.x.x.x.x.zip ..\Cyotek.Collections.Generic.CircularBuffer\bin\Release\Cyotek.Collections.Generic.CircularBuffer.dll

REM POPD

ENDLOCAL
