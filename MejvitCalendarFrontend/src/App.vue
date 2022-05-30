<template>
<div class="app-container">
  <nav-bar />
  <router-view/>
</div>
</template>
<script>
import { defineComponent } from 'vue'
import NavBar from './components/NavBar.vue'

export default defineComponent({
  components: {
    NavBar
  }
})
</script>

<style lang="scss">
.app-container {
  min-height: 100vh;
  display: grid;
  grid-template-columns: 1fr 3fr 1fr;
  grid-template-rows: 3rem auto;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
