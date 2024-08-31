// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { Constructor } from '@cratis/fundamentals';
import { IMessenger } from './IMessenger';
import { filter, Subject, Subscription } from 'rxjs';
import { Message } from './Message';

/**
 * Represents an implementation of {@link IMessenger}.
 */
export class Messenger extends IMessenger {
    private _messages: Subject<Message> = new Subject<Message>();

    /** @inheritdoc */
    publish<TMessage extends {}>(message: TMessage): void {
        this._messages.next(new Message(message.constructor as Constructor, message));
    }

    /** @inheritdoc */
    subscribe<TMessage extends {}>(type: Constructor<TMessage>, callback: (message: TMessage) => void): Subscription {
        const observable = this._messages.pipe(filter(m => m.type === callback.constructor))
        const subscription = observable.subscribe(m => callback(m.content));
        return subscription;
    }
}
