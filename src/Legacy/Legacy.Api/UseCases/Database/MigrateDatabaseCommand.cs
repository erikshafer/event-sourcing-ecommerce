using MediatR;

namespace Legacy.Api.UseCases.Database;

public record MigrateDatabaseCommand : IRequest<MigrateDataResponse>
{
    public static Func<IMediator, Task<MigrateDataResponse>> EndpointHandler
        => mediator
            => mediator.Send(new MigrateDatabaseCommand());
}
