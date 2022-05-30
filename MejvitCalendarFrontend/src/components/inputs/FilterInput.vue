<template>
  <span class="filter">
    <input type="text" :value="modelValue" @input="$emit('update:modelValue', $event.target.value)" placeholder="Filtrovat...">
    <i class="bi bi-search" v-if="modelValue === '' "></i>
    <i class="bi bi-x" v-else @click="$emit('update:modelValue', '')"></i>
  </span>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'FilterInput',
  props: {
    modelValue: {
      type: String,
      required: true
    }
  },
  emits: ['update:modelValue']
})
</script>

<style scoped>
  .filter {
    position: relative;
  }

  .filter input[type=text] {
    padding: 0.5em 2em 0.5em 0.5em;
  }

  .filter input[type=text]::placeholder {
    color: #a9a9a9;
  }

  .filter i {
    color: #a9a9a9;
    position: absolute;
    font-size: 1.2em;
    top: 0.4em;
    right: 0.6em;
  }
</style>
