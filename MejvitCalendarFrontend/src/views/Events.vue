<template>
  <side-bar>
    <ul>
      <li v-for="(place, i) in places" :key="i" class="py-2">
        <router-link :to="'/places/'+place.code" class="text-white text-decoration-none">{{ place.name }}</router-link>
      </li>
    </ul>
  </side-bar>
  <main-area>
    <h1>Přidat událost</h1>
    <datepicker class="date" style="display: inline-block;"
      v-model="startTime" :locale="cs" inputFormat="dd. MM. yyyy"
    />
    <time-picker  v-model="startTime" />
    <span> — </span>
    <time-picker  v-model="endTime" />
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
  </main-area>
</template>

<script lang="ts">
import { defineComponent, reactive, ref } from 'vue'
import axios from 'axios'
import { Place } from '@/composables/Place'
import SideBar from '@/components/SideBar.vue' // @ is an alias to /src
import MainArea from '@/components/MainArea.vue' // @ is an alias to /src
import TimePicker from '@/components/inputs/TimePicker.vue' // @ is an alias to /src
import Datepicker from 'vue3-datepicker'
import { cs } from 'date-fns/locale'
import { Options as RRuleOptions, RRule } from 'rrule'

export default defineComponent({
  name: 'Events',
  components: {
    Datepicker, MainArea, SideBar, TimePicker
  },
  setup () {
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
    return {
      endTime,
      eventName,
      isRecurrent,
      places,
      rruleOptions,
      RRule,
      startTime,
      cs,
      logRRule
    }
  }
})
</script>

<style scoped>
  .v3dp__datepicker {
    display: inline-block;
  }
</style>
