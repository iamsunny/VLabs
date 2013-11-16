@echo off

echo.
echo ========= Building SecurityTokenVisualizerControl solution =========

set solutionDir="%~dp0..\..\Assets\SecurityTokenVisualizerControl\SecurityTokenVisualizerControl.sln"
set buildType=Release
set verbosity=quiet

IF EXIST %WINDIR%\Microsoft.NET\Framework64 (
	SET msBuildDir=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319
) ELSE (
	SET msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
)

call %msBuildDir%\msbuild %solutionDir% /t:Rebuild /p:Configuration=%buildType% /verbosity:%verbosity%

echo.
echo ========= Installing SecurityTokenVisualizerControl =========

"%~dp0ToolboxInstaller.exe" uninstall "DPE Identity Samples" "SecurityTokenVisualizerControl"
"%~dp0ToolboxInstaller.exe" install "DPE Identity Samples" "%~dp0..\..\Assets\SecurityTokenVisualizerControl\Microsoft.Samples.DPE.Identity.Controls\bin\%buildType%\SecurityTokenVisualizerControl.dll"

REM Copy SecurityTokenVisualizerControl.dll to end solutions (ex2 & ex3)
mkdir "%~dp0..\..\Ex2-EasyAuthorizationWithACS\End\WebSiteACS\Bin"
mkdir "%~dp0..\..\Ex3-CustomSignInExperience\End\WebSiteACS\Bin"

echo.
copy "%~dp0..\..\Assets\SecurityTokenVisualizerControl\Microsoft.Samples.DPE.Identity.Controls\bin\%buildType%\SecurityTokenVisualizerControl.dll" "%~dp0..\..\Ex2-EasyAuthorizationWithACS\End\WebSiteACS\Bin"
copy "%~dp0..\..\Assets\SecurityTokenVisualizerControl\Microsoft.Samples.DPE.Identity.Controls\bin\%buildType%\SecurityTokenVisualizerControl.dll" "%~dp0..\..\Ex3-CustomSignInExperience\End\WebSiteACS\Bin"

echo.
echo Done