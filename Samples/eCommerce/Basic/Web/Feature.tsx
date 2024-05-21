// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { withViewModel } from '@cratis/applications.react.mvvm';
import { FeatureViewModel } from './FeatureViewModel';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

export const Feature = withViewModel(FeatureViewModel, ({viewModel}) => {

    return (
        <div>
            <h2>Hello, world : {`${viewModel.cart.id}`} </h2>

            <DataTable value={viewModel.cart.items}>
                <Column field="SKU" header="SKU"/>
                <Column field="price" header="Price"/>
                <Column field="quantity" header="Quantity"/>
            </DataTable>
        </div>
    );
});