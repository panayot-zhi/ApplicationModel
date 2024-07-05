// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { IQueryFor, QueryResultWithState, QueryResult, Paging, Sorting } from '@cratis/applications/queries';
import { Constructor } from '@cratis/fundamentals';
import { useState, useEffect } from 'react';

/**
 * Delegate type for performing a {@link IQueryFor} in the context of the {@link useQuery} hook.
 */
export type PerformQuery<TArguments = {}> = (args?: TArguments) => Promise<void>;

/**
 * Delegate type for setting the sorting in the context of the {@link useQuery} and {@link useQueryWithPaging} hooks.
 */
export type SetSorting = (sorting: Sorting) => Promise<void>;

/**
 * Delegate type for setting the page in the context of the {@link useQueryWithPaging} hook.
 */
export type SetPage = (page: number) => Promise<void>;

type QueryPerformer<TQuery extends IQueryFor<TDataType>, TDataType> = (performer: TQuery) => Promise<QueryResult<TDataType>>;

function useQueryInternal<TDataType, TQuery extends IQueryFor<TDataType>, TArguments = {}>(query: Constructor<TQuery>, performer: QueryPerformer<TQuery, TDataType>, sorting?: Sorting, paging?: Paging, args?: TArguments):
    [QueryResultWithState<TDataType>, number, PerformQuery<TArguments>, SetSorting, SetPage] {
    sorting ??= Sorting.none;
    const initialQueryInstance = new query() as TQuery;
    initialQueryInstance.paging = paging!;
    initialQueryInstance.sorting = sorting;

    const [queryInstance, setQueryInstance] = useState(initialQueryInstance);
    const [result, setResult] = useState<QueryResultWithState<TDataType>>(QueryResultWithState.initial(queryInstance.defaultValue));

    const queryExecutor = (async (args?: TArguments) => {
        const queryResult = await performer(queryInstance);
        setResult(oldResult => {
            oldResult = QueryResultWithState.fromQueryResult(queryResult, false);
            return oldResult;
        });
        
    });

    useEffect(() => {
        queryExecutor(args);
    }, []);

    return [
        result,
        queryInstance.paging?.page || 0,
        async (args?: TArguments) => {
            setResult(QueryResultWithState.fromQueryResult(result, true));
            await queryExecutor(args);
        },
        async (sorting: Sorting) => {
            setResult(QueryResultWithState.fromQueryResult(result, true));
            queryInstance.sorting = sorting;
            setQueryInstance({
                ...queryInstance,
                sorting
            });
            await queryExecutor(args);
        },
        async (page: number) => {
            setResult(QueryResultWithState.fromQueryResult(result, true));
            queryInstance.paging = {page, pageSize: queryInstance.paging?.pageSize ?? 0};
            setQueryInstance(queryInstance);
            await queryExecutor(args);
        }];
}

/**
 * React hook for working with {@link IQueryFor} within the state management of React.
 * @template TDataType Type of model the query is for.
 * @template TQuery Type of query to use.
 * @template TArguments Optional: Arguments for the query, if any
 * @param query Query type constructor.
 * @returns Tuple of {@link QueryResult} and a {@link PerformQuery} delegate.
 */
export function useQuery<TDataType, TQuery extends IQueryFor<TDataType>, TArguments = {}>(query: Constructor<TQuery>, args?: TArguments, sorting?: Sorting):
    [QueryResultWithState<TDataType>, PerformQuery<TArguments>, SetSorting] {
    const [result, _, perform, setSorting] = useQueryInternal(query, async (queryInstance: TQuery) => await queryInstance.perform(args!), sorting, undefined, args);
    return [result, perform, setSorting];
}

/**
 * React hook for working with {@link IQueryFor} within the state management of React for queries with paging.
 * @template TDataType Type of model the query is for.
 * @template TQuery Type of query to use.
 * @template TArguments Optional: Arguments for the query, if any
 * @param query Query type constructor.
 * @returns Tuple of {@link QueryResult} and a {@link PerformQuery} delegate.
 */
export function useQueryWithPaging<TDataType, TQuery extends IQueryFor<TDataType>, TArguments = {}>(query: Constructor<TQuery>, paging: Paging, args?: TArguments, sorting?: Sorting):
    [QueryResultWithState<TDataType>, number, PerformQuery<TArguments>, SetSorting, SetPage] {
    return useQueryInternal(query, async (queryInstance: TQuery) => await queryInstance.perform(args!), sorting, paging, args);
}
