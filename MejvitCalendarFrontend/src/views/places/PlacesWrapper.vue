<template>
  <side-bar>
    <div>
      <input class="search-input" type="text" v-model="placesFilter">
    </div>
    <ul>
      <li v-for="(place, i) in filteredPlaces" :key="i" class="py-2">
        <router-link :to="'/places/'+place.code">{{ place.name }}</router-link>
      </li>
    </ul>
  </side-bar>
  <main-area>
    <router-view />
  </main-area>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref } from 'vue'
import axios from 'axios'
import { Place } from '@/composables/Place'
import SideBar from '@/components/SideBar.vue'
import MainArea from '@/components/MainArea.vue'

export default defineComponent({
  name: 'Places',
  components: {
    MainArea, SideBar
  },
  setup () {
    const placesFilter = ref<string>('')
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
    const sanitizedFilter = computed(() => placesFilter.value.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase())
    const filteredPlaces = computed(() => {
      if (places.value.length > 0) {
        return places.value.filter(place => place.name?.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value))
      }
      return places.value
    })
    return { filteredPlaces, placesFilter }
  }
})
</script>
