<template>
  <div>
    <h1>Customer</h1>

    <customer-list ref="list"></customer-list>
  </div>
</template>

<script>
export default {
  name: 'Customer',
  components: {
    CustomerList: () => import('@/components/Customer/CustomerList.vue')
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
