import { useEffect, useState } from "react";
import EventFilters from "./EventFilters";
import Event from "./Event.jsx";
import axios from "axios";
import images from "./eventImages.js";
export default function Events({ memberEmail }) {
  const [events, setEvents] = useState([]);
  const [selectedEvent, setSelectedEvent] = useState();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [filters, setFilters] = useState([
    0,
    "Select Event Status",
    "Sort by date",
    false,
    memberEmail,
  ]);
  const handleRegisterClick = async (eventId) => {
    const response = await axios.post(
      `http://localhost:5004/Event/RegisterEvent/${memberEmail}/${eventId}`
    );
    if (response.data === true) {
      alert("Registered Successfully!");
    } else {
      alert("You have already registered this event!");
    }
  };
  const handleFilterChange = (filterIdx, value) => {
    const filtersArr = [...filters];
    filtersArr[filterIdx] = value;

    setFilters(filtersArr);
  };

  const handleEventClick = (eventIdx) => {
    setSelectedEvent(events[eventIdx]);
    setIsModalOpen(true);
  };
  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedEvent(null);
  };
  const fetchEvents = async () => {
    const url = `http://localhost:5004/Event/GetEvents/${filters[0]}/${filters[1]}/${filters[2]}/${filters[3]}/${filters[4]}`;
    console.log("Fetching events from:", url); // Log the URL
    const response = await axios.get(url);
    setEvents([...response.data]);
    console.log(events);
  };

  useEffect(() => {
    fetchEvents();
  }, [filters]);

  return (
    <div id="events">
      <div id="events-content">
        <ul id="filter-container">
          <li>
            <h3 id="filter-by-title">Filter By:</h3>
          </li>

          <EventFilters handleFilterChange={handleFilterChange} />
        </ul>
      </div>
      <div id="event-grid">
        <ul>
          {events.map((e, index) => (
            <Event
              name={e.name}
              imgSrc={[images[e.categoryId - 1]]}
              status={e.status}
              description={e.description}
              dateFrom={e.dateFrom}
              key={index}
              keyIdx={index}
              handleEventClick={handleEventClick}
            ></Event>
          ))}
        </ul>
      </div>
      {isModalOpen && (
        <div className="event-modal-overlay" onClick={closeModal}>
          <div
            className="event-modal-content"
            onClick={(e) => e.stopPropagation()}
          >
            <h2>{selectedEvent.name}</h2>
            <ul>
              <li>Description: {selectedEvent.description}</li>
              <li>Cost: {selectedEvent.cost}</li>
              <li>From: {selectedEvent.dateFrom}</li>
              <li>To: {selectedEvent.dateTo}</li>
              <li>Status: {selectedEvent.status}</li>
            </ul>
            <div className="event-card-btns">
              <button onClick={closeModal}>Close</button>
              <button onClick={() => handleRegisterClick(selectedEvent.id)}>
                Register for event
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
