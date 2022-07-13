<template>
  <h1>Správa událostí</h1>
  <div class="overview-header">
    <router-link to="/events/create" class="button-add">Vytvořit událost</router-link>
    <filter-input v-model="placesFilter"/>
  </div>
  <detail-item
    v-for="(event, i) in filteredEvents" :key="i"
    :id="event.id"
    :to="'/events/'+event.eventBase.place.code+'/'+event.id"
    @delete-click="(id) => { visibility.deleteEventModal = true; idToDelete = id }"
  >{{getFormattedTime(event.startDateTime)}}<br /> {{event.eventBase.place.name}}, {{ event.eventBase.name }}</detail-item>

  <!-- <delete-modal
    :id="idToDelete"
    :visible="visibility.deleteModal"
    @dismiss-click="() => { visibility.deleteModal = false }"
    @confirm-click="(id) => { deleteEntity(id, '/api/event-instances/').then(() => {visibility.deleteModal = false; fetchPlaces()}) }"
  /> -->

  <div v-if="visibility.deleteEventModal">
    <button @click="deleteSingleEvent">Smazat pouze tuto událost</button>
    <button @click="deleteAllEvents">Smazat všechny události</button>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, reactive, ref, watch } from 'vue'
import axios from 'axios'
import { deleteEntity } from '@/composables/api/apiCommunication'
import DeleteModal from '@/components/DeleteModal.vue'
import DetailItem from '@/components/DetailItem.vue'
import FilterInput from '@/components/inputs/FilterInput.vue'
import { EventInstance } from '@/composables/EventInstance'

export default defineComponent({
  name: 'PlacesDetail',
  components: { DetailItem, FilterInput },
  props: {
    placeCode: {
      type: String,
      required: false
    }
  },
  setup (props) {
    const visibility = reactive({
      deleteModal: false,
      deleteEventModal: false
    })
    const placesFilter = ref<string>('')
    const events = ref<Array<EventInstance>>([])
    const idToDelete = ref<number>()

    onMounted(() => {
      fetchEvents(props.placeCode)
      console.log(props.placeCode)
    })

    watch(
      () => props.placeCode,
      code => {
        fetchEvents(code as string)
      }
    )
    const fetchEvents = function (placeCode: string | undefined) {
      axios.get<Array<EventInstance>>('/api/event-instances' + (placeCode !== undefined ? '/' + placeCode : ''))
        .then(function (response) {
          // handle success
          console.log(response.data)
          events.value = response.data
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
    const filteredEvents = computed(() => {
      if (events.value.length > 0) {
        return events.value.filter(event =>
          event.eventBase?.place?.name?.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value) ||
          event.eventBase?.name?.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().includes(sanitizedFilter.value)
        )
      }
      return events.value
    })

    const createDialogOpened = ref<boolean>(false)

    const deleteAllEvents = function () {
      const baseEventId = events.value.find(evt => evt.id)?.eventBase?.id

      deleteEntity(baseEventId as number, '/api/events/').then(() => fetchEvents)
    }

    const deleteSingleEvent = function () {
      deleteEntity(idToDelete.value as number, '/api/event-instances/').then(() => fetchEvents)
    }

    const getFormattedTime = function (dateTime: Date) {
      const dt = new Date(dateTime)
      return dt.getDate() + '.' + (dt.getMonth() + 1) + '.' + dt.getFullYear() + ' ' + dt.getHours() + ':' + (dt.getMinutes() < 10 ? '0' : '') + dt.getMinutes()
    }
    return {
      createDialogOpened,
      deleteAllEvents,
      deleteEntity,
      deleteSingleEvent,
      fetchEvents,
      filteredEvents,
      getFormattedTime,
      idToDelete,
      placesFilter,
      visibility
    }
  }
})
</script>
