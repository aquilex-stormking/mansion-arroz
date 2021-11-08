import React from "react";
import "./ContentPage.css";
import Footer from "../footer/Footer";

export default function ContentPage(props) {
  return (
    <main className={`style-content ${props.isDrawerOpen ? "cambio-content" : "content"}`}>
      <div className="p-4 content-height">{props.children}</div>
      <Footer />
    </main>
  );
}
