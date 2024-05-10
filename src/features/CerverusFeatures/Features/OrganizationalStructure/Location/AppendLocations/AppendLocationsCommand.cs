﻿using Cerverus.Core.Domain;
using Cerverus.Features.Features.OrganizationalStructure.Shared;

namespace Cerverus.Features.Features.OrganizationalStructure.Location.AppendLocations;

public record AppendHierarchyItem(
    HierarchicalItemType Type,
    string Id,
    string? ParentId,
    string Description,
    CameraAdminSettings? DefaultCameraAdminSettings,
    CameraFunctionalSettings? DefaultCameraFunctionalSettings
): ICommand;

public record AppendHierarchyItems(
    List<AppendHierarchyItem> Items
    ): ICommand;
    
public record SetupLocation(string Id,
    string? ParentId,
    string Description,
    CameraAdminSettings? DefaultCameraAdminSettings,
    CameraFunctionalSettings? DefaultCameraFunctionalSettings
    ): ICommand, IHierarchyItem;