import { EventField } from "./EventField";

export type Event = {
  id: number;
  name: string;
  memberCount: number;
  currentMemberCount: number;
  dateTime: string;
  isRegistered: boolean;
  eventFields: EventField[];
};
