<template>
  <h1>{{ place.name }}</h1>
    <div>{{ $route.params.code }}</div>
</template>

<script lang="ts">
import { useRoute } from 'vue-router'
import { defineComponent, onMounted, ref, watch } from 'vue'
import axios from 'axios'
import { Place } from '../../composables/Place'

export default defineComponent({
  name: 'PlacesDetail',

  setup () {
    const route = useRoute()
    const place = ref<Place>({ name: '', code: '' })
    onMounted(() => {
      axios.get<Place>('/api/places/code=' + route.params.code)
        .then(function (response) {
          // handle success
          place.value = response.data
        })
        .catch(function (error) {
          // handle error
          console.log(error)
        })
        .then(function () {
          // always executed
        })
    })
    watch(
      () => route.params.code,
      code => {
        console.log('routecha')
        axios.get<Place>('/api/places/code=' + code)
          .then(function (response) {
            // handle success
            place.value = response.data
          })
          .catch(function (error) {
            // handle error
            console.log(error)
          })
          .then(function () {
            // always executed
          })
      }
    )
    return {
      place
    }
  }
})
</script>
