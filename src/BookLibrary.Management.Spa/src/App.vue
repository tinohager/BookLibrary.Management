<template>
  <div id="app">
    <el-container>
      <el-header height="100%">
        <el-menu
          class="el-menu"
          mode="horizontal"
          :router="true"
          :default-active="this.$route.path"
        >
          <el-menu-item index="/">
            <template slot="title">
              <i class="el-icon-house" />
              <span>Home</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/book">
            <template slot="title">
              <i class="el-icon-notebook-2" />
              <span>Book</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/publisher">
            <template slot="title">
              <i class="el-icon-office-building" />
              <span>Publisher</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/author">
            <template slot="title">
              <i class="el-icon-mic" />
              <span>Author</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/customer">
            <template slot="title">
              <i class="el-icon-lock" />
              <span>Customer</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/borrow">
            <template slot="title">
              <i class="el-icon-lock" />
              <span>Borrow</span>
            </template>
          </el-menu-item>
          <el-menu-item
            v-if="!isAuthenticated"
            index="/authenticate"
          >
            <template slot="title">
              <i class="el-icon-user" />
              <span>Authenticate</span>
            </template>
          </el-menu-item>
          <el-menu-item
            v-if="isAuthenticated"
            @click="clearToken"
          >
            <template slot="title">
              <i class="el-icon-user" />
              <span>Logout</span>
            </template>
          </el-menu-item>
        </el-menu>
      </el-header>
      <el-container>
        <el-main>
          <el-row>
            <el-col :span="24">
              <router-view />
            </el-col>
          </el-row>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
export default {
  name: 'App',
  computed: {
    isAuthenticated () {
      return this.$store.getters.isAuthenticated
    }
  },
  created () {
    this.$store.dispatch('tryAutoAuthenticate')
  },
  methods: {
    clearToken () {
      this.$store.dispatch('clearToken')
      this.$router.push('/')
    }
  }
}
</script>

<style>
* {
  font-family: Helvetica, Arial, sans-serif;
}
</style>

<style scoped>
#app {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

@media screen and (max-width: 600px) {
  .el-menu-item {
    padding: 0 6px;
  }
  .el-menu-item span {
    display: none;
  }
}
</style>
