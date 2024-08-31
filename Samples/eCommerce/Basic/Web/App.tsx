// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React, { useEffect } from 'react';
import { ApplicationModel } from '@cratis/applications.react';
import { IdentityProvider, useIdentity } from '@cratis/applications.react/identity';
import { MVVM } from '@cratis/applications.react.mvvm';
import { BrowserRouter } from "react-router-dom";
import { Feature } from './Feature';
import { Catalog } from './Catalog';
import { ObservingCatalog } from './ObservingCatalog';
import { blah } from './Components/blah';
import { StandardDialogs } from '@cratis/applications.react.mvvm/dialogs';

export const App = () => {
    return (
        <ApplicationModel microservice='e-commerce'>
            <StandardDialogs component={blah}>
                <MVVM>
                    <BrowserRouter>
                        <Feature blah='Horse' />
                        {/* <Catalog /> */}
                        {/* <ObservingCatalog /> */}
                    </BrowserRouter>
                </MVVM>
            </StandardDialogs>
        </ApplicationModel>
    );
}
