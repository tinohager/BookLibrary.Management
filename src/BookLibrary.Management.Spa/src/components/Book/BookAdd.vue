<template>
  <div>
    <el-form
      ref="ruleForm"
      :model="book"
      label-width="150px"
    >
      <el-form-item label="ISBN">
        <el-input
          v-model="book.isbn"
          placeholder="ISBN"
        />
      </el-form-item>
      <el-form-item
        label="Title"
        prop="title"
        :error="getErrorForField('title')"
      >
        <el-input
          v-model="book.title"
          placeholder="Title"
        />
      </el-form-item>
      <el-form-item
        label="Publisher"
        prop="publisherId"
        :error="getErrorForField('publisherId')"
      >
        <el-select
          v-model="book.publisherId"
          filterable
          placeholder="Select"
        >
          <el-option
            v-for="item in publishers"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item
        label="Author"
        prop="authorIds"
        :error="getErrorForField('authorIds')"
      >
        <el-select
          v-model="book.authorIds"
          filterable
          multiple
          placeholder="Select"
        >
          <el-option
            v-for="item in authors"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Abstract">
        <el-input
          v-model="book.abstract"
          type="textarea"
          placeholder="Abstract"
        />
      </el-form-item>
      <el-form-item label="Number of copies">
        <el-input
          v-model.number="book.bookCount"
          type="number"
          placeholder="Number of copies"
        />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          @click="add"
        >
          Add
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'BookAdd',
  data () {
    return {
      book: {
        isbn: null,
        title: '',
        authorIds: [],
        publisherId: 1,
        abstract: null,
        bookCount: 1
      },
      errors: [],
      authors: [],
      publishers: []
    }
  },
  async mounted () {
    await this.getAuthors()
    await this.getPublishers()
  },
  methods: {
    async getAuthors () {
      try {
        const response = await this.axios.get('/api/Authors')
        this.authors = response.data
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })
      }
    },
    async getPublishers () {
      try {
        const response = await this.axios.get('/api/Publishers')
        this.publishers = response.data
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })
      }
    },
    async add () {
      try {
        await this.axios.post('/api/Books', this.book)

        this.$emit('reload')
      } catch (error) {
        this.$notify.error({
          title: 'Error',
          message: error.response.data.title
        })

        this.errors = error.response.data.errors
      }
    },
    getErrorForField (field) {
      if (!this.errors) {
        return false
      }

      const keys = Object.keys(this.errors)
      const key = keys.find(element => element.toLowerCase() === field)
      if (this.errors[key]) {
        return this.errors[key][0]
      }
    }
  }
}
</script>
