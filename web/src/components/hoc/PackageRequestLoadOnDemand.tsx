import * as React from 'react';

import { PackageLoading } from '../PackageLoading';
import { PackageRequestError } from '../PackageRequestError';

import { State } from '../../reducers';
import { Actions } from '../../actions';
import { ReactComponent } from './util';
import { PackageRequestInjectedProps, withPackageRequest } from './PackageRequest';
import { withLoadOnDemand } from './LoadOnDemand';
import { withEither } from './Either';

const loadOnDemand = withLoadOnDemand<State & Actions & PackageRequestInjectedProps>({
    hasStarted: props => !!props.pkgRequestStatus,
    isLoaded: props => props.pkgRequestStatus && props.pkgRequestStatus.status !== 'STARTED',
    load: props => props.DocActions.getDoc(props.pkgRequestKey),
    LoadingComponent: PackageLoading
});

const eitherError = withEither<State & Actions & PackageRequestInjectedProps>(props => props.pkgRequestStatus.status !== 'ERROR', PackageRequestError);

/** Loads a package on demand, and handles displaying package loading and package request error components */
export const withPackageRequestLoadOnDemand =
    <TComponentProps extends {}>(Component: ReactComponent<TComponentProps & State & Actions & PackageRequestInjectedProps>) =>
    withPackageRequest(loadOnDemand(eitherError(Component)));