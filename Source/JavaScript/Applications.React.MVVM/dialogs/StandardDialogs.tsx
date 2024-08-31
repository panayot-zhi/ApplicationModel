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

export const StandardDialogContext = React.createContext<IStandardDialogContext>({
    component: () => <></>
});

export interface StandardDialogsProps {
    children?: JSX.Element | JSX.Element[];
    component: React.FunctionComponent;
}

export const StandardDialogs = (props: StandardDialogsProps) => {
    const [StandardDialog, resolver] = useDialogRequest<StandardDialogRequest, DialogResult>(StandardDialogRequest);

    const mediatorHandler = useRef<IDialogMediatorHandler | null>(null);
    mediatorHandler.current = useMemo(() => {
        return new DialogMediatorHandler();
    }, []);

    return (
        <StandardDialogContext.Provider value={{}}>
            <DialogMediator handler={mediatorHandler.current!}>
                <>
                    {props.children}
                    <StandardDialog>
                        <props.component />
                    </StandardDialog>
                </>
            </DialogMediator>
        </StandardDialogContext.Provider>
    );
};
