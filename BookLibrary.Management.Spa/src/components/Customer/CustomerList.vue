<template>
    <div>
      <el-table
        :data="items">
        <el-table-column
          prop="id"
          label="Id"
          width="180">
        </el-table-column>
        <el-table-column
          prop="firstname"
          label="Firstname">
        </el-table-column>
        <el-table-column
          prop="surname"
          label="Surname">
        </el-table-column>
        <el-table-column
          prop="postalCode"
          label="PostalCode">
        </el-table-column>
        <el-table-column
          prop="city"
          label="City">
        </el-table-column>
      </el-table>
    </div>
</template>

<script>
export default {
  name: 'CustomerList',
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
      const response = await this.axios.get('api/Customers')
      this.items = response.data
    }
  }
}
</script>
