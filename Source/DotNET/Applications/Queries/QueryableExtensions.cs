// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq.Expressions;
using System.Reflection;

namespace Cratis.Applications.Queries;

/// <summary>
/// Provides a set of methods for working with <see cref="IQueryable"/>.
/// </summary>
public static class QueryableExtensions
{
    static readonly MethodInfo _countMethod;
    static readonly MethodInfo _skipMethod;
    static readonly MethodInfo _takeMethod;

    static QueryableExtensions()
    {
        var queryableMethods = typeof(Queryable).GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Static);

        _countMethod = queryableMethods.First(m => m.Name == "Count" && m.GetParameters().Length == 1);
        _skipMethod = queryableMethods.First(m => m.Name == "Skip");
        _takeMethod = queryableMethods.First(m => m.Name == "Take");
    }

    /// <summary>
    /// Returns the number of elements in a sequence.
    /// </summary>
    /// <param name="queryable">The <see cref="IQueryable"/> that contains the elements to be counted.</param>
    /// <returns>The number of elements in the input sequence.</returns>
    public static int Count(this IQueryable queryable)
    {
        var genericMethod = _countMethod.MakeGenericMethod([queryable.ElementType]);
        var countExpression = Expression.Call(null, genericMethod, queryable.Expression);
        return queryable.Provider.Execute<int>(countExpression);
    }

    /// <summary>
    /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
    /// </summary>
    /// <param name="queryable">An <see cref="IQueryable"/> to return elements from.</param>
    /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
    /// <returns>An <see cref="IQueryable"/> that contains elements that occur after the specified index in the input sequence.</returns>
    public static IQueryable Skip(this IQueryable queryable, int count)
    {
        var genericMethod = _skipMethod.MakeGenericMethod([queryable.ElementType]);
        return (genericMethod.Invoke(null, [queryable, count]) as IQueryable)!;
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <param name="queryable">The <see cref="IQueryable"/> to return elements.</param>
    /// <param name="count">The number of elements to return.</param>
    /// <returns>An <see cref="IQueryable"/> that contains the specified number of elements from the start of source.</returns>
    public static IQueryable Take(this IQueryable queryable, int count)
    {
        var genericMethod = _takeMethod.MakeGenericMethod([queryable.ElementType]);
        return (genericMethod.Invoke(null, [queryable, count]) as IQueryable)!;
    }
}
