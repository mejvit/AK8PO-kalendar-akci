import { EventCategory } from './EventCategory'
import { Place } from './Place'

export interface EventBase {
    id?: number,
    name: string,
    startTime: Date,
    endTime: Date,
    rrule?: string
    place?: Place
    placeId?: number,
    categoryId?: number,
    category?: EventCategory
}
