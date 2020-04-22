<template>
  <div>
    <h1>Borrow</h1>

    <borrow-outstanding-list ref="list"></borrow-outstanding-list>
  </div>
</template>

<script>
export default {
  name: 'Borrow',
  components: {
    BorrowOutstandingList: () => import('@/components/Borrow/BorrowOutstandingList.vue')
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
