// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import Handlebars from 'handlebars';
import { ObservableQuerySubscription } from './ObservableQuerySubscription';
import { QueryResult } from './QueryResult';
import { Paging } from './Paging';
import { Sorting } from './Sorting';

/**
 * The delegate type representing the callback of result from the server.
 */
export type OnNextResult<TDataType> = (data: TDataType) => void;

/**
 * Defines the base of a query.
 * @template TDataType Type of model the query is for.
 * @template TArguments Optional type of arguments to use for the query.
 */
export interface IObservableQueryFor<TDataType, TArguments = {}> {
    readonly route: string;
    readonly routeTemplate: Handlebars.TemplateDelegate;
    readonly requestArguments: string[];
    readonly defaultValue: TDataType;

    /**
     * Gets the sorting for the query.
     */
    get sorting(): Sorting;

    /**
     * Sets the sorting for the query.
     */
    set sorting(value: Sorting);

    /**
     * Gets the paging for the query.
     */
    get paging(): Paging | undefined;

    /**
     * Sets the paging for the query.
     */ 
    set paging(value: Paging);

    /**
     * Set the microservice to be used for the query. This is passed along to the server to identify the microservice.
     * @param microservice Name of microservice
     */
    setMicroservice(microservice: string);

    /**
     * Subscribe to the query. This will create a subscription onto the server.
     * @param {OnNextResult} callback The callback that will receive result from the server.
     * @param [args] Optional arguments for the query - depends on whether or not the query needs arguments.
     * @returns {ObservableQuerySubscription<TDataType>}.
     */
    subscribe(callback: OnNextResult<QueryResult<TDataType>>, args?: TArguments): ObservableQuerySubscription<TDataType>;
}
