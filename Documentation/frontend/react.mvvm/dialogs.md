# Dialogs

## Confirmation Dialogs

The most common use of modal dialogs are the standard confirmation dialogs. These are dialogs where you ask the user to confirm
a specific action. The Application Model supports these out of the box and you have options for what type of confirmation you're
looking for in the form of passing it which buttons to show.

There is an enum called `DialogButtons` that has the following options:

| Value | Description |
| ----- | ----------- |
| Ok    | Only show a single Ok button, typically used to inform the user and the user to acknowledge |
| OkCancel | Show both an Ok and a Cancel button |
| YesNo | Show a Yes and No button |
| YesNoCancel | Show Yes, No and a Cancel button |

For standard confirmation dialogs, there is a specific expected result called `DialogResult` that the dialog needs to communicate back.
The values are:

* Yes
* No
* Ok
* Cancel

To use a confirmation dialog from a ViewModel, you need to take a dependency to the `IDialogs`, assuming you have hooked up [TSyringe and bindings](./tsyringe.md).
Then in a method, you can call the `showConfirmation()` on the `IDialogs` to show the confirmation. 

Below is a full sample of how this works.

```ts
import { injectable } from 'tsyringe';
import { DialogResult } from '@cratis/applications.react/dialogs';
import { DialogButtons, IDialogs } from '@cratis/applications.react.mvvm/dialogs';

@injectable()
export class YourViewModel {
    constructor(
        private readonly _dialogs: IDialogs) {
    }

    // Method called from typically your view
    async deleteTheThing() {
        const result = await this._dialogs.showConfirmation('Delete?', 'Are you sure you want to delete?', DialogButtons.YesNo);
        if( result == DialogResult.Yes ) {
            // Do something - typically call your server
        }
    }
}
```

Since you haven't defined how a confirmation dialog looks like, you will not see anything, nor will the `showConfirmation()` ever return.

### Defining the Confirmation Dialog

You define a confirmation dialog on the application level. It is then the dialog that will be used across your entire application.

The anatomy of any dialog is that it uses the `useDialogContext()` in the dialog itself to get what context it is in.
With the context you get access to the actual request payload and the method to call when the dialog should close, or the dialog resolver
as it is called.

Below is an example using [Prime React](http://primereact.org) to create a confirmation dialog supporting the different button types.

```tsx
import { Dialog } from 'primereact/dialog';
import { DialogButtons, ConfirmationDialogRequest, useDialogContext } from '@cratis/applications.react.mvvm/dialogs';
import { DialogResult } from '@cratis/applications.react/dialogs';
import { Button } from 'primereact/button';

export const ConfirmationDialog = () => {
    const { request, resolver } = useDialogContext<ConfirmationDialogRequest, DialogResult>();

    const headerElement = (
        <div className="inline-flex align-items-center justify-content-center gap-2">
            <span className="font-bold white-space-nowrap">{request.title}</span>
        </div>
    );

    const okFooter = (
        <>
            {/* Hook up buttons with resolvers resolving to expected DialogResult */}
            <Button label="Ok" icon="pi pi-check" onClick={() => resolver(DialogResult.Ok)} autoFocus />
        </>
    );

    const okCancelFooter = (
        <>
            {/* Hook up buttons with resolvers resolving to expected DialogResult */}
            <Button label="Ok" icon="pi pi-check" onClick={() => resolver(DialogResult.Ok)} autoFocus />
            <Button label="Cancel" icon="pi pi-times" severity='secondary' onClick={() => resolver(DialogResult.Cancelled)} />
        </>
    );

    const yesNoFooter = (
        <>
            {/* Hook up buttons with resolvers resolving to expected DialogResult */}
            <Button label="Yes" icon="pi pi-check" onClick={() => resolver(DialogResult.Yes)} autoFocus />
            <Button label="No" icon="pi pi-times" severity='secondary' onClick={() => resolver(DialogResult.No)} />
        </>
    );

    const yesNoCancelFooter = (
        <>
            {/* Hook up buttons with resolvers resolving to expected DialogResult */}
            <Button label="Yes" icon="pi pi-check" onClick={() => resolver(DialogResult.Yes)} autoFocus />
            <Button label="No" icon="pi pi-times" severity='secondary' onClick={() => resolver(DialogResult.No)} />
        </>
    );

    const getFooterInterior = () => {
        switch (request.buttons) {
            case DialogButtons.Ok:
                return okFooter;
            case DialogButtons.OkCancel:
                return okCancelFooter;
            case DialogButtons.YesNo:
                return yesNoFooter;
            case DialogButtons.YesNoCancel:
                return yesNoCancelFooter;
        }

        return (<></>)
    }

    const footer = (
        <div className="card flex flex-wrap justify-content-center gap-3">
            {getFooterInterior()}
        </div>
    );

    return (
        <>
            {/* On hide we call the resolver with cancelled */}
            <Dialog header={headerElement} modal footer={footer} onHide={() => resolver(DialogResult.Cancelled)} visible={true}>
                <p className="m-0">
                    {request.message}
                </p>
            </Dialog>
        </>
    );
};
```

The code above uses the `useDialogContext()` with the `ConfirmationDialogRequest` and `DialogResult` as the types expected from
the request and resolver type. Within the rendering of the component you'll notice that buttons are hooked up to resolve the
dialog with the expected `DialogResult`. Once a button is called, it resolves the request and the information will be passed
onto the `Promise` created within the `IDialogs` service.

To enable the new `ConfirmationDialog` all you need to do is hook it up in your application like below.

```tsx
export const App = () => {
    return (
        <ConfirmationDialogs component={ConfirmationDialog}>
            {/* Your application */}
        </ConfirmationDialogs>
    );
};
```

## Custom dialogs

```ts
import { injectable } from 'tsyringe';
import { DialogButtons, IDialogs } from '@cratis/applications.react.mvvm/dialogs';

export class CustomDialogRequest {}

export class CustomDialogResponse {}

@injectable()
export class FeatureViewModel {
    constructor(
        private readonly _dialogs: IDialogs) {
    }

    async doThings() {
        const result = await this._dialogs.show(new CustomDialogRequest());
        if( result == )

    }
}
```
