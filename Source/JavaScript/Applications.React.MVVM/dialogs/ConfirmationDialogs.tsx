// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React, { useMemo, useRef } from 'react';
import { ConfirmationDialogRequest } from './ConfirmationDialogRequest';
import { DialogResult } from '@cratis/applications.react/dialogs';
import { useDialogRequest } from './useDialogRequest';
import { DialogMediator } from './DialogMediator';
import { DialogMediatorHandler } from './DialogMediatorHandler';
import { IDialogMediatorHandler } from './IDialogMediatorHandler';

export interface IConfirmationDialogContext {
}

export const ConfirmationDialogContext = React.createContext<IConfirmationDialogContext>({});

export interface ConfirmationDialogsProps {
    children?: JSX.Element | JSX.Element[];
    component: React.FC | React.FC<any>;
}

const ConfirmationDialogWrapper = (props: ConfirmationDialogsProps) => {
    const [StandardDialog] = useDialogRequest<ConfirmationDialogRequest, DialogResult>(ConfirmationDialogRequest);

    return (
        <ConfirmationDialogContext.Provider value={{}}>
            <>
                {props.children}
                <StandardDialog>
                    <props.component />
                </StandardDialog>
            </>
        </ConfirmationDialogContext.Provider>
    );
};

export const ConfirmationDialogs = (props: ConfirmationDialogsProps) => {

    const mediatorHandler = useRef<IDialogMediatorHandler | null>(null);
    mediatorHandler.current = useMemo(() => {
        return new DialogMediatorHandler();
    }, []);

    return (
        <DialogMediator handler={mediatorHandler.current!}>
            <ConfirmationDialogWrapper component={props.component}>
                {props.children}
            </ConfirmationDialogWrapper>
        </DialogMediator>
    );
};
