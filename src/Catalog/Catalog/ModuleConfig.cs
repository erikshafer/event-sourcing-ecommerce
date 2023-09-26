using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Eventuous;

namespace Catalog;

internal static class ModuleConfig
{
    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255", MessageId = "The \'ModuleInitializer\' attribute should not be used in libraries")]
    internal static void InitializeModule() => TypeMap.RegisterKnownEventTypes();
}
