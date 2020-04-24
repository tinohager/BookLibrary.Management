<template>
  <div>
    <el-table
      v-loading="loading"
      :data="items"
    >
      <el-table-column
        prop="id"
        label="Id"
        width="180"
      />
      <el-table-column
        prop="name"
        label="Name"
      />
    </el-table>
  </div>
</template>

<script>
export default {
  name: 'PublisherList',
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
      const response = await this.axios.get('/api/Publishers')
      this.items = response.data
      this.loading = false
    }
  }
}
</script>
