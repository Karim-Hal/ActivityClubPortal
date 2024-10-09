import { useEffect, useState } from "react";
import axios from "axios";
import Event from "../../EventsPage/Event";
import images from "../../EventsPage/eventImages";

export default function EventManagement({ handlePageSelect }) {
  const [events, setEvents] = useState([]);
  const [boolSwitch, setBoolSwitch] = useState(false);
  const [event, setEvent] = useState({
    id: 0,
    name: "",
    description: "",
    destination: "",
    dateFrom: "",
    dateTo: "",
    cost: 0,
    status: "",
    categoryId: 0,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState();

  const handleAddEvent = () => {
    setIsModalOpen(true);
  };

  const closeModal = (modalType) => {
    if (modalType === "Add") {
      setIsModalOpen(false);
    } else if (modalType === "Delete") {
      setIsDeleteModalOpen(false);
      setSelectedEvent((prevEvent) => ({
        id: 0,
        name: "",
        description: "",
        destination: "",
        dateFrom: "",
        dateTo: "",
        cost: 0,
        status: "",
        categoryId: 0,
      }));
    } else {
      setIsEditModalOpen(false);
      setSelectedEvent((prevEvent) => ({
        id: 0,
        name: "",
        description: "",
        destination: "",
        dateFrom: "",
        dateTo: "",
        cost: 0,
        status: "",
        categoryId: 0,
      }));
    }
  };

  const fetchEvents = async () => {
    const response = await axios.get(
      "http://localhost:5004/Event/GetAllEvents"
    );
    setEvents([...response.data]);
  };

  const handleChange = (event) => {
    const { name, value } = event.target;

    if (isEditModalOpen) {
      setSelectedEvent((prevEvent) => ({
        ...prevEvent,
        [name]: value,
      }));

      setEvent((prevEvent) => ({
        ...selectedEvent,
      }));
    }

    setEvent((prevEvent) => ({
      ...prevEvent,
      [name]: value,
    }));
  };

  const handleEventSubmit = async (e) => {
    e.preventDefault();

    if (selectedEvent.id != 0) {
      const response = await axios.post(
        "http://localhost:5004/Event/AddEvent",
        event
      );

      setIsEditModalOpen(false);

      setBoolSwitch((b) => !b);
      setEvent((prevEvent) => ({
        id: 0,
        name: "",
        description: "",
        destination: "",
        dateFrom: "",
        dateTo: "",
        cost: 0,
        status: "",
        categoryId: 0,
      }));
    } else {
      const response = await axios.post(
        "http://localhost:5004/Event/AddEvent",
        event
      );

      setBoolSwitch((b) => !b);

      setIsModalOpen((m) => (m = false));
    }
  };

  const handleDeleteModal = (eventId) => {
    setSelectedEvent(...events.filter((event) => event.id === eventId));
    setIsDeleteModalOpen(true);
  };

  const handleEditModal = (eventId) => {
    const selected = events.find((event) => event.id === eventId);
    setSelectedEvent((s) => (s = selected));

    setIsEditModalOpen(true);
  };

  useEffect(() => {
    if (selectedEvent !== null) {
      // This will execute after selectedEvent has been updated
      setEvent({ ...selectedEvent });
    }
  }, [selectedEvent]);
  const handleDeleteEvent = async (e, eventId) => {
    e.preventDefault();
    const response = await axios.delete(
      `http://localhost:5004/Event/DeleteEvent/${eventId}`
    );
    setIsDeleteModalOpen(false);
    setSelectedEvent((prevEvent) => ({
      id: 0,
      name: "",
      description: "",
      destination: "",
      dateFrom: "",
      dateTo: "",
      cost: 0,
      status: "",
      categoryId: 0,
    }));
    setBoolSwitch((b) => !b);
  };

  useEffect(() => {
    fetchEvents();
  }, [boolSwitch]);

  return (
    <>
      <div id="admin-guide">
        <div className="management-container">
          <div id="add-guide-container" onClick={handleAddEvent}>
            <i className="fa-solid fa-plus"></i>
          </div>

          <h1 onClick={() => handlePageSelect("Panel")}>Return</h1>
        </div>

        <div id="guide-container" className="guide-management-mod">
          <ul id="guide-grid">
            {events.map((event, index) => (
              <Event
                name={event.name}
                imgSrc={[images[event.categoryId - 1]]}
                description={event.description}
                destination={event.destination}
                dateFrom={event.dateFrom}
                dateTo={event.dateTo}
                cost={event.cost}
                status={event.status}
                categoryId={event.categoryId}
                handleDeleteEvent={() => handleDeleteModal(event.id)}
                handleEditEvent={() => handleEditModal(event.id)}
                canDelete={true}
                keyIdx={index}
              />
            ))}
          </ul>
        </div>
      </div>

      {isModalOpen && (
        <div className="event-modal-overlay" onClick={() => closeModal("Add")}>
          <form
            method="post"
            className="event-modal-content admin-modal"
            onClick={(e) => e.stopPropagation()}
            onSubmit={handleEventSubmit}
          >
            <div className="management-form">
              <div className="management-form-1">
                <div className="mb-3">
                  <label htmlFor="name" className="form-label">
                    Event Name:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="name"
                    name="name"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="description" className="form-label">
                    Description:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="description"
                    name="description"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="destination" className="form-label">
                    Destination:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="destination"
                    name="destination"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="dateFrom" className="form-label">
                    Date From:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateFrom"
                    name="dateFrom"
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>
              <div className="management-form-2">
                <div className="mb-3">
                  <label htmlFor="dateTo" className="form-label">
                    Date To:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateTo"
                    name="dateTo"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="cost" className="form-label">
                    Cost:
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="cost"
                    name="cost"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="status" className="form-label">
                    Status:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="status"
                    name="status"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="categoryId" className="form-label">
                    Category ID:
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="categoryId"
                    name="categoryId"
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>
            </div>

            <button type="submit" className="btn btn-primary">
              Submit
            </button>
          </form>
        </div>
      )}

      {isEditModalOpen && (
        <div className="event-modal-overlay" onClick={() => closeModal("Edit")}>
          <form
            method="post"
            className="event-modal-content admin-modal"
            onClick={(e) => e.stopPropagation()}
            onSubmit={handleEventSubmit}
          >
            <div className="management-form">
              <div className="management-form-1">
                <div className="mb-3">
                  <label htmlFor="name" className="form-label">
                    Event Name:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="name"
                    name="name"
                    value={selectedEvent.name}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="description" className="form-label">
                    Description:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="description"
                    name="description"
                    value={selectedEvent.description}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="destination" className="form-label">
                    Destination:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="destination"
                    name="destination"
                    value={selectedEvent.destination}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="dateFrom" className="form-label">
                    Date From:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateFrom"
                    name="dateFrom"
                    value={selectedEvent.dateFrom}
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>

              <div className="management-form-2">
                <div className="mb-3">
                  <label htmlFor="cost" className="form-label">
                    Cost:
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="cost"
                    name="cost"
                    value={selectedEvent.cost}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="status" className="form-label">
                    Status:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="status"
                    name="status"
                    value={selectedEvent.status}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="categoryId" className="form-label">
                    Category ID:
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="categoryId"
                    name="categoryId"
                    value={selectedEvent.categoryId}
                    required
                    onChange={handleChange}
                  />
                </div>
                <div className="mb-3">
                  <label htmlFor="dateTo" className="form-label">
                    Date To:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateTo"
                    name="dateTo"
                    value={selectedEvent.dateTo}
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>
            </div>

            <button type="submit" className="btn btn-primary">
              Submit
            </button>
          </form>
        </div>
      )}

      {isDeleteModalOpen && (
        <div
          className="event-modal-overlay"
          onClick={() => closeModal("Delete")}
        >
          <form
            className="event-modal-content delete-modal"
            onClick={(e) => e.stopPropagation()}
            onSubmit={(e) => handleDeleteEvent(e, selectedEvent.id)}
          >
            <div>Are you sure you want to delete this event?</div>

            <div className="delete-modal-btns">
              <button type="submit">Yes</button>
              <button type="button" onClick={() => closeModal("Delete")}>
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}
    </>
  );
}
