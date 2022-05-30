<template>
  <main-area>
    <h1>Správa kategorií</h1>
    <div class="overview-header">
      <button @click="showEditDialog()" class="button-add">Vytvořit kategorii</button>
    </div>
    <editable-item
      v-for="(category, i) in categories" :key="i"
      :id="category.id"
      :name="category.name"
      @delete-clicked="(id) => { deleteDialog.show(id) }"
      @edit-clicked="(id) => { showEditDialog(id) }"
    />
    <edit-modal :title="createModeActive ? 'Vytvořit kategorii' : 'Upravit kategorii'"
      :visible="editDialogVisible"
      @dismiss-click="() => { editDialogVisible = false }"
      v-on="createModeActive ? {confirmClick: () => { createCategory() }} : {confirmClick: () => { editCategory() }}"
    >
      <div class="input-group">
        <div class="input-row">
          <div class="input-field">
            <label for="categoryName">Název</label>
          </div>
          <div class="input-field">
            <input type="text" v-model="activeCategory.name" id="categoryName">
          </div>
        </div>
        <div class="input-row">
          <div class="input-field">
            <label for="categoryCode">Kód</label>
          </div>
          <div class="input-field">
            <input type="text" v-model="activeCategory.code" id="categoryCode" v-if="editCodeActive">
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
      :id="deleteDialog.id"
      :visible="deleteDialog.visible"
      :responseMessage="deleteDialog.responseMessage"
      @dismiss-click="() => { deleteDialog.hide() }"
      @confirm-click="(id) => { deleteCategory(id) }"
    />
  </main-area>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref, watch } from 'vue'
import { EventCategory } from '../composables/EventCategory'
import { convertNameToCode } from '../composables/Parser'
import MainArea from '../components/MainArea.vue'
import DeleteModal from '../components/DeleteModal.vue'
import EditModal from '../components/EditModal.vue'
import EditableItem from '../components/EditableItem.vue'
import axios from 'axios'

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

    onMounted(() => {
      fetchCategories()
    })
    watch(() => activeCategory.name, (newName) => {
      if (createModeActive.value) {
        activeCategory.code = convertNameToCode(newName)
      }
    })

    const fetchCategories = function () {
      axios.get<Array<EventCategory>>('/api/categories')
        .then(function (response) {
          categories.value = response.data
        })
        .catch(function (error) {
          console.error(error)
        })
    }

    const createCategory = function () {
      axios.post('/api/categories', activeCategory)
        .then(function (response) {
          console.log(response)
          fetchCategories()
          editDialogVisible.value = false
        })
        .catch(error => { console.error(error) })
    }

    const deleteCategory = function (id: number): void {
      axios.delete('/api/categories/' + id)
        .then(function (response) {
          if (response.status === 204) {
            deleteDialog.hide()
            fetchCategories()
          }
        })
        .catch(function (error) {
          deleteDialog.responseMessage = 'Při mazání kategorie nastala chyba'
          console.error(error)
        })
    }

    const deleteDialog = reactive<{id?: number, visible: boolean, responseMessage: string, show:(id: number) => void, hide:() => void }>(
      {
        id: undefined,
        visible: false,
        responseMessage: '',
        show: function (id: number) {
          this.visible = true
          this.id = id
        },
        hide: function () {
          this.visible = false
          this.responseMessage = ''
          this.id = undefined
        }
      }
    )
    const editCategory = () => {
      axios.put('/api/categories/' + activeCategory.id, activeCategory)
        .then(response => {
          console.log(response)
          editDialogVisible.value = false
          fetchCategories()
        })
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
      deleteDialog,
      deleteCategory,
      editCategory,
      editCodeActive,
      editDialogVisible,
      showEditDialog
    }
  }
})
</script>
