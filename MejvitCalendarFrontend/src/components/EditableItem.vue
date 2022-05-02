<template>
  <div class="editable-item">
    <div class="description">
      <p v-if="editMode">{{ name }}</p>
      <strong v-else>{{ name }}</strong>
    </div>
    <button class="edit" @click="switchMode">
      <i class="bi bi-pencil-fill"></i>
    </button>
    <button class="delete" @click="$emit('deleteClicked', id)">
      <i class="bi bi-trash3-fill"></i>
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'EditableItem',
  props: {
    name: String,
    id: Number
  },
  emits: ['deleteClicked'],
  setup () {
    const editMode = ref<boolean>(false)
    const switchMode = () => { editMode.value = !editMode.value }
    const test = function (id: number) { console.log(id) }
    return {
      editMode,
      switchMode,
      test
    }
  }
})
</script>

<style scoped>
  .editable-item {
    display: flex;
    align-items: stretch;
    padding: 0.5em;
  }

  .editable-item:nth-child(2n) {
    background: #eaeaea;
  }

  .description {
    flex-grow: 8;
  }

  .edit, .delete {
    background: transparent;
    border: 0;
    padding: 0 0.5em;
  }

  .edit:hover, .delete:hover {
    cursor: pointer;
    background: #eaeaea;
  }

  .editable-item:nth-child(2n) .edit:hover, .editable-item:nth-child(2n) .delete:hover {
    background: #fff;
  }
</style>
