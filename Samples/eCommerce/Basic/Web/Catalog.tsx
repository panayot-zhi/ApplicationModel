// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { withViewModel } from '@cratis/applications.react.mvvm';
import { CatalogViewModel } from './CatalogViewModel';
import { AllProducts, ObserveAllProducts } from './API/Products';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { useEffect, useState } from 'react';
import { SortDirection, Sorting } from '@cratis/applications/queries';

export const Catalog = withViewModel(CatalogViewModel, ({ viewModel }) => {
    // // const [products, perform, setSorting, setPage] = AllProducts.useWithPaging(pageSize);
    const [observableProducts, setSorting, setPage, setPageSize] = ObserveAllProducts.useWithPaging(10);
    const [descending, setDescending] = useState(false);
    const [currentPage, setCurrentPage] = useState(0);

    return (
        <div>
            <div>Page {currentPage + 1} of {observableProducts.paging.totalPages}</div>
            <DataTable value={observableProducts.data}>
                <Column field="id" header="SKU" />
                <Column field="name" header="Name" />
            </DataTable>
            Total items: {observableProducts.paging.totalItems}
            <br />

            <button onClick={() => {
                setCurrentPage(currentPage - 1);
                setPage(currentPage - 1);
            }}>Previous page</button>

            &nbsp;
            &nbsp;

            <button onClick={() => {
                setCurrentPage(currentPage + 1);
                setPage(currentPage + 1);
            }}>Next page</button>
            <br />

            <button onClick={() => {
                setPageSize(20);
            }}>More stuff</button>

            &nbsp;

            <button onClick={() => {
                if (descending) {
                    setSorting(AllProducts.sortBy.id.ascending);
                    setPage(0);
                    setDescending(false);
                } else {
                    setSorting(AllProducts.sortBy.id.descending);
                    setPage(0);
                    setDescending(true);
                }
            }}>Change sorting</button>
        </div>
    );
});
