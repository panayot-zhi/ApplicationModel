/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { Command, CommandValidator, CommandPropertyValidators } from '@cratis/applications/commands';
import { useCommand, SetCommandValues, ClearCommandValues } from '@cratis/applications.react/commands';
import { Validator } from '@cratis/applications/validation';
import { Price } from '../Price';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/products/pricing/set-price');

export interface ISetPrice {
    SKU?: string;
    price?: Price;
}

export class SetPriceValidator extends CommandValidator {
    readonly properties: CommandPropertyValidators = {
        SKU: new Validator(),
        price: new Validator(),
    };
}

export class SetPrice extends Command<ISetPrice> implements ISetPrice {
    readonly route: string = '/api/products/pricing/set-price';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly validation: CommandValidator = new SetPriceValidator();

    private _SKU!: string;
    private _price!: Price;

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
            'price',
        ];
    }

    get SKU(): string {
        return this._SKU;
    }

    set SKU(value: string) {
        this._SKU = value;
        this.propertyChanged('SKU');
    }
    get price(): Price {
        return this._price;
    }

    set price(value: Price) {
        this._price = value;
        this.propertyChanged('price');
    }

    static use(initialValues?: ISetPrice): [SetPrice, SetCommandValues<ISetPrice>, ClearCommandValues] {
        return useCommand<SetPrice, ISetPrice>(SetPrice, initialValues);
    }
}
