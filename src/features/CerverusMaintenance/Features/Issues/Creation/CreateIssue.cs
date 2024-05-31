﻿using Cerverus.Core.Domain;
using Cerverus.Features.Features.Captures;
using Cerverus.Maintenance.Features.Features.Analysis;
using Cerverus.Maintenance.Features.Features.Shared;

namespace Cerverus.Maintenance.Features.Features.Issues;

public record CreateIssue(
    string MaintenanceProcessId,
    CaptureInfo CaptureInfo,
    CaptureError? CaptureError,
    List<FilterResult> Errors) : ICommand;