﻿using Cerverus.BackOffice.Features.OrganizationalStructure.Camera.SetupCamera;
using Cerverus.BackOffice.Features.OrganizationalStructure.Location.AppendLocations;
using Cerverus.BackOffice.Features.OrganizationalStructure.Shared;
using Cerverus.Core.Domain;

namespace Cerverus.BackOffice.Features.OrganizationalStructure.HierarchyItems;

public record HierarchyItem(
    string Id,
    string? ParentId,
    string Description,
    string Path,
    HierarchicalItemType Type
): IEntity
{
    public static HierarchyItem Create(LocationCreated locationCreated) =>
        new HierarchyItem(locationCreated.Id, locationCreated.ParentId, locationCreated.Description, locationCreated.Path, HierarchicalItemType.Location);
    
    public static HierarchyItem Create(CameraCreated cameraCreated) =>
        new HierarchyItem(cameraCreated.Id, cameraCreated.ParentId, cameraCreated.Description, cameraCreated.Path, HierarchicalItemType.Camera);
    
    public HierarchyItem Apply(CameraLocationChanged cameraLocationChanged) =>
        this with
        {
            ParentId = cameraLocationChanged.newLocationId,
            Path = cameraLocationChanged.Path
        };
    
    public HierarchyItem Apply(LocationUpdated locationUpdated) =>
        this with
        {
            Description = locationUpdated.Description,
            Path = locationUpdated.Path,
            ParentId = locationUpdated.ParentId
        };
    
    public HierarchyItem Apply(CameraUpdated cameraUpdated) =>
        this with
        {
            Description = cameraUpdated.Description,
            ParentId = cameraUpdated.ParentId,
            Path = cameraUpdated.Path
        };
}

public interface IHierarchyItemEntityQueryProvider: IEntityQueryProvider<HierarchyItem>
{
    Task<IEnumerable<HierarchyItem>> GetItems(string parent);
    Task<IEnumerable<HierarchyItem>> GetAll();
    
}