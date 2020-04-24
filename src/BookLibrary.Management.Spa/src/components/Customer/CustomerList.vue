<template>
  <div>
    <el-table
      v-loading="loading"
      :data="items"
    >
      <el-table-column
        prop="id"
        label="Id"
        width="50"
      />
      <el-table-column
        prop="firstname"
        label="Firstname"
      />
      <el-table-column
        prop="surname"
        label="Surname"
      />
      <el-table-column
        prop="postalCode"
        label="PostalCode"
      />
      <el-table-column
        prop="city"
        label="City"
      />
      <el-table-column
        fixed="right"
        label="Operations"
        width="120"
      >
        <template slot-scope="scope">
          <router-link :to="'customer/' + scope.row.id">
            <el-button type="primary">
              Detail
            </el-button>
          </router-link>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  name: 'CustomerList',
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
      try {
        this.loading = true
        const response = await this.axios.get('/api/Customers')
        this.items = response.data
      } catch (error) {

      } finally {
        this.loading = false
      }
    }
  }
}
</script>
