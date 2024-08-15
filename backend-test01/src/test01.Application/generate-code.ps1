# Rename the file
Rename-Item -Path "Program-cs" -NewName "Program.cs"

# Add packages using dotnet add package
dotnet add package Microsoft.Extensions.DependencyInjection -v 8.0.0
dotnet add package Oakton -v 6.1.0
dotnet add package Microsoft.Extensions.Configuration -v 8.0.0

# Modify the csproj file using PowerShell's text editing capabilities
(Get-Content test01.Application.csproj) -replace '<PropertyGroup>', '<PropertyGroup><OutputType>Exe</OutputType>' | Set-Content test01.Application.csproj

# Remove a directory (equivalent to rm -rf)
Remove-Item -Path "Internal" -Recurse -Force

# Run dotnet run with arguments
dotnet run -- codegen write

# Reverse the csproj modification
(Get-Content test01.Application.csproj) -replace '<PropertyGroup><OutputType>Exe</OutputType>', '<PropertyGroup>' | Set-Content test01.Application.csproj

# Remove packages using dotnet remove package
dotnet remove package Microsoft.Extensions.DependencyInjection
dotnet remove package Oakton
dotnet remove package Microsoft.Extensions.Configuration

# Rename the file back (equivalent to mv)
Rename-Item -Path "Program.cs" -NewName "Program-cs"