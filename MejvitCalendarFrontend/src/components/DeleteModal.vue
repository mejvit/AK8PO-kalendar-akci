<template>
  <div class="modal-overlay" v-if="visible">
    <div class="modal-content">
      <header>
        <h3>Smazat záznam?</h3>
        <button class="close-button" @click="$emit('update:visible', $event.target.value)">&times;</button>
      </header>
      <div class="modal-body">
        <button @click="deleteConfirm">Smazat</button>
        <button @click="deleteCancel">Zrušit</button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'DeleteModal',
  emits: ['deleteConfirm', 'deleteCancel', 'update:visible'],
  props: {
    id: Number,
    visible: {
      type: Boolean,
      required: true
    }
  },
  setup (props, context) {
    const deleteConfirm = function () {
      console.log(props.id)
      context.emit('deleteConfirm', props.id)
      context.emit('update:visible', false)
    }
    const deleteCancel = function () {
      context.emit('deleteCancel')
      context.emit('update:visible', false)
    }
    return {
      deleteCancel,
      deleteConfirm
    }
  }
})
</script>

<style scoped>
  .modal-overlay {
    position: fixed;
    z-index: 98;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;

    padding: 10%;

    background: #00000088;
  }

  .modal-content {
    z-index: 99;
    background: #ffffff;
    border-radius: 0.8rem;
    width: 100%;
    height: 100%;
  }

  .modal-content header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    border-top-left-radius: 0.8rem;
    border-top-right-radius: 0.8rem;
    background:#eaeaea;
    height: 3em;
  }

  .modal-content header .close-button {
    border: 0;
    background: transparent;
    transition: 0.3s;
    color: #000000;
    text-shadow: 1px 1px #fff;
    font-size: 2rem;
    margin-right: 0.3em;
  }

  .modal-content header .close-button:hover {
    transition: 0.3s;
    color:#dc143c;
    cursor: pointer;
  }

  .modal-content header h3 {
    padding: 0.5em 1em;
  }

  .modal-content .modal-body {
    padding: 1em;
  }
</style>
