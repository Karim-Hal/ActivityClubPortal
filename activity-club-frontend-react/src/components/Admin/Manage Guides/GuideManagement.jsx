import { useEffect, useState } from "react";
import Guide from "../../GuidesPage/Guide";
import axios from "axios";

export default function GuideManagement({ handlePageSelect }) {
  const [guides, setGuides] = useState([]);
  const [boolSwitch, setBoolSwitch] = useState(false);
  const [guide, setGuide] = useState({
    id: 0,
    fullName: "",
    email: "",
    password: "",
    dateOfBirth: "",
    joiningDate: "",
    profession: "",
    photoBase64: "",
    photo: "",
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [selectedGuide, setSelectedGuide] = useState();

  const handleAddGuide = () => {
    setIsModalOpen(true);
  };

  const closeModal = (modalType) => {
    if (modalType == "Add") {
      setIsModalOpen(false);
    } else if (modalType == "Delete") {
      setIsDeleteModalOpen(false);
      setSelectedGuide(null);
    } else {
      setIsEditModalOpen(false);
      setSelectedGuide(null);
    }
  };
  const fetchGuides = async () => {
    const response = await axios.get("http://localhost:5004/Guide/GetGuides");
    setGuides([...response.data]);
  };

  const handlePhoto = async () => {
    const file = document.getElementById("photo-guide").files[0];

    const reader = new FileReader();

    reader.onloadend = () => {
      // Base64 string
      const base64String = reader.result;
      const base64Data = base64String.split(",")[1];
      // Set the base64 string in the state
      setGuide((prevGuide) => ({
        ...prevGuide,
        photoBase64: base64Data,
      }));
    };

    if (file) {
      // Read the file as a data URL (base64)
      reader.readAsDataURL(file);
    }
  };

  const handleChange = (event) => {
    const { name, value } = event.target;

    // Check if editing an existing guide (when the edit modal is open)
    if (isEditModalOpen) {
      setSelectedGuide((prevGuide) => ({
        ...prevGuide,
        [name]: value,
      }));

      setGuide((prevGuide) => ({
        ...selectedGuide,
      }));
    }

    setGuide((prevGuide) => ({
      ...prevGuide,
      [name]: value,
    }));
  };
  const handleGuideSubmit = async (e) => {
    e.preventDefault();

    if (selectedGuide != null) {
      const response = await axios.post(
        "http://localhost:5004/Guide/AddGuide",
        guide
      );

      setIsEditModalOpen(false);
      setBoolSwitch((b) => (b = !b));
    } else {
      const response = await axios.post(
        "http://localhost:5004/Guide/AddGuide",
        guide
      );
      setBoolSwitch((b) => (b = !b));

      setIsModalOpen(false);
    }
  };

  const handleDeleteModal = (guideId) => {
    setSelectedGuide(...guides.filter((guide) => guide.id == guideId));
    setIsDeleteModalOpen(true);
  };
  const handleEditModal = (guideId) => {
    const selected = guides.find((guide) => guide.id === guideId);
    setSelectedGuide(selected);

    // Set the guide state to match the selected guide

    setIsEditModalOpen(true);
  };
  const handleDeleteGuide = async (e, guideId) => {
    e.preventDefault();
    const response = await axios.delete(
      `http://localhost:5004/Guide/DeleteGuide/${guideId}`
    );
    setIsDeleteModalOpen(false);
    setSelectedGuide();
    setBoolSwitch((b) => (b = !b));
  };
  useEffect(() => {
    fetchGuides();
  }, [boolSwitch]);

  return (
    <>
      <div id="admin-guide">
        <div className="management-container">
          <div id="add-guide-container" onClick={handleAddGuide}>
            <i className="fa-solid fa-plus"></i>
          </div>

          <h1 onClick={() => handlePageSelect("Panel")}>Return</h1>
        </div>

        <div id="guide-container" className="guide-management-mod">
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
                handleDeleteGuide={() => handleDeleteModal(guide.id)}
                handleEditGuide={() => handleEditModal(guide.id)}
                canDelete={true}
              />
            ))}
          </ul>
        </div>
      </div>

      {isModalOpen && (
        <div className="event-modal-overlay" onClick={() => closeModal("Add")}>
          <form
            method="post"
            encType="multipart/form-data"
            className="event-modal-content admin-modal"
            onClick={(e) => e.stopPropagation()}
            onSubmit={handleGuideSubmit}
          >
            <div className="management-form">
              <div className="management-form-1">
                <div className="mb-3">
                  <label htmlFor="fullName" className="form-label">
                    Full Name:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="fullName"
                    name="fullName"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="email" className="form-label">
                    Email:
                  </label>
                  <input
                    type="email"
                    className="form-control"
                    id="email"
                    name="email"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="password" className="form-label">
                    Password:
                  </label>
                  <input
                    type="password"
                    className="form-control"
                    id="password"
                    name="password"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="dateOfBirth" className="form-label">
                    Date of Birth:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateOfBirth"
                    name="dateOfBirth"
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>
              <div className="management-form-2">
                <div className="mb-3">
                  <label htmlFor="joiningDate" className="form-label">
                    Joining Date:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="joiningDate"
                    name="joiningDate"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="profession" className="form-label">
                    Profession:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="profession"
                    name="profession"
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="photo" className="form-label">
                    Photo:
                  </label>
                  <input
                    type="file"
                    className="form-control"
                    id="photo-guide"
                    name="photo"
                    required
                    accept="image/*"
                    onChange={handlePhoto}
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
            encType="multipart/form-data"
            className="event-modal-content admin-modal"
            onClick={(e) => e.stopPropagation()}
            onSubmit={handleGuideSubmit}
          >
            <div className="management-form">
              <div className="management-form-1">
                <div className="mb-3">
                  <label htmlFor="fullName" className="form-label">
                    Full Name:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="fullName"
                    name="fullName"
                    value={selectedGuide.fullName}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="email" className="form-label">
                    Email:
                  </label>
                  <input
                    type="email"
                    className="form-control"
                    id="email"
                    name="email"
                    value={selectedGuide.email}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="password" className="form-label">
                    Password:
                  </label>
                  <input
                    type="password"
                    className="form-control"
                    id="password"
                    name="password"
                    required
                    value={selectedGuide.password}
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="dateOfBirth" className="form-label">
                    Date of Birth:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dateOfBirth"
                    name="dateOfBirth"
                    value={selectedGuide.dateOfBirth}
                    required
                    onChange={handleChange}
                  />
                </div>
              </div>
              <div className="management-form-2">
                <div className="mb-3">
                  <label htmlFor="joiningDate" className="form-label">
                    Joining Date:
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="joiningDate"
                    name="joiningDate"
                    value={selectedGuide.joiningDate}
                    required
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="profession" className="form-label">
                    Profession:
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="profession"
                    name="profession"
                    required
                    value={selectedGuide.profession}
                    onChange={handleChange}
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="photo" className="form-label">
                    Photo:
                  </label>
                  <input
                    type="file"
                    className="form-control"
                    id="photo-guide"
                    name="photo"
                    required
                    accept="image/*"
                    onChange={handlePhoto}
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
          className="event-modal-overlay delete-modal"
          onClick={() => closeModal("Delete")}
        >
          <form
            className="event-modal-content"
            onClick={(e) => e.stopPropagation()}
            onSubmit={(e) => handleDeleteGuide(e, selectedGuide.id)}
          >
            <div>
              Are you sure you want to delete{" "}
              <em>{selectedGuide.fullName}'s</em> account?
            </div>

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
