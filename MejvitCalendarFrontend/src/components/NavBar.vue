<template>
<nav class="navbar">
  <h2>
    <router-link to="/" class="navbar-brand" href="#">MejvitCalendar</router-link>
  </h2>
  <ul>
    <li>
      <router-link to="/single-events">Přehled událostí</router-link>
    </li>
    <li>
      <router-link to="/events">Přehled míst</router-link>
    </li>
    <li>
      <router-link to="/places">Správa míst</router-link>
    </li>
    <li>
      <router-link to="/categories">Správa kategorií</router-link>
    </li>
  </ul>
</nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'NavBar'
})
</script>

<style scoped>
.navbar {
  grid-column-start: 1;
  grid-column-end: span 3;
  display: flex;
  background: #e3d9fc;
}

.navbar h2 {
  margin: 0;
  padding: 0;
  display: inline-block;
  align-self: center;
  padding: 0 1em 0 1em;
}

.navbar h2 a {
  color: #333;
  text-decoration: none;
  text-shadow: 1px 1px #fff;
}

.navbar ul {
  margin: 0;
  padding: 0;
  overflow: hidden;
  align-items: stretch;
  display: inline-flex;
}

.navbar ul li {
  list-style-type: none;

}

.navbar ul li a {
  display: block;
  color: #333;
  text-decoration: none;
  height: 100%;
  padding: 1em 1em 0em 1em;
  text-shadow: 1px 1px #fff;
  transition: 0.5s;
}

.navbar ul li a:hover {
  text-shadow: 1px 1px #fff;
  background: #a794d6;
  transition: 0.5s;
}
</style>
