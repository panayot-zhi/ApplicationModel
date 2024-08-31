// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React, { useContext, useEffect, useMemo } from 'react';
import { Constructor } from '@cratis/fundamentals';
import { DialogResolver } from './DialogRegistration';
import { DialogMediatorContext } from './DialogMediator';

interface DialogWrapperProps<TRequest extends {}> {
    children?: JSX.Element | JSX.Element[];
    isVisible: Boolean;
}

const DialogWrapper = <TRequest extends {}, TResponse>(props: DialogWrapperProps<TRequest>) => {
    return (
        <>
            <div>
                {props.isVisible && props.children}
            </div>
        </>
    )
}

interface IDialogRequestProps {
    children?: JSX.Element | JSX.Element[];
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

/**
 * Use a dialog request for showing a dialog.
 * @param request Type of request to use that represents a request that will be made by your view model.
 * @returns A tuple with a component to use for wrapping your dialog and a delegate used when the dialog is resolved with the result expected.
 */
export const useDialogRequest = <TRequest extends {}, TResponse>(request: Constructor<TRequest>): [React.FC<IDialogRequestProps>, DialogResolver<TResponse>] => {
    const [DialogWrapper, responder] = useConfiguredWrapper<TRequest, TResponse>(request);
    return [DialogWrapper, responder];
};
