@echo off
rem Compila Divisor.cs a divisor.exe con el compilador de C# incluido en Windows.
"%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /optimize /codepage:65001 /out:"%~dp0divisor.exe" "%~dp0Divisor.cs"
