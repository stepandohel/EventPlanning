import { Button, DatePicker, DatePickerProps, Input, Modal } from "antd";
import { useState } from "react";
import { EventFieldDescription } from "../models/EventFieldDescription";
import FieldDropDown from "../Components/FieldDropDown";
import { useEventFields } from "../hooks/useEventFields";
import { useNavigate } from "react-router-dom";

type Field = EventFieldDescription & {
  fieldType: string;
};

const newField: Field = {
  id: 0,
  fieldName: "",
  fieldType: "",
};

const CreateEvent = () => {
  const [name, setName] = useState<string>();
  const [memberCount, setMemberCount] = useState<number>();
  const [date, setDate] = useState<string>();

  const onChange: DatePickerProps["onChange"] = (date, dateString) => {
    setDate(dateString as string);
  };
  const navigate = useNavigate();

  const creteEvent = () => {
    const testarr = [];
    fields.map((item) => {
      testarr.push({
        fieldDescriptionId: item.fieldType,
        fieldValue: item.fieldName,
      });
    });
    fetch("/Events", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name,
        memberCount,
        dateTime: date,
        eventFields: testarr,
      }),
    }).then(() => {
      navigate("/events");
    });

    // const test = JSON.stringify({
    //   name,
    //   memberCount,
    //   date,
    //   eventFields: testarr,
    // });
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [fieldName, setFieldName] = useState<string>("");
  const [fieldType, setFieldType] = useState<string>("");
  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = () => {
    fetch("/EventField", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        fieldName,
        fieldType,
      }),
    }).then((resp) => {
      resp.json().then((item) => {
        setEvents((prev) => {
          return prev ? [...prev, item] : [item];
        });
      });
    });
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const { eventFields, setEvents } = useEventFields();

  const [fields, setFields] = useState<Field[]>([]);

  function addField() {
    console.dir(fields.length);
    const added = [...fields];
    const dataField = { ...newField };
    if (fields.length != 0) {
      dataField.id = added[added.length - 1].id + 1;
    }
    added.push(dataField);
    setFields(added);
  }

  function handleChangeName(value: string, index: number) {
    const valueNew: Field[] = [...fields];
    valueNew[index].fieldName = value;
    setFields(valueNew);
  }

  function handleChangeValue(value: string, index: number) {
    const valueNew: Field[] = [...fields];
    valueNew[index].fieldType = value;
    setFields(valueNew);
  }
  return (
    <div className="newcreatediv">
      <Input
        placeholder="Name"
        className="eventfield"
        onChange={(e) => {
          setName(e.target.value);
        }}
      />
      <Input
        placeholder="MemberCount"
        className="eventfield"
        onChange={(e) => {
          setMemberCount(Number(e.target.value));
        }}
      />
      <DatePicker
        format="YYYY-MM-DD HH:mm:ss"
        onChange={onChange}
        placeholder="DateTime"
        className="eventfield"
        showTime
      />
      <label>Fiels</label>
      {fields.map((item) => {
        return (
          <FieldDropDown
            eventFields={eventFields || []}
            id={item.id}
            onChangeName={handleChangeName}
            onChangeValue={handleChangeValue}
          />
        );
      })}
      <Button
        onClick={() => {
          addField();
        }}
      >
        Добавить поле
      </Button>
      <div className="eventbuttons">
        <Button type="primary" onClick={showModal}>
          Создать новое поле
        </Button>
        <Button type="primary" danger onClick={creteEvent}>
          Создать ивент
        </Button>
      </div>
      <Modal
        title="Basic Modal"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <div>
          <span className="eventlabel">Название поля</span>
          <Input
            type="text"
            id="fieldName"
            name="fieldName"
            value={fieldName}
            onChange={(e) => {
              setFieldName(e.target.value);
            }}
          />
          <span className="eventlabel">Тип поля</span>
          <Input
            type="text"
            id="fieldType"
            name="fieldType"
            value={fieldType}
            onChange={(e) => {
              setFieldType(e.target.value);
            }}
          />
        </div>
      </Modal>
    </div>
  );
};

export default CreateEvent;
