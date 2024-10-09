import { useState } from "react";
import GuideManagement from "../Manage Guides/GuideManagement";
import EventManagement from "../Manage Events/EventManagement";

export default function AdminPanel({ handleViewClick }) {
  const [selectedPage, setSelectedPage] = useState("Panel");
  const handlePageSelect = (page) => {
    setSelectedPage(page);
  };
  return (
    <>
      {selectedPage === "Panel" && (
        <div id="admin-panel">
          <h1>Admin Panel</h1>
          <ul>
            <li onClick={() => handlePageSelect("Events")}>Events</li>
            <li onClick={() => handlePageSelect("Guides")}> Guides</li>
          </ul>
          <h1 onClick={() => handleViewClick("")} className="return-btn">
            Return
          </h1>
        </div>
      )}
      {selectedPage === "Guides" && (
        <GuideManagement handlePageSelect={handlePageSelect} />
      )}
      {selectedPage === "Events" && (
        <EventManagement handlePageSelect={handlePageSelect} />
      )}
    </>
  );
}
