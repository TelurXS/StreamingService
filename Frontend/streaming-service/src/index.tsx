import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { BrowserRouter as Router } from "react-router-dom";
import { initLocalization } from "./translation/Translation";
import { AuthProvider } from 'react-auth-kit'

initLocalization();

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <AuthProvider authType="cookie" authName="_auth" cookieDomain={window.location.hostname} cookieSecure={true}>
      <Router>
        <App />
      </Router>
    </AuthProvider>
  </React.StrictMode>
);
