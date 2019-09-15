FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

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
