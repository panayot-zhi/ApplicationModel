/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

/* eslint-disable sort-imports */
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

    get requiredRequestArguments(): string[] {
        return [
        ];
    }


    static use(): [QueryResultWithState<Cart>] {
        const [result] = useObservableQuery<Cart, ObserveCartForCurrentUser>(ObserveCartForCurrentUser);
        return [result];
    }
}
