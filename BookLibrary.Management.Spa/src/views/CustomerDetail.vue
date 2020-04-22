<template>
    <div>
      <div class="customerInfo">CustomerId: {{id}}</div>
      <borrow-book :customer-id="parseInt(id)"></borrow-book>
    </div>
</template>

<script>
export default {
  name: 'CustomerDetail',
  props: {
    id: String
  },
  components: {
    BorrowBook: () => import('@/components/Borrow/BorrowBook.vue')
  }
}
</script>

<style scoped>
.customerInfo {
  margin-bottom: 20px;
}
</style>
