using MediatR;

namespace Cerverus.Maintenance.Features.Features.MaintenanceShots.ProduceMaintenanceShot;

public class Handler: IRequestHandler<ProduceMaintenanceShot>
{
    public Task Handle(ProduceMaintenanceShot request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}