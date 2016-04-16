@ECHO OFF

SETLOCAL

CALL ..\..\..\build\set35vars.bat

%msbuildexe% Cyotek.Collections.Generic.CircularBuffer.sln /p:Configuration=Release /verbosity:minimal /nologo /t:Clean,Build
CALL signcmd Cyotek.Collections.Generic.CircularBuffer\bin\Release\Cyotek.Collections.Generic.CircularBuffer.dll

PUSHD

CD releases

NUGET pack ..\Cyotek.Collections.Generic.CircularBuffer\Cyotek.Collections.Generic.CircularBuffer.csproj -Prop Configuration=Release

%zipexe% a -bd -tZip Cyotek.Collections.Generic.CircularBuffer.x.x.x.x.zip ..\Cyotek.Collections.Generic.CircularBuffer\bin\Release\Cyotek.Collections.Generic.CircularBuffer.dll

POPD

ENDLOCAL
