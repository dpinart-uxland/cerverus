﻿using Cerverus.Core.Domain;
using NodaTime;

namespace Cerverus.Maintenance.Features.Features.Issues;

public static class StartIssueResolutionHandler
{
    public static async Task Handle(StartIssueResolution startIssueResolution, IRepository<MaintenanceIssue> repository)
    {
        var issue = await repository.RehydrateOrThrow(startIssueResolution.IssueId);
        issue.Start(SystemClock.Instance.GetCurrentInstant(), "MaintenanceUser");
        await repository.Save(issue);
    }
}