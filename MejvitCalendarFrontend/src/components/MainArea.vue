<template>
  <main class="main">
      <slot></slot>
  </main>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'MainArea'
})
</script>

<style scoped>
.main {
  grid-row-start: 2;
  grid-column-start: 2;
  grid-column-end: span 1;

  padding: 1rem;
}

</style>
