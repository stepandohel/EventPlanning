import { useEffect, useState } from "react";
import { Event } from "../models/Event";
import { List, Skeleton } from "antd";
import EventListItem from "./EventListItem";

type eventListProps = {
  url: string;
};

const EventList = ({ url }: eventListProps) => {
  const [data, setData] = useState<Event[]>([]);
  function Subscribe(id: string) {
    fetch(`/Events/Subscribe?eventId=${id}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    }).then(() => {
      const dataCopy: Event[] = [...data];
      dataCopy.map((x) => {
        if (x.id === Number(id)) {
          x.currentMemberCount = x.currentMemberCount + 1;
          x.isRegistered = !x.isRegistered;
        }
      });
      setData(dataCopy);
    });
  }

  function Unsubscribe(id: string) {
    fetch(`/Events/Unsubscribe?eventId=${id}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    }).then(() => {
      const dataCopy: Event[] = [...data];
      dataCopy.map((x) => {
        if (x.id === Number(id)) {
          x.currentMemberCount = x.currentMemberCount - 1;
          x.isRegistered = !x.isRegistered;
        }
      });
      console.dir(dataCopy);
      setData(dataCopy);
    });
  }

  useEffect(() => {}, [data]);
  useEffect(() => {
    fetch(url, {
      method: "Get",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((responce) => {
      responce.json().then(setData);
    });
  }, [url]);

  return (
    <List
      className="demo-loadmore-list"
      itemLayout="horizontal"
      dataSource={data}
      renderItem={(item) => (
        <List.Item
          key={item.name}
          actions={[
            <EventListItem
              id={item.id}
              subscribe={Subscribe}
              unsubscribe={Unsubscribe}
              isRegistered={item.isRegistered}
            />,
          ]}
        >
          <Skeleton avatar title={false} loading={false} active>
            <div>
              <span className="eventlabel">Название ивента</span>
              <div>{item.name}</div>
              <span className="eventlabel">Количество участников</span>
              <div>
                {item.currentMemberCount}/{item.memberCount}
              </div>
              <span className="eventlabel">Дата и время проведения</span>
              <div>{item.dateTime}</div>
              {item.eventFields.map((eventField, index) => (
                <div key={index}>
                  <span className="eventlabel">
                    {eventField.fieldDescription.fieldName}
                  </span>
                  <div>{eventField.fieldValue}</div>
                </div>
              ))}
            </div>
          </Skeleton>
        </List.Item>
      )}
    />
  );
};

export default EventList;
