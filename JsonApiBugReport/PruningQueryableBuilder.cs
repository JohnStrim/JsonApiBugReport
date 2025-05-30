using System;
using System.Linq;
using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Queries.QueryableBuilding;
using Microsoft.Extensions.Logging;

#nullable enable

namespace JsonApiBugReport;

public class PruningQueryableBuilder(
    IIncludeClauseBuilder includeClauseBuilder,
    IWhereClauseBuilder whereClauseBuilder,
    IOrderClauseBuilder orderClauseBuilder,
    ISkipTakeClauseBuilder skipTakeClauseBuilder,
    ISelectClauseBuilder selectClauseBuilder,
    IJsonApiOptions options,
    ILogger<PruningQueryableBuilder> logger)
    : QueryableBuilder(includeClauseBuilder, whereClauseBuilder, orderClauseBuilder, skipTakeClauseBuilder,
        selectClauseBuilder)
{
    public override Expression ApplyQuery(QueryLayer layer, QueryableBuilderContext context)
    {
        ArgumentNullException.ThrowIfNull(layer);

        Prune(layer);

        var expression = base.ApplyQuery(layer, context);
        var text = expression.ToReadableString();

        if (text.StartsWith("[Microsoft.EntityFrameworkCore.Query.EntityQueryRootExpression]"))
        {
            logger.LogInformation("Expression (after prune): {Expression}", text);
        }

        return expression;
    }

    private void Prune(QueryLayer queryLayer)
    {
        if (queryLayer.Selection != null)
        {
            foreach (var resourceType in queryLayer.Selection.GetResourceTypes().ToArray())
            {
                var selectors = queryLayer.Selection.GetOrCreateSelectors(resourceType);

                foreach (var (field, subLayer) in selectors)
                {
                    if (subLayer != null)
                    {
                        Prune(subLayer);

                        if (IsDefault(subLayer))
                        {
                            selectors.Remove(field);
                        }
                    }
                }
            }

            if (queryLayer.Selection.IsEmpty)
            {
                queryLayer.Selection = null;
            }
        }
    }

    private bool IsDefault(QueryLayer queryLayer)
    {
        bool hasDefaultSort = queryLayer.Sort != null && queryLayer.Sort.ToString() == "id";

        bool hasDefaultPagination = queryLayer.Pagination != null &&
                                    Equals(queryLayer.Pagination.PageNumber, PageNumber.ValueOne) &&
                                    Equals(queryLayer.Pagination.PageSize, options.DefaultPageSize);

        var isDefault = queryLayer.Include == null && queryLayer.Filter == null &&
                        (queryLayer.Sort == null || hasDefaultSort) &&
                        (queryLayer.Pagination == null || hasDefaultPagination) && queryLayer.Selection == null;

        return isDefault;
    }
}