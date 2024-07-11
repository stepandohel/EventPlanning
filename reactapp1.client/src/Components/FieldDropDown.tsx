import { Input, Select } from "antd";
import { EventFieldDescription } from "../models/EventFieldDescription";

type FieldDropDownProps = {
  eventFields: EventFieldDescription[];
  onChangeName: (value: string, id: number) => void;
  onChangeValue: (value: string, id: number) => void;
  id: number;
};

const FieldDropDown = ({
  eventFields,
  onChangeName,
  onChangeValue,
  id,
}: FieldDropDownProps) => {
  return (
    <div>
      <Select
        className="eventfield"
        onChange={(value) => {
          onChangeValue(value, id);
        }}
        options={eventFields.map((eventField) => {
          return { value: String(eventField.id), label: eventField.fieldName };
        })}
      />
      <Input
        className="eventfield"
        onChange={(e) => {
          onChangeName(e.target.value, id);
        }}
        placeholder="Value"
      />
    </div>
  );
};

export default FieldDropDown;
