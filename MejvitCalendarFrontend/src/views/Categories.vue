<template>
  <main-area>
    <h1>Správa kategorií</h1>
    <button @click="showCreateDialog" class="button-add">Vytvořit kategorii</button>
    <editable-item
      v-for="(category, i) in categories" :key="i"
      :id="category.id"
      :name="category.name"
      @delete-clicked="(id) => { showDeleteDialog(id) }"
    />
    <edit-modal :visible="createDialogOpened" @update:visible="hideCreateDialog" title="Vytvořit kategorii">
      <div>
        <label for="placeName">Název</label>
        <input type="text" v-model="newCategory.name" id="placeName">
      </div>
      <div>
        <label for="placeCode">Kód</label>
        <input type="text" v-model="newCategory.code" id="placeCode">
      </div>
      <div class="modal-buttons">
        <button @click="createCategory">Ok</button>
        <button @click="hideCreateDialog">Zrušit</button>
      </div>
    </edit-modal>
    <delete-modal
      :id="idToDelete"
      :visible="deleteDialogVisible"
      @update:visible="hideDeleteDialog"
      @delete-confirm="(id) => { deleteEntity(id, '/api/categories/') }"
    />
  </main-area>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from 'vue'
import { EventCategory } from '../composables/EventCategory'
import { convertNameToCode } from '../composables/Parser'
import MainArea from '../components/MainArea.vue'
import DeleteModal from '../components/DeleteModal.vue'
import EditModal from '../components/EditModal.vue'
import EditableItem from '../components/EditableItem.vue'
import axios from 'axios'
import { deleteEntity } from '../composables/api/apiCommunication'

export default defineComponent({
  name: 'Events',
  components: { DeleteModal, EditableItem, EditModal, MainArea },
  setup () {
    const newCategory = reactive<EventCategory>({ name: '', code: '' })
    const categories = ref<Array<EventCategory>>([])
    const createDialogOpened = ref<boolean>(false)
    const showCreateDialog = () => { createDialogOpened.value = true }
    const hideCreateDialog = () => { createDialogOpened.value = false }

    axios.get<Array<EventCategory>>('/api/categories')
      .then(function (response) {
        // handle success
        console.log(response.data)
        categories.value = response.data
      })
      .catch(function (error) {
        // handle error
        console.log(error)
      })
      .then(function () {
        // always executed
      })

    watch(() => newCategory.name, (newName) => {
      newCategory.code = convertNameToCode(newName)
    })

    const createCategory = function () {
      axios.post('/api/categories', newCategory)
        .then(response => console.log(response))
    }

    const idToDelete = ref<number | null>(null)
    const deleteDialogVisible = ref<boolean>(false)
    const showDeleteDialog = (id: number) => {
      deleteDialogVisible.value = true
      idToDelete.value = id
    }
    const hideDeleteDialog = () => { deleteDialogVisible.value = false }

    return {
      categories,
      createCategory,
      createDialogOpened,
      deleteEntity,
      deleteDialogVisible,
      hideCreateDialog,
      hideDeleteDialog,
      idToDelete,
      newCategory,
      showCreateDialog,
      showDeleteDialog
    }
  }
})
</script>
