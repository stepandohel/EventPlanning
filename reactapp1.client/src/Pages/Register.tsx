import { Button, Input } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Register() {
  // state variables for email and passwords
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const navigate = useNavigate();

  // state variable for error messages
  const [error, setError] = useState("");

  const handleLoginClick = () => {
    navigate("/login");
  };

  // handle change events for input fields
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "confirmPassword") setConfirmPassword(value);
  };

  // handle submit event for the form
  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!email || !password || !confirmPassword) {
      setError("Please fill in all fields.");
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      setError("Please enter a valid email address.");
    } else if (password !== confirmPassword) {
      setError("Passwords do not match.");
    } else {
      setError("");
      fetch("/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
        }),
      })
        .then((data) => {
          console.log(data);
          if (data.ok) setError("Successful register.");
          else setError("Error registering.");
        })
        .catch((error) => {
          console.error(error);
          setError("Error registering.");
        });
    }
  };

  return (
    <div className="containerbox">
      <h3>Register</h3>

      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Email:</label>
        </div>
        <div>
          <Input
            type="email"
            id="email"
            name="email"
            value={email}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
        </div>
        <div>
          <Input
            type="password"
            id="password"
            name="password"
            value={password}
            onChange={handleChange}
          />
        </div>
        <div>
          <label htmlFor="confirmPassword">Confirm Password:</label>
        </div>
        <div>
          <Input
            type="password"
            id="confirmPassword"
            name="confirmPassword"
            value={confirmPassword}
            onChange={handleChange}
          />
        </div>
        <div className="registergbtns">
          <Button
            className="logingbtn"
            type="primary"
            onClick={() => {
              if (!email || !password || !confirmPassword) {
                setError("Please fill in all fields.");
              } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
                setError("Please enter a valid email address.");
              } else if (password !== confirmPassword) {
                setError("Passwords do not match.");
              } else {
                setError("");
                fetch("/register", {
                  method: "POST",
                  headers: {
                    "Content-Type": "application/json",
                  },
                  body: JSON.stringify({
                    email: email,
                    password: password,
                  }),
                })
                  .then((data) => {
                    console.log(data);
                    if (data.ok) setError("Successful register.");
                    else setError("Error registering.");
                  })
                  .catch((error) => {
                    console.error(error);
                    setError("Error registering.");
                  });
              }
            }}
          >
            Register
          </Button>
          <Button danger className="logingbtn" onClick={handleLoginClick}>
            Go to Login
          </Button>
        </div>
      </form>

      {error && <p className="error">{error}</p>}
    </div>
  );
}

export default Register;
