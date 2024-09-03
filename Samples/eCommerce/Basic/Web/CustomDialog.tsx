// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { useDialogContext } from '@cratis/applications.react.mvvm/dialogs';

export class CustomDialogRequest { 
    constructor(readonly content: string) {
    }
}

export const CustomDialog = () => {
    const { request, resolver } = useDialogContext<CustomDialogRequest, string>();

    return (
        <Dialog header="My custom dialog" visible={true} onHide={() => resolver('Did not do it..')}>
            <h2>Dialog</h2>
            {request.content}
            <br />
            <Button onClick={() => resolver('Done done done...')}>We're done</Button>
        </Dialog>
    );
};