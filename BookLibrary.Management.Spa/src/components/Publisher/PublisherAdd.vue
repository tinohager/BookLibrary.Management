<template>
    <div>
      <el-form>
        <el-form-item>
          <el-input placeholder="Name" v-model="name"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="add">Add</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<script>
export default {
  name: 'PublisherAdd',
  data () {
    return {
      name: null
    }
  },
  methods: {
    async add () {
      try {
        await this.axios.post('api/Publishers', {
          name: this.name
        })

        this.$emit('reload')
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
