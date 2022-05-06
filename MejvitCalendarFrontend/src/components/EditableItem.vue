<template>
  <div class="editable-item">
    <div class="description">
      <strong>{{ name }}</strong>
    </div>
    <button class="edit" @click="$emit('editClicked', id)">
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
  emits: ['deleteClicked', 'editClicked'],
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
    color: #333;
  }

  .editable-item:first-of-type {
    margin-top: 1em;
    border-top: 3px solid #eaeaea;
  }

  .editable-item:last-of-type {
    margin-bottom: 1em;
    border-bottom: 3px solid #eaeaea;
  }

  .editable-item:nth-child(2n) {
    background: #eaeaea;
  }

  .description {
    flex-grow: 8;
  }

  .edit, .save, .delete {
    background: transparent;
    border: 0;
    padding: 0 0.5em;
  }

  .edit i, .save i, .delete i {
    color: #333;
  }

  .edit:hover, .save:hover, .delete:hover {
    cursor: pointer;
    background: #eaeaea;
  }

  .editable-item:nth-child(2n) .edit:hover, .editable-item:nth-child(2n) .save:hover, .editable-item:nth-child(2n) .delete:hover {
    background: #fff;
  }
</style>
