<template>
  <h1>Správa událostí</h1>
  <div class="overview-header">
    <router-link to="/events/create" class="button-add">Vytvořit událost</router-link>
    <filter-input v-model="placesFilter"/>
  </div>
  <detail-item
    v-for="(place, i) in filteredPlaces" :key="i"
    :id="place.id"
    :name="place.name" :to="'/events/'+place.code"
    @delete-click="(id) => { visibility.deleteModal = true; idToDelete = id }"
  />

  <delete-modal
    :id="idToDelete"
    :visible="visibility.deleteModal"
    @dismiss-click="() => { visibility.deleteModal = false }"
    @confirm-click="(id) => { deleteEntity(id, '/api/event-instances/').then(() => {visibility.deleteModal = false; fetchPlaces()}) }"
  />
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, reactive, ref, watch } from 'vue'
import axios from 'axios'
import { Place } from '../../composables/Place'
import { deleteEntity } from '../../composables/api/apiCommunication'
import { convertNameToCode } from '../../composables/Parser'
import DeleteModal from '@/components/DeleteModal.vue'
import DetailItem from '@/components/DetailItem.vue'
import FilterInput from '@/components/inputs/FilterInput.vue'

export default defineComponent({
  name: 'PlacesDetail',
  components: { DeleteModal, DetailItem, FilterInput },

  setup () {
    const visibility = reactive({
      deleteModal: false
    })
    const newPlace = reactive<Place>({ id: undefined, name: '', code: '' })
    const placesFilter = ref<string>('')
    const places = ref<Array<Place>>([])
    const idToDelete = ref<number>()

    onMounted(() => {
      fetchPlaces()
    })

    const fetchPlaces = function () {
      axios.get<Array<Place>>('/api/event-instances')
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
    }

    const sanitizedFilter = computed(() => placesFilter.value.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase())
    const filteredPlaces = computed(() => {
      if (places.value.length > 0) {
        return places.value.filter(place => place.name.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value))
      }
      return places.value
    })

    const createDialogOpened = ref<boolean>(false)
    const createPlace = function () {
      axios.post('/api/event-instances', newPlace)
        .then(response => {
          console.log(response)
          resetPlace()
          fetchPlaces()
          createDialogOpened.value = false
        })
    }

    const resetPlace = function () {
      newPlace.id = undefined
      newPlace.code = ''
      newPlace.name = ''
    }

    watch(() => newPlace.name, (newName) => {
      newPlace.code = convertNameToCode(newName)
    })

    return {
      createPlace,
      createDialogOpened,
      deleteEntity,
      fetchPlaces,
      filteredPlaces,
      idToDelete,
      newPlace,
      placesFilter,
      visibility
    }
  }
})
</script>
