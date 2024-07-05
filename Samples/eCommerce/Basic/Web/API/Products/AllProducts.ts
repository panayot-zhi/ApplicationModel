/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { QueryFor, QueryResultWithState, SortingForQuery, Paging } from '@cratis/applications/queries';
import { useQuery, useQueryWithPaging, PerformQuery, SetSorting, SetPage } from '@cratis/applications.react/queries';
import { Product } from './Product';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/products/catalog');


export class AllProducts extends QueryFor<Product[]> {
    readonly route: string = '/api/products/catalog';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly defaultValue: Product[] = [];

    constructor() {
        super(Product, true);
    }

    get requestArguments(): string[] {
        return [
        ];
    }


    static use(): [QueryResultWithState<Product[]>, PerformQuery, SetSorting] {
        return useQuery<Product[], AllProducts>(AllProducts);
    }

    static useWithPaging(page:number, pageSize: number): [QueryResultWithState<Product[]>, number, PerformQuery, SetSorting, SetPage] {
        return useQueryWithPaging<Product[], AllProducts>(AllProducts, new Paging(page, pageSize));
    }
}
