<template>
  <h1>Správa organizací</h1>
  <div class="overview-header">
    <button @click="showCreateDialog" class="button-add">Vytvořit organizaci</button>
    <filter-input v-model="placesFilter"/>
  </div>
  <detail-item
    v-for="(place, i) in filteredPlaces" :key="i"
    :id="place.id"
    :to="'/places/'+place.code"
    @delete-click="(id) => { visibility.deleteModal = true; idToDelete = id }"
  >{{ place.name }}</detail-item>
  <edit-modal
    :visible="createDialogOpened" @update:visible="hideCreateDialog" title="Vytvořit organizaci"
    @dismiss-click="() => { createDialogOpened = false }"
    @confirmClick="createPlace"
  >
    <div class="input-group">
      <div class="input-row">
        <div class="input-field">
          <label for="categoryName">Název</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="newPlace.name" id="placeName">
        </div>
      </div>
      <div class="input-row">
        <div class="input-field">
          <label for="categoryCode">Kód</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="newPlace.code" id="placeCode">
        </div>
      </div>
    </div>
  </edit-modal>
  <delete-modal
    :id="idToDelete"
    :visible="visibility.deleteModal"
    @dismiss-click="() => { visibility.deleteModal = false }"
    @confirm-click="(id) => { deleteEntity(id, '/api/places/').then(() => {visibility.deleteModal = false; fetchPlaces()}) }"
  />
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, reactive, ref, watch } from 'vue'
import axios from 'axios'
import { Place } from '@/composables/Place'
import { deleteEntity } from '@/composables/api/apiCommunication'
import { convertNameToCode } from '@/composables/Parser'
import DeleteModal from '@/components/DeleteModal.vue'
import EditModal from '@/components/EditModal.vue'
import DetailItem from '@/components/DetailItem.vue'
import FilterInput from '@/components/inputs/FilterInput.vue'

export default defineComponent({
  name: 'PlacesDetail',
  components: { DeleteModal, DetailItem, EditModal, FilterInput },

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
    }

    const sanitizedFilter = computed(() => placesFilter.value.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase())
    const filteredPlaces = computed(() => {
      if (places.value.length > 0) {
        return places.value.filter(place => place.name?.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value))
      }
      return places.value
    })

    const createDialogOpened = ref<boolean>(false)
    const showCreateDialog = () => { createDialogOpened.value = true }
    const hideCreateDialog = () => { createDialogOpened.value = false }
    const createPlace = function () {
      axios.post('/api/places', newPlace)
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
      newPlace.code = convertNameToCode(newName as string)
    })

    return {
      createPlace,
      createDialogOpened,
      deleteEntity,
      fetchPlaces,
      filteredPlaces,
      hideCreateDialog,
      idToDelete,
      newPlace,
      showCreateDialog,
      placesFilter,
      visibility
    }
  }
})
</script>
