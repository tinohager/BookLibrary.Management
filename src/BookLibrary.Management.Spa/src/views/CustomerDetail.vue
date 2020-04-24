<template>
  <div>
    <div class="customerInfo">
      CustomerId: {{ id }}
    </div>
    <borrow-book :customer-id="parseInt(id)" />
  </div>
</template>

<script>
export default {
  name: 'CustomerDetail',
  components: {
    BorrowBook: () => import('@/components/Borrow/BorrowBook.vue')
  },
  props: {
    id: {
      type: String,
      required: true
    }
  }
}
</script>

<style scoped>
.customerInfo {
  margin-bottom: 20px;
}
</style>
