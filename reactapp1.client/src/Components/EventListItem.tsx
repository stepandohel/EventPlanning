import { Button } from "antd";
import { subscribe } from "diagnostics_channel";

type EventListItemProps = {
  id: number;
  subscribe: (id: string) => void;
  unsubscribe: (id: string) => void;
  isRegistered: boolean;
};

const EventListItem = ({
  id,
  subscribe,
  unsubscribe,
  isRegistered,
}: EventListItemProps) => {
  return (
    <Button
      onClick={() => {
        if (!isRegistered) {
          subscribe(String(id));
        } else {
          unsubscribe(String(id));
        }
      }}
    >
      {!isRegistered ? "Subscribe" : "Unsubscribe"}
    </Button>
  );
};

export default EventListItem;
