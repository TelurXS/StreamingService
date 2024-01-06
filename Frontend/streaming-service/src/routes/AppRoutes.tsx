import { RequireAuth } from "react-auth-kit";
import { Routes, Route } from "react-router-dom";
import ExampleApiAuth from "../pages/examples/ExampleApiAuth";
import ExampleLoginPage from "../pages/examples/ExampleLoginPage";
import ExamplePrivateRoute from "../pages/examples/ExamplePrivateRoute";
import ExampleRegisterPage from "../pages/examples/ExampleRegisterPage";
import HomePage from "../pages/HomePage";
import NotFoundPage from "../pages/NotFoundPage";
import TitlePage from "../pages/titles/TitlePage";
import TitlesPage from "../pages/titles/TitlesPage";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />

      <Route path="/titles">
        <Route index element={<TitlesPage />}/>
        <Route path=":slug" element={<TitlePage />} />
      </Route>

      <Route path="/login" element={<ExampleLoginPage />} />
      <Route path="/register" element={<ExampleRegisterPage />} />

      <Route path="/api-auth" element={<ExampleApiAuth />} />
      <Route path="/private" element={<RequireAuth loginPath={'/login'}>
          <div>
            Secure
          </div>
        </RequireAuth>} />

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}

export default AppRoutes;
