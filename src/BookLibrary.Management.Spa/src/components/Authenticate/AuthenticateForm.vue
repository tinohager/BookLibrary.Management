<template>
  <div>
    <div class="infoBox">
      <span>Predefined logins are already available here for test purposes</span>
      <el-form label-width="120px">
        <el-form-item label="Select user">
          <el-button
            type="info"
            @click="chooseAdmin"
          >
            Admin
          </el-button>
          <el-button
            type="info"
            @click="chooseCustomer1"
          >
            Customer 1
          </el-button>
          <el-button
            type="info"
            @click="chooseCustomer2"
          >
            Customer 2
          </el-button>
        </el-form-item>
      </el-form>
    </div>

    <el-form label-width="120px">
      <el-form-item label="Email">
        <el-input
          v-model="email"
          placeholder="email"
        />
      </el-form-item>
      <el-form-item label="Password">
        <el-input
          v-model="password"
          placeholder="password"
          show-password
        />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          @click="authenticate"
        >
          Authenticate
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
// import axios from 'axios'
export default {
  name: 'AuthenticateForm',
  data () {
    return {
      email: null,
      password: null
    }
  },
  methods: {
    chooseAdmin () {
      this.email = 'admin@booklibrary.com'
      this.password = 'admin'
    },
    chooseCustomer1 () {
      this.email = 'customer1@booklibrary.com'
      this.password = 'customer1'
    },
    chooseCustomer2 () {
      this.email = 'customer2@booklibrary.com'
      this.password = 'customer2'
    },
    async authenticate () {
      try {
        const response = await this.axios.post('/api/Authentication/Authenticate', {
          email: this.email,
          password: this.password
        })

        this.axios.defaults.headers.common.authorization = `Bearer ${response.data.token}`

        this.$notify.info({
          title: 'Success',
          message: 'Authenticate successful'
        })
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

<style scoped>
.infoBox {
  padding: 20px 16px 5px;
  background-color: #ffffec;
  border-radius: 4px;
  border-left: 5px solid #ffc850;
  margin: 20px 0px;
}

.infoBox span {
  font-weight: 600;
  margin-bottom: 10px;
  display: block;
}
</style>
