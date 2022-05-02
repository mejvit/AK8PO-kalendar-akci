<template>
<div class="time-picker-wrapper">
  <div class="hours time-part">
    <input type="text" :value="String(mDate.getHours()).padStart(2, '0')" placeholder="hh" />
    <button @click="toggleHours()" tabindex="-1">
      <span v-if="hoursSelectionVisible">&#9650;</span>
      <span v-else>&#9660;</span>
    </button>
    <ul v-if="hoursSelectionVisible" class="selector">
      <li v-for="(hour, i) in hours" :key="i" class="time-part-item">
        <span @click="setHours(hour)">{{ String(hour).padStart(2, '0') }}</span>
      </li>
    </ul>
  </div>
  <div class="delimiter">
    <span>:</span>
  </div>
  <div class="minutes time-part">
    <input type="text" :value="String(mDate.getMinutes()).padStart(2, '0')" id="minuteSelector" placeholder="mm"/>
    <button @click="toggleMinutes()" tabindex="-1">
      <span v-if="minutesSelectionVisible">&#9650;</span>
      <span v-else>&#9660;</span>
    </button>
    <ul v-if="minutesSelectionVisible" class="selector">
      <li v-for="(minute, i) in minutes" :key="i" class="time-part-item">
        <span @click="setMinutes(minute)">{{ String(minute).padStart(2, '0') }}</span>
      </li>
    </ul>
  </div>
</div>
</template>

<script lang="ts">
import { defineComponent, ref, toRef } from 'vue'

export default defineComponent({
  name: 'TimePicker',
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
.time-picker-wrapper {
  width: 16rem;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  align-items: stretch;
}
.time-part {
  margin: 0 0.5rem;
  display: flex;
  justify-content: center;
  align-items: stretch;
}

.delimiter {
  cursor: default;
  font-size: 1.5rem;
}

.selector {
  position: fixed;
  width: 3rem;
  margin: 2.8rem 0 0 -2rem;
  padding: 0;
  height: 50vh;
  overflow-y: scroll;
}

.selector li {
  width: 3rem;
  font-size: 1.2rem;
  text-align: right;
}

.selector li.time-part-item {
  display: block;
  padding: 0.2rem;
  list-style: none;
  width: 100%;
}

.selector li.time-part-item:nth-child(2n+1) {
  background: #ffffff;
}

.selector li.time-part-item:nth-child(2n) {
  background: #eaeaea;
}

.selector li span:hover {
  cursor: pointer;
}

.time-part input[type=text]
{
  width: 3rem;
  height: 100%;
  border-top: 1px solid #5f5f5f;
  border-right: 0;
  border-bottom: 1px solid #5f5f5f;
  border-left: 1px solid #5f5f5f;
  outline: 0;
  margin: 0;
  padding: 0.2rem 0.2rem 0.2rem 0.4rem;

  font-size: 1.5rem;
  text-align: center;
  border-top-left-radius: 0.3rem;
  border-bottom-left-radius: 0.3rem;
}

.time-part button {
  width: 2rem;
  height: 100%;
  padding: 0.2rem;
  font-size: 0.8rem;
  color: #666666;
  background: #e3e3e3;
  border-top: 1px solid #5f5f5f;
  border-right: 1px solid #5f5f5f;
  border-bottom: 1px solid #5f5f5f;
  border-left: 0;
  border-top-right-radius: 0.3rem;
  border-bottom-right-radius: 0.3rem;
}
</style>
