// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React, { useMemo, useRef } from 'react';
import { StandardDialogRequest } from './StandardDialogRequest';
import { DialogResult } from '@cratis/applications.react/dialogs';
import { useDialogRequest } from './useDialogRequest';
import { DialogMediator } from './DialogMediator';
import { DialogMediatorHandler } from './DialogMediatorHandler';
import { IDialogMediatorHandler } from './IDialogMediatorHandler';

export interface IStandardDialogContext {
}

export const StandardDialogContext = React.createContext<IStandardDialogContext>({});

export interface StandardDialogsProps {
    children?: JSX.Element | JSX.Element[];
    component: React.FC | React.FC<any>;
}

const StandardDialogWrapper = (props: StandardDialogsProps) => {
    const [StandardDialog] = useDialogRequest<StandardDialogRequest, DialogResult>(StandardDialogRequest);

    return (
        <StandardDialogContext.Provider value={{}}>
            <>
                {props.children}
                <StandardDialog>
                    <props.component />
                </StandardDialog>
            </>
        </StandardDialogContext.Provider>
    );
};

export const StandardDialogs = (props: StandardDialogsProps) => {

    const mediatorHandler = useRef<IDialogMediatorHandler | null>(null);
    mediatorHandler.current = useMemo(() => {
        return new DialogMediatorHandler();
    }, []);

    return (
        <DialogMediator handler={mediatorHandler.current!}>
            <StandardDialogWrapper component={props.component}>
                {props.children}
            </StandardDialogWrapper>
        </DialogMediator>
    );
};
