#nullable enable
using System;
using JsonApiBugReport.Data;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries.Expressions;
using JsonApiDotNetCore.Resources;

namespace JsonApiBugReport.ResourceDefinitions;

public sealed class UnitDefinition(IResourceGraph resourceGraph)
    : JsonApiResourceDefinition<Unit, Guid>(resourceGraph)
{
    private static readonly PaginationExpression NoPagination = new(PageNumber.ValueOne, null);

    public override PaginationExpression OnApplyPagination(PaginationExpression? existingPagination)
    {
        return NoPagination;
    }
}