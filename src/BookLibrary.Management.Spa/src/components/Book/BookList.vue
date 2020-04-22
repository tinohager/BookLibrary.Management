<template>
    <div>
      <el-table
        :data="items">
        <el-table-column
          prop="id"
          label="Image"
          width="80">
            <template slot-scope="scope">
              <span><img :src="'https://cover.ekz.de/' + scope.row.id + '.jpg'" width="40" /></span>
            </template>
        </el-table-column>
        <el-table-column
          prop="id"
          label="Id / ISBN"
          width="180">
        </el-table-column>
        <el-table-column
          prop="title"
          label="Title">
        </el-table-column>
        <el-table-column
          prop="abstract"
          label="Abstract">
        </el-table-column>
        <el-table-column
          prop="bookCount"
          label="Book Count">
        </el-table-column>
      </el-table>
    </div>
</template>

<script>
export default {
  name: 'BookList',
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
      const response = await this.axios.get('/api/Books')
      this.items = response.data
    }
  }
}
</script>
