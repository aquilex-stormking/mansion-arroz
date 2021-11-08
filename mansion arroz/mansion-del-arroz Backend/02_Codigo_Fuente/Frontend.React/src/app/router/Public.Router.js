import React from "react";
import publicRoutes from "./routes/Public.Routes";
import { Redirect, Route, Switch } from "react-router-dom";

export default function PublicRoutes() {
  return (
    <Switch>
      {publicRoutes.map((route, index) => {
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
