FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

#problem with excel com not registred
#https://git.chemsorly.com/Docker/Msbuilder/blob/nightly/dockerfiles/msbuild-vsc.Dockerfile
#RUN powershell -Command Invoke-WebRequest "https://download.microsoft.com/download/F/B/A/FBAB6866-71F8-4A3F-89A4-5BC6AB035C62/vstor_redist.exe" -OutFile "$env:TEMP\vstor_redist.exe" -UseBasicParsing  

# copy csproj and restore as distinct layers
COPY CSharp.Data.Client/*.sln ./CSharp.Data.Client/
COPY CSharp.Data.Client/CSharp.Data.Client/*.csproj ./CSharp.Data.Client/CSharp.Data.Client/
COPY CSharp.Data.Client/CSharp.Data.Client/*.config ./CSharp.Data.Client/CSharp.Data.Client/
RUN nuget restore CSharp.Data.Client/CSharp.Data.Client.sln

COPY *.sln .
COPY CSharp.Financial.Stocks.Task/*.csproj ./CSharp.Financial.Stocks.Task/
COPY CSharp.Financial.Stocks.Task/*.config ./CSharp.Financial.Stocks.Task/
RUN nuget restore

# copy everything else and build app
COPY . ./
WORKDIR /app
RUN msbuild /p:Configuration=Release

CMD ".\\CSharp.Financial.Stocks.Task\\bin\\Release\\CSharp.Financial.Stocks.Task.exe"

#docker run -it --volume=c:\:c:\test vinils/csharp-saude-fitbittask cmd
#net use z: \\servernameOrIp\z$ /user:Administrator Password
