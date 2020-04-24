<template>
  <div>
    <el-table
      v-loading="loading"
      :data="items"
    >
      <el-table-column
        prop="bookId"
        label="Book"
      >
        <template slot-scope="scope">
          <span><strong>{{ scope.row.bookTitle }}</strong><br>{{ scope.row.bookId }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="customerId"
        label="CustomerId"
        width="100"
      />
      <el-table-column
        prop="customerName"
        label="Customer Name"
      />
      <el-table-column
        prop="startDate"
        label="Start Date"
      >
        <template slot-scope="scope">
          <span class="date">{{ scope.row.startDate }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="endDate"
        label="End Date"
      />
      <el-table-column
        prop="durationDays"
        label="Borrow days"
        sortable
      />
      <el-table-column
        prop="feePrice"
        label="Fee Price (€)"
      />
      <el-table-column
        fixed="right"
        label="Operations"
        width="120"
      >
        <template slot-scope="scope">
          <el-button
            type="primary"
            @click="returnBook(scope.row.customerId, scope.row.bookId, scope.row.startDate)"
          >
            Return
          </el-button>
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
      loading: false,
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
      this.loading = false
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
