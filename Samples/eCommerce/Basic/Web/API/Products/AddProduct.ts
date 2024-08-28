/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

/* eslint-disable sort-imports */
/* eslint-disable @typescript-eslint/no-empty-interface */
// eslint-disable-next-line header/header
import { Command, CommandPropertyValidators, CommandValidator } from '@cratis/applications/commands';
import { useCommand, SetCommandValues, ClearCommandValues } from '@cratis/applications.react/commands';
import { Validator } from '@cratis/applications/validation';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/products/catalog/add-product');

export interface IAddProduct {
    SKU?: string;
    name?: string;
}

export class AddProductValidator extends CommandValidator {
    readonly properties: CommandPropertyValidators = {
        SKU: new Validator(),
        name: new Validator(),
    };
}

export class AddProduct extends Command<IAddProduct> implements IAddProduct {
    readonly route: string = '/api/products/catalog/add-product';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly validation: CommandValidator = new AddProductValidator();

    private _SKU!: string;
    private _name!: string;

    constructor() {
        super(Object, false);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    get properties(): string[] {
        return [
            'SKU',
            'name',
        ];
    }

    get SKU(): string {
        return this._SKU;
    }

    set SKU(value: string) {
        this._SKU = value;
        this.propertyChanged('SKU');
    }
    get name(): string {
        return this._name;
    }

    set name(value: string) {
        this._name = value;
        this.propertyChanged('name');
    }

    static use(initialValues?: IAddProduct): [AddProduct, SetCommandValues<IAddProduct>, ClearCommandValues] {
        return useCommand<AddProduct, IAddProduct>(AddProduct, initialValues);
    }
}
