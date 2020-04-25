<template>
  <div>
    <el-form
      :model="model"
      label-width="150px"
    >
      <el-form-item
        label="Firstname *"
        prop="firstname"
        :error="getErrorForField('firstname')"
      >
        <el-input
          v-model="model.firstname"
          placeholder="Firstname"
        />
      </el-form-item>
      <el-form-item
        label="Surname *"
        prop="surname"
        :error="getErrorForField('surname')"
      >
        <el-input
          v-model="model.surname"
          placeholder="Surname"
        />
      </el-form-item>
      <el-form-item
        label="Street"
        prop="street"
        :error="getErrorForField('street')"
      >
        <el-input
          v-model="model.street"
          placeholder="Street"
        />
      </el-form-item>
      <el-form-item
        label="PostalCode"
        prop="postalcode"
        :error="getErrorForField('postalcode')"
      >
        <el-input
          v-model="model.postalcode"
          placeholder="PostalCode"
        />
      </el-form-item>
      <el-form-item
        label="City"
        prop="city"
        :error="getErrorForField('city')"
      >
        <el-input
          v-model="model.city"
          placeholder="City"
        />
      </el-form-item>
      <el-form-item
        label="CountryCode"
        prop="countrycode"
        :error="getErrorForField('countrycode')"
      >
        <el-input
          v-model="model.countrycode"
          placeholder="CountryCode"
        />
      </el-form-item>
      <el-form-item
        label="Email *"
        prop="email"
        :error="getErrorForField('email')"
      >
        <el-input
          v-model="model.email"
          placeholder="Email"
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
  name: 'CustomerAdd',
  data () {
    return {
      model: {
        gender: 'unknown',
        firstname: '',
        surname: '',
        street: '',
        postalCode: '',
        city: '',
        countryCode: '',
        email: ''
      },
      errors: []
    }
  },
  methods: {
    async add () {
      try {
        await this.axios.post('/api/Customers', this.model)

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
