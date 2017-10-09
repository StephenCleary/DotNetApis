import * as React from "react";
import { RouteComponentProps } from "react-router";
import { Route } from "react-router-dom";

import { Home } from "./Home";
import { Package } from "./Package";
import { State } from "../reducers";
import { Actions } from "../actions";

export function Main(props: State & Actions & RouteComponentProps<any>)
{
    return (
    <div>
        <Route exact path="/" render={() => <Home {...props} />} />
        <Route path="/pkg/:packageId/:packageVersion?/:targetFramework?" render={(routeProps) => <Package {...props} {...routeProps}/>}/>
    </div>);
}