/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { Command, CommandValidator, CommandPropertyValidators, useCommand, SetCommandValues, ClearCommandValues } from 'Infrastructure/commands';
import { Validator } from 'Infrastructure/validation';
import { AddItemToCart } from './AddItemToCart';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/carts/add-item');

export interface IAddItem {
    addItemToCart?: AddItemToCart;
}

export class AddItemValidator extends CommandValidator {
    readonly properties: CommandPropertyValidators = {
        addItemToCart: new Validator(),
    };
}

export class AddItem extends Command<IAddItem> implements IAddItem {
    readonly route: string = '/api/carts/add-item';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly validation: CommandValidator = new AddItemValidator();

    private _addItemToCart!: AddItemToCart;

    constructor() {
        super(Object, false);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    get properties(): string[] {
        return [
            'addItemToCart',
        ];
    }

    get addItemToCart(): AddItemToCart {
        return this._addItemToCart;
    }

    set addItemToCart(value: AddItemToCart) {
        this._addItemToCart = value;
        this.propertyChanged('addItemToCart');
    }

    static use(initialValues?: IAddItem): [AddItem, SetCommandValues<IAddItem>, ClearCommandValues] {
        return useCommand<AddItem, IAddItem>(AddItem, initialValues);
    }
}
