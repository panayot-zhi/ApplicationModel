// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { injectable } from 'tsyringe';
import { Cart, CartForCurrentUser, ObserveCartForCurrentUser } from './API/Carts';
import { DialogButtons, IDialogs } from '@cratis/applications.react.mvvm/dialogs';
import { CustomDialogRequest } from './Feature';


@injectable()
export class FeatureViewModel {
    constructor(
        readonly query: ObserveCartForCurrentUser, 
        private readonly _dialogs: IDialogs) {
        // query.subscribe(async result => {
        //     this.cart = result.data;

        //     await dialogs.showStandard('Hello', 'This is a message', DialogButtons.Ok);
        // });
    }

    cart: Cart = new Cart();

    async doStuff() {
        const result = await this._dialogs.show(new CustomDialogRequest());
        console.log(`Result: ${result}`);
    }

    async doOtherStuff() {
        const result = await this._dialogs.showStandard('Delete?', 'Are you sure you want to delete?', DialogButtons.YesNo);
        console.log(`Result: ${result}`);
    }
}