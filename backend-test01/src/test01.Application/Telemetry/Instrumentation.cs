using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace test01.Application.Telemetry
{
    [ExcludeFromCodeCoverage]
    public class Instrumentation : IInstrumentation
    {
        //public Counter<long> RoleCreated { get; }

        public Instrumentation(Meter meter)
        {
            if (meter is null)
            {
                throw new ArgumentNullException(nameof(meter));
            }

            //RoleCreated = meter.CreateCounter<long>("test_app_role_created");
        }
    }
}
