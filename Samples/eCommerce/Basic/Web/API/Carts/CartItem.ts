/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { field } from '@cratis/fundamentals';
import { Price } from '../Price';

export class CartItem {

    @field(String)
    SKU!: string;

    @field(Price)
    price!: Price;

    @field(Number)
    quantity!: number;
}
