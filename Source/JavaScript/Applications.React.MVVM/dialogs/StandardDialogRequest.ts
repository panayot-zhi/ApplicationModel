// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { DialogButtons } from './DialogButtons';

/**
 * Represents the request for a standard dialog.
 */
export class StandardDialogRequest {

    /**
     * Initializes a new instance of {@link StandardDialogRequest}.
     * @param {String} title The title of the dialog.
     * @param {String} message The message to show in the dialog.
     * @param {DialogButtons} buttons Buttons to use in the dialog.
     */
    constructor(
        readonly title: string,
        readonly message: string,
        readonly buttons: DialogButtons) {
    }
}
