import { useTranslation } from "react-i18next";

function HomePage() {
  const { t } = useTranslation();

  return <h1>{t("welcome_text")}</h1>;
}

export default HomePage;
