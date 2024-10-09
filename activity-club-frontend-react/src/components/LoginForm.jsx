import { useEffect, useRef, useState } from "react";
import axios from "axios";

export default function LoginForm({ changeLogin, saveUser }) {
  const signupFooter = (
    <div className="login-form-footer">
      Already have an account?{" "}
      <button id="register-btn" onClick={() => handleFormChange("Sign Up")}>
        Login
      </button>
    </div>
  );
  const loginFooter = (
    <div className="login-form-footer">
      Don't have an account?{" "}
      <button id="register-btn" onClick={() => handleFormChange("Login")}>
        Register
      </button>
    </div>
  );

  const [signupInfo, setSignupInfo] = useState({
    FullName: "",
    Email: "",
    Password: "",
    Gender: "",
  });
  const [signupInfoCombined, setSignupInfoCombined] = useState({});
  const [photo, setPhoto] = useState("");
  const [loginInfo, setLoginInfo] = useState({
    Email: "",
    Password: "",
  });

  const combinedInfo = {
    "Sign Up": signupInfo,
    Login: loginInfo,
  };

  const [currentForm, setCurrentForm] = useState("Sign Up");
  useEffect(() => {
    setLoginInfo({
      Email: "",
      Password: "",
    });
    setSignupInfo({
      FullName: "",
      Email: "",
      Password: "",
      Gender: "",
    });
  }, [currentForm]);

  const handleFormChange = (formType) => {
    setCurrentForm(formType === "Sign Up" ? "Login" : "Sign Up");
  };
  const handleChange = (e) => {
    if (currentForm === "Sign Up") {
      setSignupInfo((info) => ({
        ...info,
        [e.target.name]: e.target.value,
      }));
    } else {
      setLoginInfo((info) => ({
        ...info,
        [e.target.name]: e.target.value,
      }));
    }
  };

  const handlePhoto = (event) => {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      // Base64 string
      const base64String = reader.result;
      // Set the base64 string in the state

      setPhoto((p) => (p = base64String));
      var SignUpRequest = signupInfo;
      setSignupInfoCombined((info) => ({
        SignUpRequest,
        photo: base64String,
      }));
    };

    if (file) {
      // Read the file as a data URL (base64)
      reader.readAsDataURL(file);
    }
  };

  const handleSubmission = async (event) => {
    event.preventDefault();
    try {
      if (currentForm === "Sign Up") {
        const response = await axios.post(
          "http://localhost:5004/Member/Sign Up",
          signupInfoCombined
        );
        if (response.data) {
          setCurrentForm("Login");
        } else {
          alert("Email already in use!");
        }
      } else {
        const response = await axios.post(
          "http://localhost:5004/Member/Login",
          loginInfo
        );
        if (response.data === false) {
          alert("Account does not exist!");
        } else if (response.data !== "") {
          changeLogin(response.data != null);
          saveUser(response.data);
        } else {
          alert("Wrong email or password!");
        }
      }
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <div id="form-container">
      <form onSubmit={handleSubmission} id="login-signup-form">
        <h1>{currentForm}</h1>
        <hr />

        {currentForm === "Sign Up" ? (
          <div className="form-group">
            <input
              type="text"
              name="FullName"
              onChange={handleChange}
              className="form-control"
              placeholder="Full Name"
              value={signupInfo.FullName}
              required
            />
          </div>
        ) : null}

        <div className="form-group">
          <input
            type="email"
            name="Email"
            onChange={handleChange}
            className="form-control"
            aria-describedby="emailHelp"
            placeholder="Email"
            value={combinedInfo[currentForm].Email}
            required
          />
        </div>
        <div className="form-group">
          <input
            type="password"
            name="Password"
            onChange={handleChange}
            className="form-control"
            placeholder="Password"
            required
            value={combinedInfo[currentForm].Password}
          />
        </div>

        {currentForm === "Sign Up" ? (
          <div>
            <div className="gender-input">
              <input
                type="radio"
                name="Gender"
                onChange={handleChange}
                id="M"
                value="Male"
                required
              />
              <label htmlFor="M">Male</label>
            </div>
            <div className="gender-input">
              <input
                type="radio"
                name="Gender"
                onChange={handleChange}
                id="F"
                value="Female"
              />
              <label htmlFor="F">Female</label>
            </div>

            <div className="photo-input">
              <input
                className="form-control"
                type="file"
                onChange={handlePhoto}
                name="memberPhoto"
                required
                accept="image/*"
              />
            </div>
          </div>
        ) : null}

        <div>
          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </div>

        {currentForm === "Sign Up" ? signupFooter : loginFooter}
      </form>
    </div>
  );
}
