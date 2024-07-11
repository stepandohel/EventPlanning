import { useEffect, useState } from "react";
import { EventFieldDescription } from "../models/EventFieldDescription";

export const useEventFields = () => {
  const [eventFields, setEventFields] = useState<EventFieldDescription[]>();
  useEffect(() => {
    fetch("/EventField", {
      method: "Get",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((responce) => {
      responce.json().then((data) => {
        setEventFields(data);
      });
    });
  }, []);

  return { eventFields, setEvents: setEventFields };
};
