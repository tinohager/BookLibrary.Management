<template>
  <div>
    <el-form
      ref="ruleForm"
      :model="borrowInfo"
      label-width="120px"
    >
      <el-form-item label="Start Date">
        <el-date-picker
          v-model="borrowInfo.startDate"
          type="date"
          placeholder="Pick a day"
        />
      </el-form-item>
      <el-form-item label="Book">
        <el-select
          v-model="borrowInfo.bookId"
          filterable
          placeholder="Select"
        >
          <el-option
            v-for="item in books"
            :key="item.id"
            :label="item.title"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          @click="borrow"
        >
          Borrow
        </el-button>
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
  async mounted () {
    this.borrowInfo.customerId = this.customerId
    await this.getBooks()
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
        await this.axios.post('/api/Borrows', this.borrowInfo)

        this.$notify.info({
          title: 'Success',
          message: 'Borrow successful'
        })
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: `Cannot borrow the book, ${error.response.data.title}`
        })

        this.errors = error.response.data.errors
      }
    }
  }
}
</script>
