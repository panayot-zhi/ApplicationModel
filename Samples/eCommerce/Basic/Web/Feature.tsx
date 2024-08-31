// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { withViewModel } from '@cratis/applications.react.mvvm';
import { FeatureViewModel } from './FeatureViewModel';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useIdentity } from '@cratis/applications.react/identity';
import { useDialogRequest, StandardDialogRequest } from '@cratis/applications.react.mvvm/dialogs';
import { Dialog } from 'primereact/dialog';


export interface FeatureProps {
    blah: string;
}


export class MyRequest { }


export const Feature = withViewModel<FeatureViewModel, FeatureProps>(FeatureViewModel, ({ viewModel, props }) => {
    // const [MyDialog, responder] = useDialogRequest<StandardDialogRequest, string>(StandardDialogRequest);
    const identity = useIdentity();
    return (
        <div>
            <h2>Hello {`${identity.name}`} your cart id is {`${viewModel.cart.id}`} </h2>

            <DataTable value={viewModel.cart.items}>
                <Column field="SKU" header="SKU" />
                <Column field="price.net" header="Net Price" />
                <Column field="price.gross" header="Gross Price" />
                <Column field="quantity" header="Quantity" />
            </DataTable>

            <button onClick={() => viewModel.doStuff()}>Open dialog</button>

            {/* <MyDialog>
                <Dialog header="Header" visible={true} onHide={() => responder('blah')}>
                    <h2>Dialog</h2>
                    Hello world
                    <button onClick={() => responder('asaddas')}>We're done</button>
                </Dialog>

            </MyDialog> */}
        </div>
    );
});