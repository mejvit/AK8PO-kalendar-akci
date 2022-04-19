<template>
  <div class="about">
    <h1>Přidat událost</h1>
    <time-picker  v-model="startTime" />
    <button @click="showMeDateValue">date</button>
    <input type="text" :value="eventName">
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import TimePicker from '@/components/TimePicker.vue' // @ is an alias to /src

export default defineComponent({
  name: 'Events',
  components: {
    TimePicker
  },
  setup () {
    const startTime = ref<Date>(new Date())
    const showMeDateValue = function () {
      console.log(startTime.value)
    }

    const eventName = ref<string>('test')

    return {
      eventName,
      showMeDateValue,
      startTime
    }
  }
})
</script>
