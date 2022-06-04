<template>
  <h1>Přidat událost</h1>
  <section class="detail-form">
  <ul>
    <li>
      <label>
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
    <li>
      <label class="grow-0">
        <span>Začátek:</span>
        <datepicker class="date" style="display: inline-block;"
          v-model="event.startTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
        <time-picker v-model="event.startTime" />
      </label>
      <span> — </span>
      <label class="grow-3">
        <span>Konec:</span>
        <time-picker v-model="event.endTime" />
        <datepicker class="date"
          v-model="event.endTime" :locale="cs" inputFormat="dd. MM. yyyy"
        />
      </label>
    </li>
    <li>
      <label>
        <span>Kategorie:</span>
        <select v-model="event.categoryId">
          <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.name }}</option>
        </select>
      </label>
    </li>
    <li>
      <label>
        <input type="checkbox" class="grow-0">
        <span class="inline">Opakovat</span>
      </label>
    </li>
  </ul>
  <h2>Opakující se událost</h2>
  <ul>
    <li></li>
  </ul>
  <h3>Končí</h3>
  <ul>
    <li>
      <label>
        <span>Datum:</span>
        <datepicker id="rruleUntil" class="date" style="width: auto;"
          v-model="rruleOptions.until" :locale="cs" inputFormat="dd. MM. yyyy"
        />
      </label>
    </li>
    <li>
      <label>
        <span>Po opakováních:</span>
        <input id="rruleCount" type="number" v-model="rruleOptions.count" style="width: auto;" />
      </label>
    </li>
  </ul>
  </section>

  <br>

  <div class="input-buttons">
    <button class="button-back" @click="$router.push('/events')">Zpět</button>
    <button class="button-save" @click="saveEvent">Uložit</button>
  </div>
  <p>{{ event.rrule }}</p>
  <br>
  <br>
  <br>
  <br>
  <br>
  <br>
  <!---->
  <datepicker class="date" style="display: inline-block;"
    v-model="startTime" :locale="cs" inputFormat="dd. MM. yyyy"
  />
  <time-picker  v-model="startTime" />
  <span> — </span>
  <time-picker v-model="endTime" />
  <datepicker class="date"
    v-model="endTime" :locale="cs" inputFormat="dd. MM. yyyy"
  />
  <input id="isRecurrent" type="checkbox" @change="() => {isRecurrent = !isRecurrent}"><label for="isRecurrent">Opakující se událost</label>
  <div v-if="isRecurrent">
    <h3>Opakování</h3>
    <dl>
      <dt><label for="rruleFreq">Opakovat</label></dt>
      <dd>
        <input type="number" name="" id=""  />
      </dd>
      <dd>
        <select id="rruleFreq" v-model="rruleOptions.freq">
          <option :value="RRule.DAILY">Denně</option>
          <option :value="RRule.WEEKLY">Týdně</option>
          <option :value="RRule.MONTHLY">Měsíčně</option>
          <option :value="RRule.YEARLY">Ročně</option>
        </select>
      </dd>
      <dt v-if="rruleOptions.freq == RRule.WEEKLY"><label for="rruleByDay">Dny</label></dt>
      <dd v-if="rruleOptions.freq == RRule.WEEKLY">
        <input type="checkbox" id="rruleByWeekDayMo" :value="0" /><label for="rruleByWeekDayMo">Po</label>
        <input type="checkbox" id="rruleByWeekDayTu" :value="1" /><label for="rruleByWeekDayTu">Út</label>
        <input type="checkbox" id="rruleByWeekDayWe" :value="2" /><label for="rruleByWeekDayWe">St</label>
        <input type="checkbox" id="rruleByWeekDayTh" :value="3" /><label for="rruleByWeekDayTh">Čt</label>
        <input type="checkbox" id="rruleByWeekDayFr" :value="4" /><label for="rruleByWeekDayFr">Pá</label>
        <input type="checkbox" id="rruleByWeekDaySa" :value="5" /><label for="rruleByWeekDaySa">So</label>
        <input type="checkbox" id="rruleByWeekDaySu" :value="6" /><label for="rruleByWeekDaySu">Ne</label>
      </dd>

      <dt><label for="rruleByDay">Vždy</label></dt>
      <dd>
        <input type="number" id="rruleByMonthDay" min="1" max="31" />
        <label for="rruleByMonthDay"> den v měsíci</label>
      </dd>
    </dl>
    <h3>Končí</h3>
    <dl>
      <dt><label for="rruleUntil">Datum</label></dt>
      <dd>
        <datepicker id="rruleUntil" class="date"
          v-model="rruleOptions.until" :locale="cs" inputFormat="dd. MM. yyyy"
        />
      </dd>
      <dt><label for="rruleCount">Po</label></dt>
      <dd><input id="rruleCount" type="number" v-model="rruleOptions.count"/><label for="rruleCount"> opakováních</label></dd>
    </dl>
    <p>{{ rruleOptions.byweekday }}</p>
    <p>{{ rruleOptions.freq }}</p>
    <button @click="logRRule">rrule</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from 'vue'
import { EventBase } from '@/composables/EventBase'
import axios from 'axios'
import { Place } from '@/composables/Place'
import TimePicker from '@/components/inputs/TimePicker.vue' // @ is an alias to /src
import Datepicker from 'vue3-datepicker'
import { cs } from 'date-fns/locale'
import { Options as RRuleOptions, RRule } from 'rrule'
import { EventCategory } from '@/composables/EventCategory'

export default defineComponent({
  name: 'EventsDetail',
  components: {
    Datepicker, TimePicker
  },
  setup () {
    const event = reactive<EventBase>({
      name: '',
      startTime: new Date(),
      endTime: new Date(),
      place: {
        name: '',
        code: ''
      }
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
      byweekday: (new Date().getDay() + 6) % 7,
      bynweekday: null,
      byhour: null,
      byminute: null,
      bysecond: null,
      byeaster: null
    })
    const places = ref<Array<Place>>([])
    axios.get<Array<Place>>('/api/places')
      .then(function (response) {
        // handle success
        console.log(response.data)
        places.value = response.data
      })
      .catch(function (error) {
        // handle error
        console.log(error)
      })
      .then(function () {
        // always executed
      })

    const categories = ref<Array<EventCategory>>([])
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
      event.rrule = new RRule(rruleOptions).toString()
    })

    return {
      categories,
      endTime,
      event,
      eventName,
      isRecurrent,
      places,
      rruleOptions,
      RRule,
      startTime,
      cs,
      logRRule,
      saveEvent
    }
  }
})
</script>

<style scoped>
  .v3dp__datepicker {
    display: inline-block;
  }
</style>
