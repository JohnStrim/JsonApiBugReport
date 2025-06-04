using JsonApiBugReport.Data;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries.Expressions;
using JsonApiDotNetCore.Resources;

#nullable enable

namespace JsonApiBugReport.ResourceDefinitions;

public sealed class UnitGroupDefinition(IResourceGraph resourceGraph)
    : JsonApiResourceDefinition<UnitGroup, int>(resourceGraph)
{
    private static readonly PaginationExpression NoPagination = new(PageNumber.ValueOne, null);

    public override PaginationExpression OnApplyPagination(PaginationExpression? existingPagination)
    {
        return NoPagination;
    }
}