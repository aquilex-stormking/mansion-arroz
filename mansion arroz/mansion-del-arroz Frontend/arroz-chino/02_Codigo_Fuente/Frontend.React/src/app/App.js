import React, { useState } from "react";
import AuthService from "./services/Auth.Service";
import CssBaseline from "@mui/material/CssBaseline";

// ROUTERS
import PublicRoutes from "./router/Public.Router";
import PrivateRoutes from "./router/Private.Router";

// COMPONENTS
import Box from "@mui/material/Box";
import Header from "./layouts/header/Header";
import MenuDrawer from "./layouts/menu-drawer/MenuDrawer";
import ContentPage from "./layouts/content-page/ContentPage";

export default function App() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(true);
  const isAuth = AuthService.isData;

  return isAuth ? (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header setIsDrawerOpen={setIsDrawerOpen} isDrawerOpen={isDrawerOpen} />
      <MenuDrawer setIsDrawerOpen={setIsDrawerOpen} isDrawerOpen={isDrawerOpen} />

      <ContentPage isDrawerOpen={isDrawerOpen}>
        <div className="card mb-5 app-card-height p-4">
          <PrivateRoutes />
        </div>
      </ContentPage>
    </Box>
  ) : (
    // <></>
    <PublicRoutes />
  );
}
