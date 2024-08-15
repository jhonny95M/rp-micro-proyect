using System.Diagnostics.CodeAnalysis;

namespace test01.Application
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public const int DefaultPageSize = 50;
    }
    public static class LogConstants
    {
        public const string LogErrorMessage = "Error al ejecutar {0}";
    }
}
