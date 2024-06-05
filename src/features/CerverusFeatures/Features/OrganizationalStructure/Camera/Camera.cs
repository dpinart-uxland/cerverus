﻿using Cerverus.Core.Domain;
using Cerverus.Features.Features.OrganizationalStructure.Shared;

namespace Cerverus.Features.Features.OrganizationalStructure.Camera;

public partial class Camera: AggregateRoot, IOrganizationStructureItem
{
    public Camera(){}
    public string Path { get; set; }
    public string ParentId { get; set; }
    public string Description { get; set; }
    public CameraAdminSettings? AdminSettings { get; set; }
    public CameraFunctionalSettings? FunctionalSettings { get; set; }
}

public interface ICameraEntityQueryProvider : IEntityQueryProvider<Camera>
{
    Task<IEnumerable<string>> GetCameraIdsByPath(string path);
}