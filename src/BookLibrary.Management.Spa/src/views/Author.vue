<template>
  <div>
    <h1>Author</h1>

    <el-button
      type="primary"
      @click="visible = true"
    >
      Add <i class="el-icon-lock" />
    </el-button>
    <el-dialog
      :visible.sync="visible"
      title="Add an Author"
    >
      <author-add @reload="reload" />
    </el-dialog>

    <author-list ref="list" />
  </div>
</template>

<script>
export default {
  name: 'Author',
  components: {
    AuthorList: () => import('@/components/Author/AuthorList.vue'),
    AuthorAdd: () => import('@/components/Author/AuthorAdd.vue')
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
