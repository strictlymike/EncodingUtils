@ECHO OFF
SET CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe

FOR %%f IN (*.cs) DO "%CSC%" "%%f"
