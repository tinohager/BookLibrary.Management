<template>
    <div>
      CustomerId: {{id}}
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
