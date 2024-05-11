﻿using Cerverus.Core.MartenPersistence.QueryProviders;
using Cerverus.Features.Features.OrganizationalStructure.Camera;
using Marten;

namespace Cerverus.BackOffice.Persistence.QueryProviders;

internal class CameraQueryProvider(IQuerySession querySession) : QueryProvider<Camera>(querySession), ICameraQueryProvider;