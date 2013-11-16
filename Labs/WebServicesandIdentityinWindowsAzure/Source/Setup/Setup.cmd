@ECHO off
%~d0
CD "%~dp0"

ECHO Install Visual Studio 2010 Code Snippets for the lab:
ECHO -------------------------------------------------------------------------------
CALL .\Scripts\InstallCodeSnippets.cmd
ECHO Done!
ECHO.
ECHO *******************************************************************************
ECHO.

CD "%~dp0"
ECHO Setup Certificates:
ECHO -------------------------------------------------------------------------------
CALL .\Scripts\SetupCertificates.cmd
ECHO Done!
ECHO.
ECHO *******************************************************************************
ECHO.

CD "%~dp0"
ECHO Compile BlobsExplorer:
ECHO -------------------------------------------------------------------------------
CALL .\Scripts\BuildAssets.cmd
ECHO Done!
ECHO.
ECHO *******************************************************************************
ECHO.

CD "%~dp0"
ECHO This lab requires you to enable LoadUserProfile in the DefaultAppPool and ASP.NET v4.0 application pools. 
ECHO.
CHOICE /M "Do you want to enable LoadUserProfile" 
IF ERRORLEVEL 2 GOTO End
IF ERRORLEVEL 1 GOTO Continue
:Continue
CALL .\Scripts\enableUserProfile.cmd
ECHO Done!
ECHO.
ECHO *******************************************************************************
ECHO.
:End

@PAUSE
