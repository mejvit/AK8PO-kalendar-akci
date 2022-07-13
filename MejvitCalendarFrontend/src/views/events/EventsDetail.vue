<template>
  <h1>{{editMode ? 'Upravit' : 'Přidat' }} událost</h1>
  <section class="detail-form">
  <ul>
    <li>
      <div v-if="editMode">
        <span>Místo:</span>
        <span>{{ event.place.name }}</span>
      </div>
      <label v-else>
        <span>Místo:</span>
        <select v-model="event.placeId">
          <option v-for="place in places" :key="place.id" :value="place.id">{{ place.name }}</option>
        </select>
      </label>
      <label class="grow-3">
        <span>Název:</span>
        <input type="text" v-model="event.name">
      </label>
    </li>
    <li v-if="editMode">
      <div class="grow-0">
        <span>Začátek:</span>
        <datepicker style="display: inline-block; width: 8em; margin-right: 0.5em;"
          v-model="eventInstance.startDateTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
        <time-picker v-model="eventInstance.startDateTime" />
      </div>
      <span> — </span>
      <div class="grow-3">
        <span>Konec:</span>
        <time-picker v-model="eventInstance.endDateTime" />
        <datepicker style="display: inline-block; width: 8em; margin-left: 0.5em;"
          v-model="eventInstance.endDateTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
      </div>
    </li>
    <li v-else>
      <div class="grow-0">
        <span>Začátek:</span>
        <datepicker style="display: inline-block; width: 8em; margin-right: 0.5em;"
          v-model="event.startTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
        <time-picker v-model="event.startTime" />
      </div>
      <span> — </span>
      <div class="grow-3">
        <span>Konec:</span>
        <time-picker v-model="event.endTime" />
        <datepicker style="display: inline-block; width: 8em; margin-left: 0.5em;"
          v-model="event.endTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
      </div>
    </li>
    <li>
      <label>
        <span>Kategorie:</span>
        <select v-model="event.categoryId" class="w-auto">
          <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.name }}</option>
        </select>
      </label>
    </li>
    <li>
      <label class="grow-0">
        <input type="checkbox" v-model="eventHasRRule">
        <span class="inline">Opakovat</span>
      </label>
    </li>
  </ul>
  <div v-if="eventHasRRule">
  <h3>Opakující se událost</h3>
    <ul>
      <li>
        <label class="inline">
          <span>Opakuje se jednou za</span>
          <input type="number" min="1" v-model="rruleOptions.interval">
          <select v-model="rruleOptions.freq">
            <option :value="RRule.DAILY">{{ freqString.daily }}</option>
            <option :value="RRule.WEEKLY">{{ freqString.weekly }}</option>
            <option :value="RRule.MONTHLY">{{ freqString.monthly }}</option>
            <option :value="RRule.YEARLY">{{ freqString.yearly }}</option>
          </select>
        </label>
      </li>
      <li v-if="rruleOptions.freq == RRule.WEEKLY" style="justify-content: flex-start;">
        <label class="inline grow-0"><input type="checkbox" :value="0" v-model="rruleOptions.byweekday" /><span>Po</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="1" v-model="rruleOptions.byweekday" /><span>Út</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="2" v-model="rruleOptions.byweekday" /><span>St</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="3" v-model="rruleOptions.byweekday" /><span>Čt</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="4" v-model="rruleOptions.byweekday" /><span>Pá</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="5" v-model="rruleOptions.byweekday" /><span>So</span></label>
        <label class="inline grow-0"><input type="checkbox" :value="6" v-model="rruleOptions.byweekday" /><span>Ne</span></label>
      </li>
      <li v-if="rruleOptions.freq == RRule.MONTHLY">
        <input type="radio" name="toggleMonthlyRepeating" value="dayNr" v-model="monthlyRepeatingToggler" id=""/>
        <label class="inline">
          <span>Vždy </span>
          <input id="rruleCount" type="number" v-model="rruleOptions.bymonthday" min="1" max="31" @focus="() => { monthlyRepeatingToggler = 'dayNr'}"/>
          <span>. dne v měsíci</span>
        </label>
      </li>
      <li v-if="rruleOptions.freq == RRule.MONTHLY">
        <input type="radio" name="toggleMonthlyRepeating" value="weekday" v-model="monthlyRepeatingToggler" id="">
        <span style="top: 0.4em;">Každý</span>
        <select name="" id="" v-model="monthFreq.dayNr" @focus="() => { monthlyRepeatingToggler = 'weekday' }">
          <option value="1">první</option>
          <option value="2">druhý</option>
          <option value="3">třetí</option>
          <option value="4">čtvrtý</option>
          <option value="5">pátý</option>
          <option value="-1">poslední</option>
          <option value="-2">předposlední</option>
        </select>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="0" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>Po</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="1" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>Út</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="2" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>St</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="3" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>Čt</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="4" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>Pá</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="5" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>So</span>
        </label>
        <label class="inline grow-0">
          <input type="radio" name="byMonthDay" :value="6" v-model="monthFreq.weekday" @change="() => {monthlyRepeatingToggler = 'weekday'}"/><span>Ne</span>
        </label>
        <span style="top: 0.4em;">v měsíci</span>
      </li>
    </ul>
    <h3>Končí</h3>
    <ul>
      <li>
        <label>
          <input type="radio" name="toggleUntilCount" id="" value="never" v-model="rruleUntilToggler">
          <span style="top: 0; display: inline-block;margin-left: 1em;">Nikdy</span>
        </label>
      </li>
      <li>
        <input type="radio" name="toggleUntilCount" id="" value="datetime" v-model="rruleUntilToggler">
        <label>
          <span>Datum:</span>
          <datepicker id="rruleUntil" class="date" style="width: auto;"
            v-model="rruleOptions.until" :locale="cs" inputFormat="dd. MM. yyyy"
            @focus="() => {rruleUntilToggler = 'datetime'}"
          />
        </label>
      </li>
      <li>
        <input type="radio" name="toggleUntilCount" id="" value="count" v-model="rruleUntilToggler" />
        <label class="inline">
          <span>Po </span>
          <input id="rruleCount" type="number" v-model="rruleOptions.count" @focus="() => {rruleUntilToggler = 'count'}" />
          <span> opakováních</span>
        </label>
      </li>
    </ul>
  </div>
  </section>

  <br>

  <div class="input-buttons">
    <button class="button-back" @click="$router.push('/events')">Zpět</button>
    <button class="button-save" @click="saveEvent">Uložit</button>
  </div>
  <p>{{ event.rrule }}</p>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, reactive, ref, watch } from 'vue'
import { EventBase } from '@/composables/EventBase'
import axios from 'axios'
import { Place } from '@/composables/Place'
import TimePicker from '@/components/inputs/TimePicker.vue' // @ is an alias to /src
import Datepicker from 'vue3-datepicker'
import { cs } from 'date-fns/locale'
import { ByWeekday, Options as RRuleOptions, RRule } from 'rrule'
import { EventCategory } from '@/composables/EventCategory'
import { EventInstance } from '@/composables/EventInstance'

export default defineComponent({
  name: 'EventsDetail',
  components: {
    Datepicker, TimePicker
  },
  props: {
    editMode: {
      type: Boolean,
      default: false,
      required: true
    },
    placeCode: {
      type: String,
      required: false
    },
    eventInstanceId: {
      type: Number,
      required: false
    }
  },
  setup (props) {
    onMounted(() => {
      console.log(props.editMode, props.placeCode, props.eventInstanceId)
      axios.get<EventInstance>('/api/event-instances/' + props.eventInstanceId)
        .then(function (response) {
          // handle success
          console.log(response.data)
          eventInstance.startDateTime = new Date(response.data.startDateTime as Date)
          eventInstance.endDateTime = new Date(response.data.endDateTime as Date)
          if (response.data.eventBase !== undefined) {
            event.id = response.data.eventBase.id
            event.name = response.data.eventBase.name
            event.startTime = new Date(response.data.eventBase.startTime as Date)
            event.endTime = new Date(response.data.eventBase.endTime as Date)
            event.place = response.data.eventBase.place
            event.categoryId = response.data.eventBase.categoryId
            event.category = response.data.eventBase.category
            if (response.data.eventBase.rRule !== undefined) {
              event.rRule = response.data.eventBase.rRule
              console.log(response.data.eventBase.rRule)
              const parsedRRule = RRule.fromString(response.data.eventBase.rRule as string)
              Object.assign(rruleOptions, parsedRRule.options)
              eventHasRRule.value = true
            }
          }
        })
        .catch(function (error) {
          // handle error
          console.error(error)
        })
        .then(function () {
          // always executed
        })

      axios.get<Array<Place>>('/api/places')
        .then(function (response) {
          // handle success
          console.log(response.data)
          places.value = response.data
        })
        .catch(function (error) {
          // handle error
          console.error(error)
        })
        .then(function () {
          // always executed
        })

      axios.get<Array<EventCategory>>('/api/categories/')
        .then(function (response) {
          console.log(response.data)
          categories.value = response.data
        })
        .catch(function (error) {
          // handle error
          console.error(error)
        })
        .then(function () {
          // always executed
        })
    })
    const event = reactive<EventBase>({
      name: '',
      startTime: new Date(),
      endTime: new Date(),
      place: {
        name: '',
        code: ''
      }
    })

    const eventInstance = reactive<EventInstance>({
      startDateTime: new Date(),
      endDateTime: new Date()
    })

    const endTime = ref<Date>(new Date())
    const startTime = ref<Date>(new Date())
    const rruleOptions: RRuleOptions = reactive({
      freq: RRule.WEEKLY,
      dtstart: null,
      interval: 1,
      wkst: null,
      count: null,
      until: null,
      tzid: 'Europe/Prague',
      bysetpos: null,
      bymonth: null,
      bymonthday: null,
      bynmonthday: null,
      byyearday: null,
      byweekno: null,
      byweekday: [(new Date().getDay() + 6) % 7],
      bynweekday: null,
      byhour: null,
      byminute: null,
      bysecond: null,
      byeaster: null
    })
    const places = ref<Array<Place>>([])
    const categories = ref<Array<EventCategory>>([])

    const eventName = ref<string>('test')

    const isRecurrent = ref<boolean>(true)
    const logRRule = function () {
      console.log(new RRule(rruleOptions).toString())
    }

    const saveEvent = function () {
      axios.post('/api/events', event)
        .then(response => {
          console.log(response)
        })
    }

    watch(rruleOptions, () => {
      event.rRule = new RRule(rruleOptions).toString()
    })
    watch(
      () => rruleOptions.freq,
      () => {
        if (rruleOptions.freq === RRule.DAILY || rruleOptions.freq === RRule.YEARLY) {
          rruleOptions.byweekday = null
        } else if (rruleOptions.freq === RRule.WEEKLY || rruleOptions.freq === RRule.MONTHLY) {
          rruleOptions.byweekday = [(new Date().getDay() + 6) % 7]
        }
      }
    )

    const eventHasRRule = ref<boolean>(false)
    watch(eventHasRRule, (val) => {
      if (!val) {
        event.rRule = undefined
      } else {
        event.rRule = new RRule(rruleOptions).toString()
      }
    })

    const monthlyRepeatingToggler = ref<string>('dayNr')
    watch(monthlyRepeatingToggler, (val) => {
      switch (val) {
        case 'dayNr':
          rruleOptions.byweekday = null
          break
        case 'weekday':
          rruleOptions.bymonthday = null
          break
        default:
          break
      }
    })
    const rruleUntilToggler = ref<string>('never')
    watch(rruleUntilToggler, (val) => {
      switch (val) {
        case 'until':
          rruleOptions.count = null
          break

        case 'count':
          rruleOptions.until = null
          break

        default:
          rruleOptions.until = null
          rruleOptions.count = null
          break
      }
    })

    const freqString = computed(() => {
      if (rruleOptions.interval < 2) {
        return {
          daily: 'den',
          weekly: 'týden',
          monthly: 'měsíc',
          yearly: 'rok'
        }
      } else if (rruleOptions.interval < 5) {
        return {
          daily: 'dny',
          weekly: 'týdny',
          monthly: 'měsíce',
          yearly: 'roky'
        }
      }
      return {
        daily: 'dní',
        weekly: 'týdnů',
        monthly: 'měsíců',
        yearly: 'let'
      }
    })

    const monthFreq = reactive<{weekday: ByWeekday, dayNr: number}>({
      weekday: (new Date().getDay() + 6) % 7,
      dayNr: 1
    })

    watch([() => monthFreq.weekday, () => monthFreq.dayNr], () => {
      switch (monthFreq.weekday) {
        case 0:
          rruleOptions.byweekday = RRule.MO.nth(monthFreq.dayNr)
          break
        case 1:
          rruleOptions.byweekday = RRule.TU.nth(monthFreq.dayNr)
          break
        case 2:
          rruleOptions.byweekday = RRule.WE.nth(monthFreq.dayNr)
          break
        case 3:
          rruleOptions.byweekday = RRule.TH.nth(monthFreq.dayNr)
          break
        case 4:
          rruleOptions.byweekday = RRule.FR.nth(monthFreq.dayNr)
          break
        case 5:
          rruleOptions.byweekday = RRule.SA.nth(monthFreq.dayNr)
          break
        case 6:
          rruleOptions.byweekday = RRule.SU.nth(monthFreq.dayNr)
          break
      }
    })

    return {
      categories,
      endTime,
      event,
      eventInstance,
      eventName,
      isRecurrent,
      places,
      rruleOptions,
      RRule,
      eventHasRRule,
      rruleUntilToggler,
      startTime,
      cs,
      logRRule,
      saveEvent,
      freqString,
      monthFreq,
      monthlyRepeatingToggler
    }
  }
})
</script>

<style scoped>
  .v3dp__datepicker {
    display: inline-block;
  }
</style>
