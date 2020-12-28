dotnet build "Armazenagem3L-API.sln" /p:DebugType=Full
SonarScanner.MSBuild.exe begin /k:"alexandria-armazem3l" /d:sonar.host.url="https://sonar.dtidigital.com.br" /d:sonar.login="cbbb1e84c9ef06b135cc391889e18605e73adc48" /d:sonar.cs.opencover.reportsPaths=results.xml /d:sonar.coverage.exclusions=**/Controllers/*.cs,**/Models/*.cs,**/Data/*.cs,**/ExceptionHandler/*.cs,**/Logger/*.cs,**/Logger/Impl/*.cs,**/Repositories/*.cs,**/Repositories/impl/*.cs,**/Migrations/*.cs,**/Program.cs,**/Startup.cs
MsBuild.exe /t:Rebuild
OpenCover.Console.Exe -target:"c:\program files\dotnet\dotnet.exe" -targetargs:"test" -oldStyle -register:user -filter:"+[*]Armazenagem3L_API.Services* +[*]Armazenagem3L_API.Util*"
SonarScanner.MSBuild.exe end /d:sonar.login="cbbb1e84c9ef06b135cc391889e18605e73adc48"