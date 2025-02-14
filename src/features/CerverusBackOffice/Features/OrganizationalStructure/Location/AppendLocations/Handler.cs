﻿using Cerverus.BackOffice.Features.OrganizationalStructure.Shared;
using Cerverus.BackOffice.Features.Shared;
using Cerverus.Core.Domain;
using Wolverine;

namespace Cerverus.BackOffice.Features.OrganizationalStructure.Location.AppendLocations;

public sealed class Handler(IRepository<OrganizationalStructure.Location.Location> locationRepository, IMessageBus bus, HierarchySetupCommandFactory commandFactory) :
    IRepositoryHandlerMixin<OrganizationalStructure.Location.Location>
{
    public async Task Handle(AppendHierarchyItems request, CancellationToken cancellationToken = default)
    {
        var sortedItems = request.Items.OrderBy(x => x, new AppendLocationCommandSorter(request.Items))
            .Select(commandFactory.Produce)
            .ToList();
        foreach (var item in sortedItems)
          await bus.InvokeAsync(item, cancellationToken);
    }
    private class AppendLocationCommandSorter(IEnumerable<AppendHierarchyItem> allItems): IComparer<AppendHierarchyItem>
    {
        public int Compare(AppendHierarchyItem? x, AppendHierarchyItem? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            if(string.IsNullOrEmpty(x.ParentId)) return -1;
            if(string.IsNullOrEmpty(y.ParentId)) return 1;
            if (x.ParentId == y.ParentId) return 0;
            if(!IsParentInList(x)) return -1;
            if(!IsParentInList(y)) return 1;
            if (x.ParentId == y.Id) return 1;
            if (y.ParentId == x.Id) return -1;
            return 0;
        }
        
        private bool IsParentInList(AppendHierarchyItem parent)
        {
            return allItems.Any(x => x.Id == parent.ParentId);
        }
    }

    public IRepositoryBase<OrganizationalStructure.Location.Location> Repository => locationRepository;
}

public sealed class SetupLocationHandler(
    IRepository<OrganizationalStructure.Location.Location> locationRepository,
    IHierarchyItemPathProvider pathProvider)
{
    public async Task Handle(SetupLocation request)
    {
        var path = await pathProvider.GetPathAsync(request);
        var location = await locationRepository.Rehydrate(request.Id);
        await (location == null ? CreateLocation(request, path) : UpdateLocation(location, request, path));
    }

    private Task CreateLocation(SetupLocation setupLocation, string path)
    {
        var location = new OrganizationalStructure.Location.Location(setupLocation, path);
        return locationRepository.Create(location);
    }

    private Task UpdateLocation(OrganizationalStructure.Location.Location location, SetupLocation setupLocation, string path)
    {
        location.Handle(setupLocation, path);
        return locationRepository.Save(location);
    }
}