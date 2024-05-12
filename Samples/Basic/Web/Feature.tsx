// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { withViewModel } from '@cratis/applications.react.mvvm';
import { FeatureViewModel } from './FeatureViewModel';

export const Feature = withViewModel(FeatureViewModel, ({viewModel}) => {
    return (
        <div>
            <h2>Hello, world : {viewModel.count} </h2>
        </div>
    );
});