import React from "react";
import "./MenuDrawer.css";
import { useLocation, NavLink } from "react-router-dom";

// COMPONENTS
import { Nav } from "react-bootstrap";
import { Drawer } from "@mui/material";

// ROUTES
import privateRoutes from "../../router/routes/Private.Routes";

export default function MenuDrawer(props) {
  const location = useLocation();

  const activeRoute = (routePath) => {
    return location.pathname.indexOf(routePath) > -1 ? "link-active" : "";
  };

  return (
    <Drawer
      anchor="left"
      variant="persistent"
      open={props.isDrawerOpen}
      className="width-drawer scroll-drawer"
      classes={{ paper: "width-drawer scroll-drawer bg-drawer" }}
    >
      <div className="app-drawer">
        <Nav className="d-block">
          {privateRoutes.map((route, key) => {
            if (route.path !== "*")
              return (
                <li key={key}>
                  <NavLink to={route.path} className="nav-link" activeClassName={activeRoute(route.path)}>
                    <i className={"size-icon pr-2 text-white " + route.icon} />
                    <p>{route.name}</p>
                  </NavLink>
                </li>
              );
            return null;
          })}
        </Nav>
      </div>
    </Drawer>
  );
}
