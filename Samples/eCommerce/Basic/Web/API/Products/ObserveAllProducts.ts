/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { ObservableQueryFor, QueryResultWithState, Sorting, SortingActions, SortingActionsForObservableQuery, Paging } from '@cratis/applications/queries';
import { useObservableQuery, useObservableQueryWithPaging, SetSorting, SetPage, SetPageSize } from '@cratis/applications.react/queries';
import { Product } from './Product';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/products/catalog/observe');

class ObserveAllProductsSortBy {
    private _id: SortingActionsForObservableQuery<Product[]>;
    private _name: SortingActionsForObservableQuery<Product[]>;
    private _isRegistered: SortingActionsForObservableQuery<Product[]>;

    constructor(readonly query: ObserveAllProducts) {
        this._id = new SortingActionsForObservableQuery<Product[]>('id', query);
        this._name = new SortingActionsForObservableQuery<Product[]>('name', query);
        this._isRegistered = new SortingActionsForObservableQuery<Product[]>('isRegistered', query);
    }

    get id(): SortingActionsForObservableQuery<Product[]> {
        return this._id;
    }
    get name(): SortingActionsForObservableQuery<Product[]> {
        return this._name;
    }
    get isRegistered(): SortingActionsForObservableQuery<Product[]> {
        return this._isRegistered;
    }
}

class ObserveAllProductsSortByWithoutQuery {
    private _id: SortingActions  = new SortingActions('id');
    private _name: SortingActions  = new SortingActions('name');
    private _isRegistered: SortingActions  = new SortingActions('isRegistered');

    get id(): SortingActions {
        return this._id;
    }
    get name(): SortingActions {
        return this._name;
    }
    get isRegistered(): SortingActions {
        return this._isRegistered;
    }
}

export class ObserveAllProducts extends ObservableQueryFor<Product[]> {
    readonly route: string = '/api/products/catalog/observe';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly defaultValue: Product[] = [];
    private readonly _sortBy: ObserveAllProductsSortBy;
    private static readonly _sortBy: ObserveAllProductsSortByWithoutQuery = new ObserveAllProductsSortByWithoutQuery();

    constructor() {
        super(Product, true);
        this._sortBy = new ObserveAllProductsSortBy(this);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    get sortBy(): ObserveAllProductsSortBy {
        return this._sortBy;
    }

    static get sortBy(): ObserveAllProductsSortByWithoutQuery {
        return this._sortBy;
    }

    static use(sorting?: Sorting): [QueryResultWithState<Product[]>, SetSorting] {
        return useObservableQuery<Product[], ObserveAllProducts>(ObserveAllProducts, undefined, sorting);
    }

    static useWithPaging(pageSize: number, sorting?: Sorting): [QueryResultWithState<Product[]>, SetSorting, SetPage, SetPageSize] {
        return useObservableQueryWithPaging<Product[], ObserveAllProducts>(ObserveAllProducts, new Paging(0, pageSize), undefined, sorting);
    }
}
