import { EventException } from './EventException'
import { EventCategory } from './EventCategory'
import { Place } from './Place'

export interface EventBase {
  id?: number;
  name?: string;
  rRule?: string;
  placeId?: number;
  place?: Place;
  categoryId?: number;
  category?: EventCategory;
  eventExceptions?: Array<EventException>;
  startTime?: Date;
  endTime?: Date;
}
