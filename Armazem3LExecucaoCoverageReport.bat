dotnet build "Armazenagem3L-API.sln" /p:DebugType=Full
OpenCover.Console.Exe -target:"c:\program files\dotnet\dotnet.exe" -targetargs:"test" -oldStyle -register:user -filter:"+[*]Armazenagem3L_API.Services* +[*]Armazenagem3L_API.Util*"
ReportGenerator.exe -reports:results.xml -targetdir:coverage