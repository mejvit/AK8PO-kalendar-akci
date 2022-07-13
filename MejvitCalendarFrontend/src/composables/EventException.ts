import { EventBase } from './EventBase'

export interface EventException {
  eventId?: number;
  event?: EventBase;
  date?: Date;
}
