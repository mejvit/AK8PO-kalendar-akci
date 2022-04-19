<template>
<div>
  <div class="hours">
    <input type="text" :value="String(mDate.getHours()).padStart(2, '0')" class="py-2" placeholder="HH" />
    <button @click="toggleHours()">
      <span v-if="hoursSelectionVisible">&#8963;</span>
      <span v-else>&#8964;</span>
    </button>
    <ul v-if="hoursSelectionVisible" class="selector">
      <li v-for="(hour, i) in hours" :key="i" class="time-part-item py-1 px-3">
        <span @click="setHours(hour)">{{ String(hour).padStart(2, '0') }}</span>
      </li>
    </ul>
  </div>
  <div class="minutes">
    <input type="text" :value="String(mDate.getMinutes()).padStart(2, '0')" class="py-2" placeholder="mm"/>
    <button @click="toggleMinutes()" class="btn btn-primary">
      <span v-if="minutesSelectionVisible">&#8963;</span>
      <span v-else>&#8964;</span>
    </button>
    <ul v-if="minutesSelectionVisible" class="selector">
      <li v-for="(minute, i) in minutes" :key="i" class="time-part-item py-1 px-3">
        <span @click="setMinutes(minute)">{{ String(minute).padStart(2, '0') }}</span>
      </li>
    </ul>
  </div>
</div>
</template>

<script lang="ts">
import { defineComponent, ref, toRef } from 'vue'

export default defineComponent({
  name: 'DateTimePicker',
  emits: ['update:modelValue'],
  props: {
    modelValue: {
      type: Date,
      required: true
    }
  },
  setup (props, context) {
    const hoursSelectionVisible = ref<boolean>(false)
    const minutesSelectionVisible = ref<boolean>(false)
    const hours = ref<Array<number>>([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23])
    const minutes = ref<Array<number>>([
      0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
      10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
      20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
      30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
      40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
      50, 51, 52, 53, 54, 55, 56, 57, 58, 59
    ])

    const mDate = toRef(props, 'modelValue')
    mDate.value.setSeconds(0)
    mDate.value.setMilliseconds(0)

    const toggleHours = function () {
      hoursSelectionVisible.value = !hoursSelectionVisible.value
    }
    const toggleMinutes = function () {
      minutesSelectionVisible.value = !minutesSelectionVisible.value
    }

    const setHours = function (hour: number) {
      mDate.value.setHours(hour)
      toggleHours()
      context.emit('update:modelValue', mDate.value)
    }

    const setMinutes = function (minute: number) {
      mDate.value.setMinutes(minute)
      toggleMinutes()
      context.emit('update:modelValue', mDate.value)
    }

    return {
      hours,
      hoursSelectionVisible,
      mDate,
      minutes,
      minutesSelectionVisible,
      setHours,
      setMinutes,
      toggleHours,
      toggleMinutes
    }
  }
})
</script>

<style scoped>
.hours {
  display: inline-block;
}

.minutes {
  display: inline-block;
}

.selector {
  position: fixed;
  height: 50vh;
  overflow-y: scroll;
}

.selector li {
  text-align: right;
}

.selector li.time-part-item {
  list-style: none;
}

.selector li.time-part-item:nth-child(2n) {
  background: #eaeaea;
}

.selector li span:hover {
  cursor: pointer;
}
</style>
