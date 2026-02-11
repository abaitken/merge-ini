@echo off
cd /d "%~dp0"

IF "%1" EQU "-" goto :ready
cmd /D /K %0 -
goto :eof

:ready

for %%i in (.) do SET ABAITKEN.BUILD.PROJECT=%%~nxi
TITLE %ABAITKEN.BUILD.PROJECT%: ABAITKEN Build Environment

echo ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
echo ++                                                        ++
echo ++ ABAITKEN Build Environment                             ++
echo ++                                                        ++
echo ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
echo.
echo.
echo Use the 'dotnet' tools to invoke builds.
echo.
echo 'dotnet build' to compile
echo 'dotnet test' to run tests
echo 'dotnet pack' to package up ready for publishing
echo 'dotnet publish' to publish the results
echo.
