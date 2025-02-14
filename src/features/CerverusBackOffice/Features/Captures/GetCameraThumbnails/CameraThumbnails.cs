﻿using Cerverus.BackOffice.Features.Captures.CaptureSnapshots;
using Cerverus.Core.Domain;

namespace Cerverus.BackOffice.Features.Captures.GetCameraThumbnails;

public record CameraThumbnails(
    string Id,
    HashSet<string> Thumbnails) : IEntity
{
    public CameraThumbnails Apply(SnapshotCaptured @event)
    {
        return string.IsNullOrEmpty(@event.Settings.ThumbnailPath) ? this : this with
        {
            Thumbnails = new HashSet<string>(Thumbnails)
            {
                @event.Settings.ThumbnailPath!
            }
        };
    }
}

public interface ICameraThumbnailsEntityQueryProvider : IEntityQueryProvider<CameraThumbnails>
{
    
}