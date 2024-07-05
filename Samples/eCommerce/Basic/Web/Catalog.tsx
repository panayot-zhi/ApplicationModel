// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { withViewModel } from '@cratis/applications.react.mvvm';
import { CatalogViewModel } from './CatalogViewModel';
import { AllProducts } from './API/Products';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useState } from 'react';

export const Catalog = withViewModel(CatalogViewModel, ({ viewModel }) => {
    const [products, currentPage, perform, setSorting, setPage] = AllProducts.useWithPaging(10);

    return (
        <div>
            <div>Page {currentPage + 1}</div>
            <DataTable value={products.data}>
                <Column field="id" header="SKU" />
                <Column field="name" header="Name" />
            </DataTable>

            <button onClick={() => {
                const page = currentPage + 1;
                setPage(page);
            }}>Next page</button>
        </div>
    );
});
