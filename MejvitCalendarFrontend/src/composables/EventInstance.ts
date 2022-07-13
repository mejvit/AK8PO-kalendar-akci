import { EventBase } from './EventBase'

export interface EventInstance {
  id?: number;
  startDateTime?: Date;
  endDateTime?: Date;
  name?: string;
  eventBase?: EventBase;
}
