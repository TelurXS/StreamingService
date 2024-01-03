import { useParams } from "react-router-dom";

function TitlePage() {
  const { slug } = useParams();

  return <h1>Title {slug}</h1>;
}

export default TitlePage;
