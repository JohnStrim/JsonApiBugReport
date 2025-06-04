using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries.Expressions;
using JsonApiDotNetCore.Resources;
using System.Linq;

namespace JsonApiBugReport.ResourceDefinitions;

public class CommonDefinition<TResource, TId>(IResourceGraph resourceGraph,
    JsonApiDotNetCore.Queries.IEvaluatedIncludeCache includeCache)
    : JsonApiResourceDefinition<TResource, TId>(resourceGraph)
    where TResource : class, IIdentifiable<TId>
{
    private static readonly PaginationExpression NoPagination = new(PageNumber.ValueOne, null);
    public override PaginationExpression OnApplyPagination(PaginationExpression existingPagination)
    {
        var expr = includeCache.Get();

        if (expr?.Elements == null)
            return base.OnApplyPagination(existingPagination);

        if (expr.Elements.Any(CheckIfPaginationAppliedInInclude))
            return NoPagination;

        return base.OnApplyPagination(existingPagination);
    }

    private bool CheckIfPaginationAppliedInInclude(IncludeElementExpression include)
    {
        if (include.Relationship.RightType.ClrType == typeof(TResource))
            return true;
        else if (include.Children == null)
            return false;

        return include.Children.OfType<IncludeElementExpression>()
            .Any(CheckIfPaginationAppliedInInclude);
    }
}
