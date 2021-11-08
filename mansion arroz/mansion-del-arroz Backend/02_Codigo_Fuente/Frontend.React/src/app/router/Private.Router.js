import React from "react";
import privateRoutes from "./routes/Private.Routes";
import { Redirect, Route, Switch } from "react-router-dom";

export default function PrivateRoutes() {
  return (
    <Switch>
      {privateRoutes.map((route, index) => {
        return route.path !== "*" ? (
          <Route key={index} exact path={route.path} component={route.component} />
        ) : (
          <Route
            key={index}
            path={route.path}
            render={() => {
              return <Redirect to={route.rediret} />;
            }}
          />
        );
      })}
    </Switch>
  );
}
