﻿using Marten;
using Weasel.Core;

namespace Cerverus.Core.MartenPersistence;

public class MartenDbFixture : IDisposable
{
    private readonly IDocumentStore _documentStore;

    public MartenDbFixture(string connectionString)
    {
        _documentStore = CreateStore(connectionString);
        DocumentSession = _documentStore.LightweightSession();
    }

    public IDocumentSession DocumentSession { get; init; }

    public void Dispose()
    {
        _documentStore.Dispose();
        DocumentSession.Dispose();
    }

    private IDocumentStore CreateStore(string connectionString)
    {
        return DocumentStore.For(options =>
        {
            options.Connection(connectionString);
            options.AutoCreateSchemaObjects = AutoCreate.All;
            options
                .SetupSerialization()
                .ConfigureEventSore();
        });
    }
}