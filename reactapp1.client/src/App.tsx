import "./App.css";

import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./Pages/Home";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import EventList from "./Components/EventList";
import MyEvents from "./Components/MyEvents";
import CreateEvent from "./Pages/CreateEvent";
import ConfirmEmail from "./Components/ConfirmEmail";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<Home />}>
          <Route path="events" element={<EventList url="/Events" />} />
          <Route path="myevents" element={<MyEvents />} />
          <Route path="myevents/createevent" element={<CreateEvent />} />
        </Route>
        <Route path="/confirmEmail" element={<ConfirmEmail />} />
      </Routes>
    </BrowserRouter>
  );
}
export default App;
