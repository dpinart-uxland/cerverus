﻿using Cerverus.BackOffice.Features.Captures.CaptureSnapshots;

namespace Cerverus.Api.Bootstrap;

public static class ConfigurationBootstrapper
{
    public static IServiceCollection SetupConfigurations(this IServiceCollection services, IConfiguration configuration)
    {

            services.Configure<SnapshotCaptureSettings>(configuration.GetSection("SnaphotCaptures"));

            // existing code...
        return services;
    }
    
}