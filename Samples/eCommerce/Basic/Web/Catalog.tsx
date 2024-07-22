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
    const [pageSize, setPageSize] = useState(10);
    // // const [products, currentPage, perform, setSorting, setPage] = AllProducts.useWithPaging(pageSize);
    const [observableProducts, setSorting, setPage] = ObserveAllProducts.useWithPaging(10);
    const [descending, setDescending] = useState(false);
    const [currentPage, setCurrentPage] = useState(0);

    return (
        <div>
            <div>Page {currentPage + 1}</div>
            <DataTable value={observableProducts.data}>
                <Column field="id" header="SKU" />
                <Column field="name" header="Name" />
            </DataTable>

            <button onClick={() => {
                setCurrentPage(currentPage - 1);
                setPage(currentPage - 1);
            }}>Previous page</button>

            <button onClick={() => {
                setCurrentPage(currentPage + 1);
                setPage(currentPage + 1);
            }}>Next page</button>
            <br />

            <button onClick={() => {
                setPage(20);
            }}>More stuff</button>

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
