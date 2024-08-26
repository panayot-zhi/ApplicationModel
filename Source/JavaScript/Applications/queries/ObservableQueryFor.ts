// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { IObservableQueryFor, OnNextResult } from './IObservableQueryFor';
import Handlebars from 'handlebars';
import { ObservableQueryConnection } from './ObservableQueryConnection';
import { ObservableQuerySubscription } from './ObservableQuerySubscription';
import { ValidateRequestArguments } from './ValidateRequestArguments';
import { IObservableQueryConnection } from './IObservableQueryConnection';
import { NullObservableQueryConnection } from './NullObservableQueryConnection';
import { Constructor } from '@cratis/fundamentals';
import { JsonSerializer } from '@cratis/fundamentals';
import { QueryResult } from './QueryResult';
import { Sorting } from './Sorting';
import { Paging } from './Paging';
import { SortDirection } from './SortDirection';
import { Globals } from '../Globals';

/**
 * Represents an implementation of {@link IQueryFor}.
 * @template TDataType Type of data returned by the query.
 */
export abstract class ObservableQueryFor<TDataType, TArguments = {}> implements IObservableQueryFor<TDataType, TArguments> {
    private _microservice: string;
    abstract readonly route: string;
    abstract readonly routeTemplate: Handlebars.TemplateDelegate<any>;
    abstract readonly defaultValue: TDataType;
    abstract get requestArguments(): string[];
    sorting: Sorting;
    paging: Paging | undefined;

    /**
     * Initializes a new instance of the {@link ObservableQueryFor<,>}} class.
     * @param modelType Type of model, if an enumerable, this is the instance type.
     * @param enumerable Whether or not it is an enumerable.
     */
    constructor(readonly modelType: Constructor, readonly enumerable: boolean) {
        this.sorting = Sorting.none;
        this._microservice = Globals.microservice ?? '';
    }

    /** @inheritdoc */
    setMicroservice(microservice: string) {
        this._microservice = microservice;
    }

    /** @inheritdoc */
    subscribe(callback: OnNextResult<QueryResult<TDataType>>, args?: TArguments): ObservableQuerySubscription<TDataType> {
        let actualRoute = this.route;
        let connection: IObservableQueryConnection<TDataType>;
        const connectionQueryArguments: any = {};

        if (this.paging && this.paging.pageSize > 0) {
            connectionQueryArguments.pageSize = this.paging.pageSize;
            connectionQueryArguments.page = this.paging.page;
        }

        if (this.sorting.hasSorting) {
            connectionQueryArguments.sortBy = this.sorting.field;
            connectionQueryArguments.sortDirection = (this.sorting.direction === SortDirection.descending) ? 'desc' : 'asc';
        }

        if (!ValidateRequestArguments(this.constructor.name, this.requestArguments, args)) {
            connection = new NullObservableQueryConnection(this.defaultValue);
        } else {
            actualRoute = this.routeTemplate(args);
            connection = new ObservableQueryConnection<TDataType>(actualRoute, this._microservice);
        }

        const subscriber = new ObservableQuerySubscription(connection);
        connection.connect(data => {
            const result: any = data;
            try {
                if (this.enumerable) {
                    if (Array.isArray(result.data)) {
                        result.data = JsonSerializer.deserializeArrayFromInstance(this.modelType, result.data);
                    } else {
                        result.data = [];
                    }
                } else {
                    result.data = JsonSerializer.deserializeFromInstance(this.modelType, result.data);
                }
                callback(result);
            } catch (ex) {
                console.log(ex);
            }
        }, connectionQueryArguments);
        return subscriber;
    }
}
