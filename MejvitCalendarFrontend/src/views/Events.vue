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
    <datepicker v-model="startTime" :locale="cs" />
    <time-picker  v-model="startTime" />
    <button @click="showMeDateValue">date</button>
    <input type="text" :value="eventName">
  </main-area>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { Place } from '../composables/Place'
import SideBar from '../components/SideBar.vue' // @ is an alias to /src
import MainArea from '../components/MainArea.vue' // @ is an alias to /src
import TimePicker from '../components/TimePicker.vue' // @ is an alias to /src
import Datepicker from 'vue3-datepicker'
import { cs } from 'date-fns/locale'

export default defineComponent({
  name: 'Events',
  components: {
    Datepicker, MainArea, SideBar, TimePicker
  },
  setup () {
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

    const startTime = ref<Date>(new Date())
    const showMeDateValue = function () {
      console.log(startTime.value)
    }

    const eventName = ref<string>('test')

    return {
      eventName,
      places,
      showMeDateValue,
      startTime,
      cs
    }
  }
})
</script>
