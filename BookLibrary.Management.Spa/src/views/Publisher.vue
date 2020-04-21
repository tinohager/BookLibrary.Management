<template>
  <div>
    <h1>Publisher</h1>

    <el-button type="primary" @click="visible = true">Add</el-button>
    <el-dialog :visible.sync="visible" title="Add a Publisher">
      <publisher-add @reload="reload"></publisher-add>
    </el-dialog>

    <publisher-list ref="list"></publisher-list>
  </div>
</template>

<script>
export default {
  name: 'Publisher',
  components: {
    PublisherList: () => import('@/components/Publisher/PublisherList.vue'),
    PublisherAdd: () => import('@/components/Publisher/PublisherAdd.vue')
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
