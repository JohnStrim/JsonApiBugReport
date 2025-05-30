using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Queries.QueryableBuilding;
using Microsoft.Extensions.Logging;

#nullable enable

namespace JsonApiBugReport;

public class LoggingQueryableBuilder(
    IIncludeClauseBuilder includeClauseBuilder,
    IWhereClauseBuilder whereClauseBuilder,
    IOrderClauseBuilder orderClauseBuilder,
    ISkipTakeClauseBuilder skipTakeClauseBuilder,
    ISelectClauseBuilder selectClauseBuilder,
    ILogger<LoggingQueryableBuilder> logger)
    : QueryableBuilder(includeClauseBuilder, whereClauseBuilder, orderClauseBuilder, skipTakeClauseBuilder,
        selectClauseBuilder)
{
    public override Expression ApplyQuery(QueryLayer layer, QueryableBuilderContext context)
    {
        var expression = base.ApplyQuery(layer, context);
        var text = expression.ToReadableString();

        if (text.StartsWith("[Microsoft.EntityFrameworkCore.Query.EntityQueryRootExpression]"))
        {
            logger.LogInformation("Expression: {Expression}", text);
        }

        return expression;
    }
}
