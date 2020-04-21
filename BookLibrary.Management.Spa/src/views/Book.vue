<template>
  <div>
    <h1>Book</h1>

    <book-list ref="list"></book-list>
  </div>
</template>

<script>
export default {
  name: 'Book',
  components: {
    BookList: () => import('@/components/Book/BookList.vue')
  },
  data () {
    return {
      visible: false
    }
  },
  methods: {
    reload () {
      this.visible = false
      this.$refs.list.getItems()
    }
  }
}
</script>
