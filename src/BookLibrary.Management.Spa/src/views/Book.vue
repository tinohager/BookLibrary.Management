<template>
  <div>
    <h1>Book</h1>

    <el-button
      type="primary"
      @click="visible = true"
    >
      Add <i class="el-icon-lock" />
    </el-button>
    <el-dialog
      :visible.sync="visible"
      title="Add a Book"
    >
      <book-add @reload="reload" />
    </el-dialog>

    <book-list ref="list" />
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
