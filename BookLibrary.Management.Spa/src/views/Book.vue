<template>
  <div>
    <h1>Book</h1>

    <el-button type="primary" @click="visible = true">Add</el-button>
    <el-dialog :visible.sync="visible" title="Add a Book">
      <book-add @reload="reload"></book-add>
    </el-dialog>

    <book-list ref="list"></book-list>
  </div>
</template>

<script>
export default {
  name: 'Book',
  components: {
    BookList: () => import('@/components/Book/BookList.vue'),
    BookAdd: () => import('@/components/Book/BookAdd.vue')
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
