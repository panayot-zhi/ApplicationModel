/*---------------------------------------------------------------------------------------------
 *  **DO NOT EDIT** - This file is an automatically generated file.
 *--------------------------------------------------------------------------------------------*/

// eslint-disable-next-line header/header
import { QueryFor, QueryResultWithState } from '@cratis/applications/queries';
import { useQuery, PerformQuery } from '@cratis/applications.react/queries';
import { TopLevelType } from './TopLevelType';
import Handlebars from 'handlebars';

const routeTemplate = Handlebars.compile('/api/my/do-stuff');

export class GetStuff extends QueryFor<TopLevelType[]> {
    readonly route: string = '/api/my/do-stuff';
    readonly routeTemplate: Handlebars.TemplateDelegate = routeTemplate;
    readonly defaultValue: TopLevelType[] = [];

    constructor() {
        super(TopLevelType, true);
    }

    get requestArguments(): string[] {
        return [
        ];
    }

    static use(): [QueryResultWithState<TopLevelType[]>, PerformQuery] {
        return useQuery<TopLevelType[], GetStuff>(GetStuff);
    }
}
