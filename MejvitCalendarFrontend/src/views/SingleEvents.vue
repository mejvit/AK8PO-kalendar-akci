<template>
  <main-area class="no-sidebar">
    <ul>
      <li v-for="event in singleEvents" :key="event.id" class="event">
        <span class="time">{{ getFormattedTime(event.startDateTime) }}</span>
        <span class="place">{{ event.eventBase.place.name }}</span>
        <span class="name">{{ event.eventBase.name }}</span>
      </li>
    </ul>
  </main-area>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import axios from 'axios'
import { Place } from '@/composables/Place'
import { EventInstance } from '@/composables/EventInstance'
import MainArea from '@/components/MainArea.vue' // @ is an alias to /src
import { cs } from 'date-fns/locale'

export default defineComponent({
  name: 'SingleEvents',
  components: {
    MainArea
  },
  setup () {
    const places = ref<Array<Place>>([])
    onMounted(() => {
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
    })

    const startTime = ref<Date>(new Date())
    const showMeDateValue = function () {
      console.log(startTime.value)
    }

    const eventName = ref<string>('test')

    const singleEvents = ref<Array<EventInstance>>([])
    axios.get<Array<EventInstance>>('/api/event-instances')
      .then(function (response) {
        singleEvents.value = response.data
      })
      .catch(function (error) {
        console.error(error)
      })

    const getFormattedTime = function (dateTime: Date) {
      const dt = new Date(dateTime)
      return dt.getDate() + '.' + (dt.getMonth() + 1) + '.' + dt.getFullYear() + ' ' + dt.getHours() + ':' + (dt.getMinutes() < 10 ? '0' : '') + dt.getMinutes()
    }

    return {
      eventName,
      places,
      showMeDateValue,
      startTime,
      cs,
      singleEvents,
      getFormattedTime
    }
  }
})
</script>

<style lang="scss" scoped>
.event {
  list-style-type: none;
  .time {
    font-weight: 700;
  }
}
</style>
