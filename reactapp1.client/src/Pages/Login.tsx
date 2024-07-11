import { Checkbox, CheckboxProps, Input, Button } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
  // state variables for email and passwords
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [rememberme, setRememberme] = useState<boolean>(false);
  // state variable for error messages
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();

  // handle change events for input fields
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
  };

  const onChangeCheckBox: CheckboxProps["onChange"] = (e) => {
    setRememberme(e.target.checked);
  };
  const handleRegisterClick = () => {
    navigate("/register");
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!email || !password) {
      setError("Please fill in all fields.");
    } else {
      setError("");

      let loginurl = "";
      if (rememberme == true) loginurl = "/login?useCookies=true";
      else loginurl = "/login?useSessionCookies=true";

      fetch(loginurl, {
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
          if (data.ok) {
            setError("Successful Login.");
            window.location.href = "/";
          } else setError("Error Logging In.");
        })
        .catch((error) => {
          console.error(error);
          setError("Error Logging in.");
        });
    }
  };

  return (
    <div className="containerbox">
      <h3>Login</h3>
      <form onSubmit={handleSubmit}>
        <div>
          <label className="forminput" htmlFor="email">
            Email:
          </label>
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
          <Checkbox
            id="rememberme"
            name="rememberme"
            onChange={onChangeCheckBox}
          >
            Remember Me
          </Checkbox>
        </div>
        <div className="logingbtns">
          <Button
            className="logingbtn"
            type="primary"
            onClick={() => {
              if (!email || !password) {
                setError("Please fill in all fields.");
              } else {
                setError("");
              }
              let loginurl = "";
              if (rememberme == true) loginurl = "/login?useCookies=true";
              else loginurl = "/login?useSessionCookies=true";

              fetch(loginurl, {
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
                  if (data.ok) {
                    setError("Successful Login.");
                    navigate("/events");
                  } else setError("Error Logging In.");
                })
                .catch((error) => {
                  console.error(error);
                  setError("Error Logging in.");
                });
            }}
          >
            Login
          </Button>

          <Button danger className="logingbtn" onClick={handleRegisterClick}>
            Register
          </Button>
        </div>
      </form>
      {error && <p className="error">{error}</p>}
    </div>
  );
}

export default Login;
