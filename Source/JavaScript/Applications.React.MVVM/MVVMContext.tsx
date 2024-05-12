// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React from 'react';
import { Bindings } from './Bindings';
import { configure as configureMobx } from 'mobx';

export interface MVVMProps {
    children?: JSX.Element | JSX.Element[];
}

export interface MVVMContextDefinition {   
}

export const MVVMContext = React.createContext({} as MVVMContextDefinition);

export const MVVM = (props: MVVMProps) => {
    configureMobx({
        enforceActions: 'never'
    });
    
    Bindings.initialize();
 
    return (
        <MVVMContext.Provider value={{}}>
            {props.children}
        </MVVMContext.Provider>
    );
}