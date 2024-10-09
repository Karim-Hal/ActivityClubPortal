import { useState } from "react";
import axios from "axios";
export default function AdminLogin({ handleAdminLogin, handleViewClick }) {
  const [adminInfo, setAdminInfo] = useState({
    email: "",
    password: "",
    gender: "",
    fullName: "",
  });
  const handleChange = (e) => {
    setAdminInfo((info) => ({ ...info, [e.target.name]: e.target.value }));
  };
  const submitLogin = async () => {
    const response = await axios.post(
      "http://localhost:5004/User/LoginUser",
      adminInfo
    );

    localStorage.setItem("isAdminLoggedIn", response.data);
    handleAdminLogin(response.data);
    if (response.data === false) {
      alert("Wrong email or password!");
    }
  };

  return (
    <div id="admin-login">
      <h1 onClick={() => handleViewClick("")} className="return-btn">
        Return
      </h1>
      <div id="admin-login-container">
        <form id="admin-login-form">
          <h2>Login</h2>
          <div className="mb-3">
            <input
              type="email"
              name="email"
              onChange={(e) => handleChange(e)}
              className="form-control"
              aria-describedby="emailHelp"
              placeholder="Email"
              value={adminInfo.email}
              required
            />
          </div>
          <div>
            <input
              type="password"
              name="password"
              onChange={(e) => handleChange(e)}
              className="form-control"
              placeholder="Password"
              required
              value={adminInfo.password}
            />
          </div>
          <button type="button" onClick={submitLogin} id="admin-login-btn">
            Submit
          </button>
        </form>
      </div>
    </div>
  );
}
