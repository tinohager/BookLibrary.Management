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
      const response = await this.axios.get('api/Borrows/Outstanding')
      this.items = response.data
    }
  }
}
</script>
