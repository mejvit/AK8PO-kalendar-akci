<template>
  <div class="sidebar">
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'SideBar'
})
</script>

<style scoped>
.sidebar {
  grid-row-start: 2;
  grid-column-start: 1;
  grid-column-end: span 1;

  background: #666;
}

.sidebar:deep() input.search-input {
  border-radius: 0.4rem;
  border: 1px solid #555;
  width: calc(100% - 3em);
  margin: 0.8em 1em 0.8em 1em;
  padding: 0.5em;
}

.sidebar:deep() ul {
  margin: 0;
  padding: 0;
}

.sidebar:deep() ul li {
  list-style: none;
  border-bottom: 1px solid #555;
}

.sidebar:deep() ul li a {
  display: block;
  width: calc(100%-1em);
  color: #fff;
  text-decoration: none;
  text-shadow: 1px 1px #333;
  padding: 0.8em 0em 0.8em 1em;
}

.sidebar:deep() ul li a:hover {
  background: #333;
}
</style>
