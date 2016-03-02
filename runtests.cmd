@ECHO OFF

SETLOCAL

SET SCRIPTPATH=%~dp0
SET SCRIPTPATH=%SCRIPTPATH:~0,-1%

CD %SCRIPTPATH%

CALL ..\..\..\build\set35vars.bat

SET RESULTDIR=%1
IF "%RESULTDIR%"=="" SET RESULTDIR=testresults

IF NOT EXIST %RESULTDIR% MKDIR %RESULTDIR%

SET DLLNAME=Cyotek.Collections.Generic.CircularBuffer.Tests
SET SRCDIR=%DLLNAME%\
SET SLNNAME=Cyotek.Collections.Generic.CircularBuffer.sln

%NUGETEXE% restore %SLNNAME%

%MSBUILDEXE% %SLNNAME% %cbbuild%
IF %ERRORLEVEL% NEQ 0 GOTO :testsfailed

%nunitexe% %SRCDIR%bin\release\%DLLNAME%.dll /xml=testresults\%DLLNAME%.xml %nunitargs%
IF %ERRORLEVEL% NEQ 0 GOTO :testsfailed

ENDLOCAL

GOTO :eof

:testsfailed
CECHO {0c}ERROR: *** TEST RUN FAILED ***{#}{\n}
EXIT /b 1
