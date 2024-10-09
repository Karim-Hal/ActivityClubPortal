import "bootstrap/dist/css/bootstrap.min.css";

import LoginForm from "./components/LoginForm";
import { useState, useEffect } from "react";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Home from "./components/HomePage/Home";
import Events from "./components/EventsPage/Events";
import Guides from "./components/GuidesPage/Guides";
import FirstView from "./components/FirstView";
import AdminLogin from "./components/AdminLogin";
import AdminPanel from "./components/Admin/AdminPanel/AdminPanel";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(
    localStorage.getItem("isLoggedIn") === "true"
  );
  const [currentUser, setCurrentUser] = useState({});
  const [currentView, setCurrentView] = useState("");
  const [isAdminLoggedIn, setIsAdminLoggedIn] = useState(
    localStorage.getItem("isAdminLoggedIn") === "true"
  );

  useEffect(() => {
    localStorage.setItem("isLoggedIn", isLoggedIn);
    localStorage.setItem("isAdminLoggedIn", isAdminLoggedIn);
  }, [isLoggedIn, isAdminLoggedIn]);

  const [selectedPage, setSelectedPage] = useState("Home");
  function handlePageChange(page) {
    setSelectedPage(page);
    console.log(selectedPage);
  }
  function handleSignOut() {
    setIsLoggedIn(false);
    setSelectedPage("Home");
    localStorage.clear();
  }
  function handleLogin(data) {
    setIsLoggedIn(data);
  }

  function setUser(user) {
    localStorage.setItem("user", JSON.stringify(user));
    setCurrentUser(JSON.parse(localStorage.getItem("user")));
  }
  function handleViewClick(view) {
    setCurrentView(view);
  }
  function handleAdminLogin(data) {
    setIsAdminLoggedIn(data);
  }
  function handleExploreClick() {
    setSelectedPage("Events");
  }
  localStorage.clear();
  return (
    <>
      {isLoggedIn ? (
        <>
          <Header
            id="menu-header"
            selectedPage={selectedPage}
            handlePageChange={handlePageChange}
            handleSignOut={handleSignOut}
          />
          <main id="page-content">
            {selectedPage === "Home" && (
              <Home
                memberEmail={currentUser.email}
                handleExploreClick={handleExploreClick}
              />
            )}
            {selectedPage === "Events" && (
              <Events memberEmail={currentUser.email} />
            )}
            {selectedPage === "Guides" && <Guides />}
          </main>
          <Footer />
        </>
      ) : currentView === "" ? (
        <FirstView handleViewClick={handleViewClick} />
      ) : currentView === "User" ? (
        <LoginForm changeLogin={handleLogin} saveUser={setUser} />
      ) : (
        (isAdminLoggedIn && (
          <AdminPanel handleViewClick={handleViewClick} />
        )) || (
          <AdminLogin
            handleAdminLogin={handleAdminLogin}
            handleViewClick={handleViewClick}
          />
        )
      )}
    </>
  );
}

export default App;
