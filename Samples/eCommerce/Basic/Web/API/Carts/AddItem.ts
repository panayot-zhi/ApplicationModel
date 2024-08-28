/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

/* eslint-disable sort-imports */
/* eslint-disable @typescript-eslint/no-empty-interface */
// eslint-disable-next-line header/header
import { Command, CommandPropertyValidators, CommandValidator } from '@cratis/applications/commands';
import { useCommand, SetCommandValues, ClearCommandValues } from '@cratis/applications.react/commands';
import { Validator } from '@cratis/applications/validation';
import { Guid } from '@cratis/fundamentals';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/carts/add-item');

export interface IAddItem {
    sku?: string;
    quantity?: number;
}

export class AddItemValidator extends CommandValidator {
    readonly properties: CommandPropertyValidators = {
        sku: new Validator(),
        quantity: new Validator(),
    };
}

export class AddItem extends Command<IAddItem, Guid> implements IAddItem {
    readonly route: string = '/api/carts/add-item';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly validation: CommandValidator = new AddItemValidator();

    private _sku!: string;
    private _quantity!: number;

    constructor() {
        super(Guid, false);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    get properties(): string[] {
        return [
            'sku',
            'quantity',
        ];
    }

    get sku(): string {
        return this._sku;
    }

    set sku(value: string) {
        this._sku = value;
        this.propertyChanged('sku');
    }
    get quantity(): number {
        return this._quantity;
    }

    set quantity(value: number) {
        this._quantity = value;
        this.propertyChanged('quantity');
    }

    static use(initialValues?: IAddItem): [AddItem, SetCommandValues<IAddItem>, ClearCommandValues] {
        return useCommand<AddItem, IAddItem>(AddItem, initialValues);
    }
}
