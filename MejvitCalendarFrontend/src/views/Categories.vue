<template>
  <main-area>
    <h1>Správa kategorií</h1>
    <button @click="showEditDialog()" class="button-add">Vytvořit kategorii</button>
    <editable-item
      v-for="(category, i) in categories" :key="i"
      :id="category.id"
      :name="category.name"
      @delete-clicked="(id) => { showDeleteDialog(id) }"
      @edit-clicked="(id) => { showEditDialog(id) }"
    />
    <edit-modal title="Vytvořit kategorii"
      :visible="editDialogVisible"
      @dismiss-clicked="() => { editDialogVisible = false }"
      v-on="createModeActive ? {confirmClicked: () => { createCategory() }} : {confirmClicked: () => { editCategory() }}"
    >
    <div class="input-group">
      <div class="input-row">
        <div class="input-field">
          <label for="placeName">Název</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="activeCategory.name" id="placeName">
        </div>
      </div>
      <div class="input-row">
        <div class="input-field">
          <label for="placeCode">Kód</label>
        </div>
        <div class="input-field">
          <input type="text" v-model="activeCategory.code" id="placeCode" v-if="editCodeActive">
          <span v-else>{{ activeCategory.code }}</span>
          <button @click="() => { editCodeActive = false }" v-if="editCodeActive && !createModeActive" class="edit-toggle">
            <i class="bi bi-x-square-fill"></i>
          </button>
          <button @click="() => { editCodeActive = true }" class="edit-toggle" v-else-if="!editCodeActive">
            <i class="bi bi-pencil-fill"></i>
          </button>
        </div>
      </div>
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
    const categories = ref<Array<EventCategory>>([])
    const activeCategory = reactive<EventCategory>({
      id: undefined,
      name: '',
      code: ''
    })

    const editDialogVisible = ref<boolean>(false)
    const createModeActive = ref<boolean>(false)
    const editCodeActive = ref<boolean>(false)

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

    watch(() => activeCategory.name, (newName) => {
      if (createModeActive.value) {
        activeCategory.code = convertNameToCode(newName)
      }
    })

    const createCategory = function () {
      axios.post('/api/categories', activeCategory)
        .then(response => console.log(response))
        .then(hideDeleteDialog)
    }

    const idToDelete = ref<number | null>(null)
    const deleteDialogVisible = ref<boolean>(false)
    const showDeleteDialog = (id: number) => {
      deleteDialogVisible.value = true
      idToDelete.value = id
    }
    const hideDeleteDialog = () => { deleteDialogVisible.value = false }
    const editCategory = () => {
      axios.put('/api/categories/' + activeCategory.id, activeCategory)
        .then(response => console.log(response))
        .then(hideDeleteDialog)
    }

    const showEditDialog = (id?: number) => {
      if (id !== undefined) {
        editCodeActive.value = false
        createModeActive.value = false
        const filteredCategory: EventCategory = categories.value.filter((cat: EventCategory) => cat.id === id)[0]
        activeCategory.id = filteredCategory.id
        activeCategory.code = filteredCategory.code
        activeCategory.name = filteredCategory.name
      } else {
        activeCategory.id = undefined
        activeCategory.code = ''
        activeCategory.name = ''
        editCodeActive.value = true
        createModeActive.value = true
      }
      editDialogVisible.value = true
    }
    return {
      activeCategory,
      categories,
      createCategory,
      createModeActive,
      deleteEntity,
      deleteDialogVisible,
      editCategory,
      editCodeActive,
      editDialogVisible,
      hideDeleteDialog,
      idToDelete,
      showEditDialog,
      showDeleteDialog
    }
  }
})
</script>

<style scoped>
  .edit-toggle {
    border: 0;
    background: transparent;
    margin-left: 0.5em;
  }

  .edit-toggle:hover {
    cursor: pointer;
    background: #eaeaea;
  }
</style>
