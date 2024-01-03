import { Routes, Route } from "react-router-dom";
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

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}

export default AppRoutes;
