<template>
    <div>
      <el-table
        :data="items">
        <el-table-column
          prop="bookId"
          label="BookId"
          width="180">
        </el-table-column>
        <el-table-column
          prop="customerId"
          label="CustomerId">
        </el-table-column>
        <el-table-column
          prop="startDate"
          label="StartDate">
        </el-table-column>
        <el-table-column
          prop="endDate"
          label="EndDate">
        </el-table-column>
        <el-table-column
          prop="feePrice"
          label="FeePrice (€)">
        </el-table-column>
        <el-table-column
          fixed="right"
          label="Operations"
          width="120">
          <template slot-scope="scope">
            <el-button type="primary" @click="returnBook(scope.row.customerId, scope.row.bookId, scope.row.startDate)">Return</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
</template>

<script>
export default {
  name: 'BorrowOutstandingList',
  data () {
    return {
      loading: true,
      items: null
    }
  },
  async created () {
    await this.getItems()
  },
  methods: {
    async getItems () {
      this.loading = true
      const response = await this.axios.get('/api/Borrows/Outstanding')
      this.items = response.data
    },
    async returnBook (customerId, bookId, startDate) {
      try {
        await this.axios.put('/api/Borrows', {
          bookId: bookId,
          customerId: customerId,
          startDate: startDate,
          endDate: new Date().toISOString()
        })
        this.$notify.info({
          title: 'Success',
          message: 'Add borrow successful'
        })
        this.getItems()
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })
      }
    }
  }
}
</script>
