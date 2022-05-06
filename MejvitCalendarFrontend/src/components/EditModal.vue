<template>
  <div class="modal-overlay" v-if="visible">
    <div class="modal-content">
      <header>
        <h3>{{ title }}</h3>
        <button class="close-button" @click="$emit('dismissClick')">&times;</button>
      </header>
      <div class="modal-body">
        <slot></slot>
      </div>
      <footer>
        <button @click="$emit('confirmClick')" class="button-confirm">
          <i class="bi bi-check-square-fill"></i> Uložit
        </button>
        <button @click="$emit('dismissClick')" class="button-dismiss">
          <i class="bi bi-x-square-fill"></i> Zrušit
        </button>
      </footer>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'EditModal',
  emits: ['confirmClick', 'dismissClick'],
  props: {
    title: String,
    visible: {
      type: Boolean,
      required: true
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
    background: #00000088;
    display: flex;
  }

  .modal-content {
    z-index: 99;
    background: #ffffff;
    border-radius: 0.8rem;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    margin: auto auto;
    width: 50%;
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
    color: #333333;
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
    color: #333333;
    padding: 0.5em 1em;
  }

  .modal-content .modal-body {
    padding: 1em;
  }

  .modal-content footer {
    display: flex;
    justify-content: center;
  }

  .modal-content footer button {
    font-size: 1.1em;
    margin: 1em;
    padding: 0.5em 1em;
    border: 0;
    color: #333333;
    background: #eaeaea;
    transition: 0.3s;
    border-radius: 0.25rem;
  }

  .modal-content footer button i {
    margin-right: 0.5em;
    border-radius: 0.25rem;
  }

  .modal-content footer button:hover {
    cursor: pointer;
    transition: 0.3s;
  }

  .button-confirm i {
    color: #3a9439;
  }

  .button-dismiss i {
    color: #dc143c;
  }

  .button-confirm:hover {
    color: #eaeaea;
    background: #3a9439;
  }

  .button-dismiss:hover {
    color: #eaeaea;
    background: #dc143c;
  }

  .button-confirm:hover i {
    color: #eaeaea;
    transition: 0.3s;
  }

  .button-dismiss:hover i {
    color: #eaeaea;
    transition: 0.3s;
  }
</style>
