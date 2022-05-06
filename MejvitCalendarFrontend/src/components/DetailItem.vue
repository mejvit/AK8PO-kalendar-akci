<template>
  <div class="detail-item">
    <div class="description">
      <router-link :to="to" class="link">
        <strong>{{ name }}</strong>
      </router-link>
    </div>
    <button class="delete" @click="$emit('deleteClicked', id)">
      <i class="bi bi-trash3-fill"></i>
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'DetailItem',
  props: {
    name: String,
    id: Number,
    to: String
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
  .detail-item {
    display: flex;
    align-items: stretch;
    color: #333;
  }

  .detail-item:first-of-type {
    margin-top: 1em;
    border-top: 3px solid #eaeaea;
  }

  .detail-item:last-of-type {
    margin-bottom: 1em;
    border-bottom: 3px solid #eaeaea;
  }

  .detail-item:nth-child(2n) {
    background: #eaeaea;
  }

  .description {
    flex-grow: 8;
  }

  .description .link {
    display: inline-block;
    width: 100%;
    color: #333;
    text-decoration: none;
    padding: 0.5em 0 0.5em 0.5em;
  }
  .description .link:hover {
    background: #cacaca;
  }
  .save, .delete {
    background: transparent;
    border: 0;
    padding: 0 0.5em;
    margin: 0.5em;
  }

  .save i, .delete i {
    color: #333;
  }

  .save:hover, .delete:hover {
    cursor: pointer;
    background: #eaeaea;
  }

  .detail-item:nth-child(2n) .save:hover, .detail-item:nth-child(2n) .delete:hover {
    background: #fff;
  }
</style>
