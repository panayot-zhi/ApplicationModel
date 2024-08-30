// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React, { useContext, useEffect, useMemo } from 'react';
import { Constructor } from '@cratis/fundamentals';


export type DialogResolver<TResponse> = (response: TResponse) => void;
export type DialogRequest<TRequest extends {}, TResponse> = (request: TRequest, resolve: DialogResolver<TResponse>) => void;


export interface DialogRegistration<TRequest extends {}, TResponse> {
    type: Constructor;
    requester: DialogRequest<TRequest, TResponse>;
    responder: DialogResolver<TResponse>;
}

export interface IDialogContext {
}

export const DialogContext = React.createContext<IDialogContext>(undefined as any);


export interface DialogWrapperProps<TRequest extends {}> {
    children?: JSX.Element | JSX.Element[];
    isVisible: Boolean;
}

export const DialogWrapper = <TRequest extends {}, TResponse>(props: DialogWrapperProps<TRequest>) => {
    return (
        <>
            <div>
                {props.isVisible && props.children}
            </div>
        </>
    )
}

export abstract class IDialogMediatorContext {
    abstract subscribe<TRequest extends {}, TResponse>(requestType: Constructor<TRequest>, requester: DialogRequest<TRequest, TResponse>, responder: DialogResolver<TResponse>): void;
    abstract show<TRequest extends {}, TResponse>(request: TRequest): Promise<TResponse>;
}

export class DialogMediatorContextImplementation extends IDialogMediatorContext {
    private _registrations: DialogRegistration<any, any>[] = [];

    /** @inheritdoc */
    subscribe<TRequest extends {}, TResponse>(requestType: Constructor<TRequest>, requester: DialogRequest<TRequest, TResponse>, responder: DialogResolver<TResponse>): void {
        this._registrations.push({
            type: requestType,
            requester,
            responder
        });
    }

    /** @inheritdoc */
    show<TRequest extends {}, TResponse>(request: TRequest): Promise<TResponse> {
        const promise = new Promise<TResponse>((resolve, reject) => {
            const registration = this._registrations.find(_ => _.type === request.constructor);
            if (registration) {
                registration.requester(request, resolve);
            }
        });

        return promise;
    }
}


export const DialogMediatorContext = React.createContext<IDialogMediatorContext>(undefined as any);

export interface DialogMediatorProps {
    children?: JSX.Element | JSX.Element[];
    context: IDialogMediatorContext;
}

interface IDialogRequestProps {
    children?: JSX.Element | JSX.Element[];
}

export const DialogMediator = (props: DialogMediatorProps) => {
    return (
        <DialogMediatorContext.Provider value={props.context}>
            {props.children}
        </DialogMediatorContext.Provider>
    );
}

const useConfiguredWrapper = <TRequest extends {}, TResponse>(type: Constructor<TRequest>): [React.FC<IDialogRequestProps>, DialogResolver<TResponse>] => {
    const mediatorContext = useContext(DialogMediatorContext);
    const [isVisible, setIsVisible] = React.useState(false);

    const requester = (request: TRequest, resolver: DialogResolver<TResponse>) => {
        setIsVisible(true);
    };

    const responder = (response: TResponse) => {
        setIsVisible(false);
    };

    useEffect(() => {
        mediatorContext.subscribe(type, requester, responder);
    }, []);

    const ConfiguredWrapper: React.FC<IDialogRequestProps> = useMemo(() => {
        return ({ children }) => {
            return (
                <DialogWrapper isVisible={isVisible}>
                    {children}
                </DialogWrapper>);
        }
    }, [isVisible]);

    return [ConfiguredWrapper, responder];
};

export const useDialogRequest = <TRequest extends {}, TResponse>(request: Constructor<TRequest>): [React.FC<IDialogRequestProps>, DialogResolver<TResponse>] => {
    const [DialogWrapper, responder] = useConfiguredWrapper<TRequest, TResponse>(request);
    return [DialogWrapper, responder];
};
