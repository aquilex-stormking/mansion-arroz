import React from "react";
import "./Header.css";
import AuthService from "../../services/Auth.Service";
import logoApp from "../../../assets/images/logo1.png";

// COMPONENTS
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import MenuIcon from "@mui/icons-material/Menu";
import IconButton from "@mui/material/IconButton";

export default function Header(props) {
  return (
    <AppBar className="header-app" position="fixed" open={props.isDrawerOpen}>
      <Toolbar>
        <IconButton color="inherit" onClick={() => props.setIsDrawerOpen(!props.isDrawerOpen)} edge="start">
          <MenuIcon />
        </IconButton>
        <div className="w-100 d-flex justify-content-between">
          <div className="pl-2 d-flex align-items-center">
            <img className="img-fluid" width="40" src={logoApp} alt="logo" />
            <h4 className="px-2 mb-0">Arroz Chino</h4>
          </div>
          <div className="d-flex align-items-center">
            <h6 className="pr-2 mb-0">{AuthService.UserName}</h6>
            <button className="btn btn-sm btn-link pt-2" onClick={() => AuthService.logout()}>
              <i className="fas fa-sign-out-alt size-icon text-white"></i>
            </button>
          </div>
        </div>
      </Toolbar>
    </AppBar>
  );
}
