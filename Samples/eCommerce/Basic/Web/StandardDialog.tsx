// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { Dialog } from 'primereact/dialog'
import { DialogButtons, StandardDialogRequest, useDialogContext } from '@cratis/applications.react.mvvm/dialogs'
import { DialogResult } from '@cratis/applications.react/dialogs'
import { Button } from 'primereact/button';
import { s } from 'vite/dist/node/types.d-aGj9QkWt';

export const StandardDialog = () => {
    const { request, resolver } = useDialogContext<StandardDialogRequest, DialogResult>();

    const headerElement = (
        <div className="inline-flex align-items-center justify-content-center gap-2">
            <span className="font-bold white-space-nowrap">{request.title}</span>
        </div>
    );

    const okFooter = (
        <>
            <Button label="Ok" icon="pi pi-check" onClick={() => resolver(DialogResult.Success)} autoFocus />
        </>
    );

    const okCancelFooter = (
        <>
            <Button label="Ok" icon="pi pi-check" onClick={() => resolver(DialogResult.Success)} autoFocus />
            <Button label="Cancel" icon="pi pi-times" onClick={() => resolver(DialogResult.Cancelled)} autoFocus />
        </>
    );

    const yesNoFooter = (
        <>
            <Button label="Yes" icon="pi pi-check" onClick={() => resolver(DialogResult.Success)} autoFocus />
            <Button label="No" icon="pi pi-times" onClick={() => resolver(DialogResult.Cancelled)} autoFocus />
        </>
    );

    const yesNoCancelFooter = (
        <>
            <Button label="Yes" icon="pi pi-check" onClick={() => resolver(DialogResult.Success)} autoFocus />
            <Button label="No" icon="pi pi-times" onClick={() => resolver(DialogResult.Cancelled)} autoFocus />
        </>
    );

    const getFooterInterior = () => {
        switch (request.buttons) {
            case DialogButtons.Ok:
                return okFooter;
            case DialogButtons.OkCancel:
                return okCancelFooter;
            case DialogButtons.YesNo:
                return yesNoFooter;
            case DialogButtons.YesNoCancel:
                return yesNoCancelFooter;
        }

        return (<></>)
    }

    const footer = (
        <div className="card flex flex-wrap justify-content-center gap-3">
            {getFooterInterior()}
        </div>
    );

    return (
        <>
            <Dialog header={headerElement} modal footer={footer} onHide={() => resolver(DialogResult.Cancelled)} visible={true}>
                <p className="m-0">
                    {request.message}
                </p>
            </Dialog>
        </>
    )
}