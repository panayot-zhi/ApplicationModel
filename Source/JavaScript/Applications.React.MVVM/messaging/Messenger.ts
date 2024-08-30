// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { IMessenger } from './IMessenger';

/**
 * Represents an implementation of {@link IMessenger}.
 */
export class Messenger extends IMessenger {
    private _subscribers: any[] = [];

    /** @inheritdoc */
    publish<T>(message: T): void {
        this._subscribers.forEach(subscriber => subscriber(message));
    }

    /** @inheritdoc */
    subscribe<T>(callback: (message: T) => void): void {
        this._subscribers.push(callback);
    }

    /** @inheritdoc */
    unsubscribe<T>(callback: (message: T) => void): void {

    }
}
