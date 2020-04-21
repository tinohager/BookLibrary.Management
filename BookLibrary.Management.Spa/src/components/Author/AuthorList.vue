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
          prop="name"
          label="Name">
        </el-table-column>
      </el-table>
    </div>
</template>

<script>
export default {
  name: 'AuthorList',
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
      const response = await this.axios.get('api/Authors')
      this.items = response.data
    }
  }
}
</script>
