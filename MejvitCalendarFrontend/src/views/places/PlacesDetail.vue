<template>
  <h1>{{ placeName }}</h1>
    <div class="input-group">
      <div class="input-row">
        <div class="input-field">
          <label for="placeName">Název</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="place.name" id="placeName">
        </div>
      </div>
      <div class="input-row">
        <div class="input-field">
          <label for="placeCode">Kód</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="place.code" id="categoryCode" v-if="editCodeActive">
          <span v-else>{{ place.code }}</span>
          <button @click="() => { editCodeActive = false }" v-if="editCodeActive" class="edit-toggle">
            <i class="bi bi-x-square-fill"></i>
          </button>
          <button @click="() => { editCodeActive = true }" class="edit-toggle" v-else-if="!editCodeActive">
            <i class="bi bi-pencil-fill"></i>
          </button>
        </div>
      </div>
    </div>
    <div class="input-buttons">
      <button class="button-back" @click="$router.push('/places')">Zpět</button>
      <button class="button-save" @click="updatePlace">Uložit</button>
    </div>
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
    const place = ref<Place>({ id: undefined, name: '', code: '' })
    const editCodeActive = ref<boolean>(false)
    onMounted(() => {
      fetchPlace()
    })
    const placeName = ref<string>('')
    const fetchPlace = () => {
      axios.get<Place>('/api/places/code=' + route.params.code)
        .then(function (response) {
          // handle success
          place.value = response.data
          placeName.value = place.value.name
        })
        .catch(function (error) {
          // handle error
          console.log(error)
        })
        .then(function () {
          // always executed
        })
    }

    const updatePlace = () => {
      axios.put('/api/places/' + place.value.id, place.value)
        .then(response => {
          console.log(response)
          fetchPlace()
        })
    }

    watch(
      () => route.params.code,
      code => {
        if (code === undefined) { return }
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
      editCodeActive,
      place,
      placeName,
      updatePlace
    }
  }
})
</script>
