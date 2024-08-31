// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { Constructor } from '@cratis/fundamentals';
import { IDialogMediatorHandler } from './IDialogMediatorHandler';
import { DialogRegistration, DialogRequest, DialogResolver } from './DialogRegistration';

/**
 * Represents an implementation of {@link IDialogMediatorHandler}
 */
export class DialogMediatorHandler extends IDialogMediatorHandler {
    private _registrations: WeakMap<Constructor, DialogRegistration<any, any>> = new WeakMap();

    /** @inheritdoc */
    subscribe<TRequest extends {}, TResponse>(requestType: Constructor<TRequest>, requester: DialogRequest<TRequest, TResponse>, responder: DialogResolver<TResponse>): void {
        this._registrations.set(
            requestType,
            new DialogRegistration<TRequest, TResponse>(requester, responder));
    }

    /** @inheritdoc */
    show<TRequest extends {}, TResponse>(request: TRequest): Promise<TResponse> {
        if (!this._registrations.has(request.constructor as Constructor)) {
            return Promise.reject('No registration found for request');
        }

        const promise = new Promise<TResponse>((resolve, reject) => {
            const registration = this._registrations.get(request.constructor as Constructor)!;
            registration.requester(request, resolve);
        });

        return promise;
    }
}
