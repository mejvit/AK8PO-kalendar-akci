<template>
    <h1>Správa míst</h1>
    <button @click="showCreateDialog" class="button-add">Vytvořit místo</button>
    <input v-model="placesFilter" />
    <ul>
      <li v-for="(place, i) in filteredPlaces" :key="i">
        <router-link :to="'/places/'+place.code">{{ place.name }}</router-link>
      </li>
    </ul>
    <edit-modal :visible="createDialogOpened" @update:visible="hideCreateDialog" title="Vytvořit místo">
      <div>
        <label for="placeName">Název</label>
        <input type="text" v-model="newPlace.name" id="placeName">
      </div>
      <div>
        <label for="placeCode">Kód</label>
        <input type="text" v-model="newPlace.code" id="placeCode">
      </div>
      <div class="modal-buttons">
        <button @click="createPlace">Ok</button>
        <button @click="hideCreateDialog">Zrušit</button>
      </div>
    </edit-modal>
</template>

<script lang="ts">
import { computed, defineComponent, reactive, ref, watch } from 'vue'
import axios from 'axios'
import { Place } from '../../composables/Place'
import { convertNameToCode } from '../../composables/Parser'
import EditModal from '../../components/EditModal.vue'

export default defineComponent({
  name: 'PlacesDetail',
  components: { EditModal },

  setup () {
    const newPlace = reactive<Place>({ name: '', code: '' })
    const placesFilter = ref<string>('')
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

    const sanitizedFilter = computed(() => placesFilter.value.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase())
    const filteredPlaces = computed(() => {
      if (places.value.length > 0) {
        return places.value.filter(place => place.name.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value))
      }
      return places.value
    })

    const createDialogOpened = ref<boolean>(false)
    const showCreateDialog = () => { createDialogOpened.value = true }
    const hideCreateDialog = () => { createDialogOpened.value = false }
    const createPlace = function () {
      axios.post('/api/places', newPlace)
        .then(response => console.log(response))
    }

    watch(() => newPlace.name, (newName) => {
      newPlace.code = convertNameToCode(newName)
    })

    return {
      createPlace,
      createDialogOpened,
      filteredPlaces,
      hideCreateDialog,
      newPlace,
      showCreateDialog,
      placesFilter
    }
  }
})
</script>
