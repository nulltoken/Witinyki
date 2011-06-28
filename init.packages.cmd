REM inspired from http://msmvps.com/blogs/theproblemsolver/archive/2011/04/12/nuget-and-projects-under-source-control.aspx

SET BASEDIR=%~dp0

pushd %BASEDIR%

"Lib\NuGet\NuGet.exe" install "Witinyki\packages.config" -o packages
"Lib\NuGet\NuGet.exe" install "Witinyki.Tests\packages.config" -o packages

popd