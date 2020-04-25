<template>
  <div>
    <h1>Customer</h1>

    <el-button
      type="primary"
      @click="visible = true"
    >
      Add <i class="el-icon-lock" />
    </el-button>
    <el-dialog
      :visible.sync="visible"
      title="Add a Customer"
    >
      <customer-add @reload="reload" />
    </el-dialog>

    <customer-list ref="list" />
  </div>
</template>

<script>
export default {
  name: 'Customer',
  components: {
    CustomerList: () => import('@/components/Customer/CustomerList.vue'),
    CustomerAdd: () => import('@/components/Customer/CustomerAdd.vue')
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
