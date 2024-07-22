// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { injectable } from 'tsyringe';
import { Cart, CartForCurrentUser, ObserveCartForCurrentUser } from './API/Carts';
import { FeatureProps } from './Feature';

@injectable()
export class FeatureViewModel {
    constructor(readonly query: ObserveCartForCurrentUser, readonly props: FeatureProps) {
        query.subscribe(result => {
            this.cart = result.data;
        });
    }

    cart: Cart = new Cart();
}