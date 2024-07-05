/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { QueryFor, QueryResultWithState, SortingForQuery, Paging } from '@cratis/applications/queries';
import { useQuery, useQueryWithPaging, PerformQuery, SetSorting, SetPage } from '@cratis/applications.react/queries';
import { Product } from './Product';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/products/catalog');

class AllProductsSortBy {
    private _id: SortingForQuery<Product[]>;
    private _name: SortingForQuery<Product[]>;
    private _isRegistered: SortingForQuery<Product[]>;

    constructor(readonly query: AllProducts) {
        this._id = new SortingForQuery<Product[]>('id', query);
        this._name = new SortingForQuery<Product[]>('name', query);
        this._isRegistered = new SortingForQuery<Product[]>('isRegistered', query);
    }

    id(): SortingForQuery<Product[]> {
        return this._id;
    }
    name(): SortingForQuery<Product[]> {
        return this._name;
    }
    isRegistered(): SortingForQuery<Product[]> {
        return this._isRegistered;
    }
}

export class AllProducts extends QueryFor<Product[]> {
    readonly route: string = '/api/products/catalog';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly defaultValue: Product[] = [];
    readonly _sortBy: AllProductsSortBy;

    constructor() {
        super(Product, true);
        this._sortBy = new AllProductsSortBy(this);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    get sortBy(): AllProductsSortBy {
        return this._sortBy;
    }

    static use(): [QueryResultWithState<Product[]>, PerformQuery, SetSorting] {
        return useQuery<Product[], AllProducts>(AllProducts);
    }

    static useWithPaging(pageSize: number): [QueryResultWithState<Product[]>, number, PerformQuery, SetSorting, SetPage] {
        return useQueryWithPaging<Product[], AllProducts>(AllProducts, new Paging(0, pageSize));
    }
}
