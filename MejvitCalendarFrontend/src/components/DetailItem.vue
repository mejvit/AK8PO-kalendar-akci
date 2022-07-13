<template>
  <div class="detail-item">
    <router-link :to="to" class="link">
      <span>
        <slot></slot>
      </span>
    </router-link>
    <button class="delete" @click="$emit('deleteClick', id)">
      <i class="bi bi-trash3-fill"></i>
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'DetailItem',
  props: {
    id: Number,
    to: String
  },
  emits: ['deleteClick'],
  setup () {
    const editMode = ref<boolean>(false)
    const switchMode = () => { editMode.value = !editMode.value }

    const test = function (id: number) { console.log(id) }
    return {
      editMode,
      switchMode,
      test
    }
  }
})
</script>

<style lang="scss" scoped>
  .detail-item {
    display: flex;
    align-items: stretch;
    justify-content: center;
    color: #333;
    height: 3em;
    box-sizing: border-box;

    &:first-of-type {
      margin-top: 1em;
      border-top: 3px solid #eaeaea;
    }

    &:last-of-type {
      margin-bottom: 1em;
      border-bottom: 3px solid #eaeaea;
    }

    &:nth-child(2n) {
      background: #eaeaea;
    }

    &:hover {
      background: #cacaca;
    }

    .link {
      flex-grow: 1;
      color: #333;
      text-decoration: none;
      display: flex;
      align-items: center;

      span {
        padding-left: 1em;
        font-weight: bold;
      }
    }

    .delete {
      background: transparent;
      border: 0;
      flex-grow: 0;
      width: 3em;
      &:hover {
        cursor: pointer;

        i {
          color: $danger-red;
        }
      }

      i {
        color: #333;
      }
    }
  }
</style>
