<template>
  <div>
    <el-form :model="borrowInfo" ref="ruleForm">
      <el-form-item label="Book">
          <el-select v-model="borrowInfo.bookId" filterable placeholder="Select">
          <el-option
              v-for="item in books"
              :key="item.id"
              :label="item.title"
              :value="item.id">
          </el-option>
          </el-select>
      </el-form-item>
      <el-form-item>
          <el-button type="primary" @click="borrow">Borrow</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'BorrowBook',
  props: {
    customerId: {
      type: Number,
      required: true
    }
  },
  data () {
    return {
      errors: [],
      books: [],
      borrowInfo: {
        bookId: null,
        customerId: 0,
        startDate: null
      }
    }
  },
  mounted () {
    this.getBooks()
  },
  methods: {
    async getBooks () {
      try {
        const response = await this.axios.get('/api/Books')
        this.books = response.data
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })
      }
    },
    async borrow () {
      try {
        const today = new Date().toISOString()
        this.borrowInfo.customerId = this.customerId
        this.borrowInfo.startDate = today

        await this.axios.post('/api/Borrows', this.borrowInfo)

        this.$notify.info({
          title: 'Success',
          message: 'Add borrow successful'
        })
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })

        this.errors = error.response.data.errors
      }
    }
  }
}
</script>
