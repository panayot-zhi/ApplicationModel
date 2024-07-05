// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React from 'react';
import { IdentityProvider } from '@cratis/applications.react/identity';
import { MVVM } from '@cratis/applications.react.mvvm';
import { BrowserRouter } from "react-router-dom";
import { Feature } from './Feature';
import { Catalog } from './Catalog';

export const App = () => {
    return (
        <IdentityProvider>
            <MVVM>
                <BrowserRouter>
                    {/* <Feature /> */}
                    <Catalog />
                </BrowserRouter>
            </MVVM>
        </IdentityProvider>
    );
}
