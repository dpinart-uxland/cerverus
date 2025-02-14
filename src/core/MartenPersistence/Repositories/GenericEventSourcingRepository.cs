﻿using Cerverus.Core.Domain;
using Marten;

namespace Cerverus.Core.MartenPersistence.Repositories;

public class GenericEventSourcingRepository(IDocumentSession session): IGenericRepository
{
    public async Task<TAggregateRoot?> Rehydrate<TAggregateRoot>(string id, long? version=null)
        where TAggregateRoot : AggregateRoot, new()
    {
        var events = await session.Events.FetchStreamAsync(id, version ?? 0);
        if(events.Count == 0)
            return null;
        var aggregate = new TAggregateRoot
        {
            Id = id
        };
        foreach (var @event in events)
            aggregate.ApplyEvent((IDomainEvent)@event.Data);
        return aggregate;
    }
    
    public Task Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot, new()
    {
        session.Events.Append(aggregateRoot.Id, aggregateRoot.GetUncommittedEvents());
        return Task.CompletedTask;
    }
    
    public Task Create<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot, new()
    {
        session.Events.StartStream<TAggregateRoot>(aggregateRoot.Id, aggregateRoot.GetUncommittedEvents());
        return Task.CompletedTask;
    }
}