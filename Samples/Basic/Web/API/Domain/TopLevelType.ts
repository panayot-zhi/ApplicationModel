/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { field } from '@cratis/fundamentals';

import { ChildType } from './ChildType';

export class TopLevelType {

    @field(String)
    value!: string;

    @field(String)
    id!: string;

    @field(Date)
    occurred!: Date;

    @field(Boolean)
    isSomething!: boolean;

    @field(ChildType)
    child!: ChildType;

    @field(String, true)
    strings!: string[];

    @field(Number, true)
    numbers!: number[];

    @field(ChildType, true)
    children!: ChildType[];
}
