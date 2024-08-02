/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { Command, CommandValidator, CommandPropertyValidators } from '@cratis/applications/commands';
import { useCommand, SetCommandValues, ClearCommandValues } from '@cratis/applications.react/commands';
import { Validator } from '@cratis/applications/validation';
import { Guid } from '@cratis/fundamentals';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/users/{id}/change-profile-picture');

export interface IChangeProfilePicture {
    id?: Guid;
    picture?: string;
}

export class ChangeProfilePictureValidator extends CommandValidator {
    readonly properties: CommandPropertyValidators = {
        id: new Validator(),
        picture: new Validator(),
    };
}

export class ChangeProfilePicture extends Command<IChangeProfilePicture> implements IChangeProfilePicture {
    readonly route: string = '/api/users/{id}/change-profile-picture';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly validation: CommandValidator = new ChangeProfilePictureValidator();

    private _id!: Guid;
    private _picture!: string;

    constructor() {
        super(Object, false);
    }

    get requestArguments(): string[] {
        return [
            'id',
        ];
    }

    get properties(): string[] {
        return [
            'id',
            'picture',
        ];
    }

    get id(): Guid {
        return this._id;
    }

    set id(value: Guid) {
        this._id = value;
        this.propertyChanged('id');
    }
    get picture(): string {
        return this._picture;
    }

    set picture(value: string) {
        this._picture = value;
        this.propertyChanged('picture');
    }

    static use(initialValues?: IChangeProfilePicture): [ChangeProfilePicture, SetCommandValues<IChangeProfilePicture>, ClearCommandValues] {
        return useCommand<ChangeProfilePicture, IChangeProfilePicture>(ChangeProfilePicture, initialValues);
    }
}
