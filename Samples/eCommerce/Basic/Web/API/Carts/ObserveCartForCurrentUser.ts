/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { ObservableQueryFor, QueryResultWithState } from '@cratis/applications/queries';
import { useObservableQuery } from '@cratis/applications.react/queries';
import { Cart } from './Cart';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/carts/observe');


export class ObserveCartForCurrentUser extends ObservableQueryFor<Cart> {
    readonly route: string = '/api/carts/observe';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly defaultValue: Cart = {} as any;

    constructor() {
        super(Cart, false);
    }

    get requestArguments(): string[] {
        return [
        ];
    }


    static use(sorting?: Sorting): [QueryResultWithState<Cart[]>, SetSorting] {
        return useObservableQuery<Cart[], ObserveCartForCurrentUser>(ObserveCartForCurrentUser, undefined, sorting);
    }
}
