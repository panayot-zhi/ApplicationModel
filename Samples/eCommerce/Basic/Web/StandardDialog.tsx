// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { Dialog } from 'primereact/dialog'
import { StandardDialogRequest, useDialogContext } from '@cratis/applications.react.mvvm/dialogs'
import { DialogResult } from '@cratis/applications.react/dialogs'

export const StandardDialog = () => {
    const { request, resolver } = useDialogContext<StandardDialogRequest, DialogResult>();

    return (
        <>
            <Dialog header={request.title} onHide={() => resolver(DialogResult.Cancelled)} visible={true}>
                <h1>{request.message}</h1>
                <button onClick={() => resolver(DialogResult.Success)}>OK</button>
            </Dialog>
        </>
    )
}