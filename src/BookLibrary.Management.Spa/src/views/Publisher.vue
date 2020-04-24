<template>
  <div>
    <h1>Publisher</h1>

    <el-button
      type="primary"
      @click="visible = true"
    >
      Add <i class="el-icon-lock" />
    </el-button>
    <el-dialog
      :visible.sync="visible"
      title="Add a Publisher"
    >
      <publisher-add @reload="reload" />
    </el-dialog>

    <publisher-list ref="list" />
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
