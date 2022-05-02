<template>
  <main-area class="no-sidebar">
  <div >
    <div class="row">
      <div class="col-sm-9">
        <div class="container">
          <div class="row">
            <div class="col">
              <router-link to="/places">
                <div class="card" style="width: 18rem;">
                  <img src="" class="card-img-top" alt="Správa míst" />
                  <div class="card-body">
                    <p class="card-text">Správa míst</p>
                  </div>
                </div>
              </router-link>
              </div>
            <div class="col">
              <router-link to="/places">
                <div class="card" style="width: 18rem;">
                  <img src="" class="card-img-top" alt="...">
                  <div class="card-body">
                    <p class="card-text">Správa kategorií</p>
                  </div>
                </div>
              </router-link>
            </div>
            <div class="col"></div>
          </div>
        </div>
        <h1>Přidat událost</h1>
        <datepicker v-model="startTime" :locale="cs" />
        <time-picker  v-model="startTime" />
        <button @click="showMeDateValue">date</button>
        <input type="text" :value="eventName">
      </div>
    </div>
  </div>

  </main-area>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { Place } from '../composables/Place'
import MainArea from '../components/MainArea.vue' // @ is an alias to /src
import TimePicker from '../components/TimePicker.vue' // @ is an alias to /src
import Datepicker from 'vue3-datepicker'
import { cs } from 'date-fns/locale'

export default defineComponent({
  name: 'SingleEvents',
  components: {
    Datepicker, MainArea, TimePicker
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
