import React, { useEffect } from "react";
import { useLocation } from "react-router-dom";

const ConfirmEmail = () => {
  const location = useLocation();
  useEffect(() => {
    console.dir(location.search);
  }, []);
  fetch(`/ConfirmEmail/${location.search}`, {
    method: "Get",
    headers: {
      "Content-Type": "application/json",
    },
  });
  return <div>Email confirmed</div>;
};

export default ConfirmEmail;
