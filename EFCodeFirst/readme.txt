dotnet ef migrations add Initial --startup-project ../ef-console/ef-console.csproj
dotnet ef migrations add AddEmployeeAddress --startup-project ../ef-console/ef-console.csproj

dotnet ef database update  --startup-project ../ef-console/ef-console.csproj