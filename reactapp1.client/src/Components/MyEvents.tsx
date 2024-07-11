import { Button, Skeleton, List } from "antd";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { Event } from "../models/Event";

const MyEvents = () => {
  const navigate = useNavigate();
  const [data, setData] = useState<Event[]>([]);
  useEffect(() => {
    fetch("/MyEvents", {
      method: "Get",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((responce) => {
      responce.json().then(setData);
    });
  }, []);
  return (
    <div>
      <List
        className="demo-loadmore-list"
        itemLayout="horizontal"
        dataSource={data}
        renderItem={(item) => (
          <List.Item
            key={item.name}
            actions={[
              <Button
                danger
                onClick={() => {
                  fetch(`/Events?eventId=${item.id}`, {
                    method: "Delete",
                    headers: {
                      "Content-Type": "application/json",
                    },
                  }).then(() => {
                    const dataCopy: Event[] = [...data];
                    const index = dataCopy.indexOf(item);
                    dataCopy.splice(index, 1);
                    setData(dataCopy);
                  });
                }}
              >
                Delete
              </Button>,
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
      <Button
        type="primary"
        onClick={() => {
          navigate("createevent");
        }}
      >
        Create new Event
      </Button>
    </div>
  );
};

export default MyEvents;
