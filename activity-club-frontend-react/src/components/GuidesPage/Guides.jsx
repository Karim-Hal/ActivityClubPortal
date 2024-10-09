import { useEffect, useState } from "react";
import axios from "axios";
import Guide from "./Guide";

export default function Guides() {
  const [guides, setGuides] = useState([]);

  const fetchGuides = async () => {
    try {
      const response = await axios.get("http://localhost:5004/Guide/GetGuides");
      setGuides([...response.data]);
      console.log(response.data);
    } catch (error) {
      console.error("Error fetching guides:", error);
    }
  };
  useEffect(() => {
    fetchGuides();
  }, []);

  return (
    <div id="guides">
      <div id="padding-div"></div>
      <div id="guides-content">
        <h2>Meet Your Guides!</h2>
        <ul id="guide-grid">
          {guides.map((guide, index) => (
            <Guide
              key={index}
              email={guide.email}
              photo={guide.photoBase64}
              fullName={guide.fullName}
              dateOfBirth={guide.dateOfBirth}
              joiningDate={guide.joiningDate}
              profession={guide.profession}
            />
          ))}
        </ul>
      </div>
    </div>
  );
}
