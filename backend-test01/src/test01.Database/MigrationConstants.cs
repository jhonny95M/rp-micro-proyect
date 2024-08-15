using System.Diagnostics.CodeAnalysis;

namespace test01.Database
{
    [ExcludeFromCodeCoverage]
    public static class MigrationConstants
    {
        public static readonly string[] Sources = { "Migrations/Tables", "Migrations/Procedures" };
        public const string ScriptPrefix = "V";
        public const string RepeatableScriptPrefix = "R";
        public static readonly string[] Commands = { "migrate", "repair", "info" };
        public const string Strategy = "each";
        public const string ConnectionStringKey = "TESTAPP-DB";

    }
}
