// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { container } from "tsyringe";
import { Constructor } from '@cratis/fundamentals';
import { FunctionComponent, ReactElement, useMemo, useRef, useState } from 'react';
import { Observer } from 'mobx-react';
import { makeAutoObservable } from 'mobx';
import { useParams } from 'react-router-dom';
import { DialogMediator, DialogMediatorContextImplementation, IDialogMediatorContext } from './dialogs/useDialogRequest';
import { IDialogs } from './dialogs';
import { Dialogs } from './dialogs';

export interface IViewContext<T, TProps = any> {
    viewModel: T,
    props: TProps,
}

export function withViewModel<TViewModel extends {}, TProps extends {} = {}>(viewModelType: Constructor<TViewModel>, targetComponent: FunctionComponent<IViewContext<TViewModel, TProps>>) {
    const renderComponent = (props: TProps) => {
        const params = useParams();
        const dialogMediatorContext = useRef<IDialogMediatorContext | null>(null);
        const vm = useRef<TViewModel | null>(null);
        
        dialogMediatorContext.current = useMemo(() => {
            return new DialogMediatorContextImplementation();
        }, []);

        vm.current = useMemo(() => {
            const child = container.createChildContainer();
            
            child.registerInstance('props', props);
            child.registerInstance('params', params);

            const dialogService = new Dialogs(dialogMediatorContext.current!);
            child.registerInstance<IDialogs>(IDialogs as Constructor<IDialogs>, dialogService);
            const vm = child.resolve<TViewModel>(viewModelType) as any;
            makeAutoObservable(vm as any);
            return vm;
        }, []);

        const component = () => targetComponent({ viewModel: vm.current!, props }) as ReactElement<any, string>;
        return (
            <DialogMediator context={dialogMediatorContext.current!}>
                <Observer>
                    {component}
                </Observer>
            </DialogMediator>
        );
    };

    return renderComponent;
}
