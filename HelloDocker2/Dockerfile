# FROM microsoft/aspnetcore
# WORKDIR /app
# COPY bin/Debug/netcoreapp2.0/publish .
# ENTRYPOINT ["dotnet", "HelloDocker2.dll"]


# Build Stage
FROM microsoft/aspnetcore-build as build-env
WORKDIR /hellodocker-2
COPY HelloDocker2.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -o /publish

# Runtime Image Stage
FROM microsoft/aspnetcore
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT [ "dotnet", "HelloDocker2.dll" ]