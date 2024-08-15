using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Wolverine;

namespace test01.Application
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrap
    {
        public static void RegisterSecurityTypesForWolverine(this WolverineOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);
            options.Discovery.IncludeAssembly(typeof(Bootstrap).Assembly);
        }

        public static void LoadEnvironmentVariables()
        {
            var root = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(root, ".env");

            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }

}
