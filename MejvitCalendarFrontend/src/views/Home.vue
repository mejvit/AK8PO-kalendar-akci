<template>
  <main-area class="no-sidebar">
    <HelloWorld msg="Welcome to Your Vue.js + TypeScript App"/>
  </main-area>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import HelloWorld from '../components/HelloWorld.vue' // @ is an alias to /src
import MainArea from '../components/MainArea.vue' // @ is an alias to /src

export default defineComponent({
  name: 'Home',
  components: {
    HelloWorld, MainArea
  }
})
</script>
