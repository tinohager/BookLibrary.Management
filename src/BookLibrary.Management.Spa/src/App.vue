<template>
  <div id="app">
    <el-container>
      <el-header>
        <el-menu class="el-menu-demo" mode="horizontal" :router="true" :default-active="this.$route.path">
          <el-menu-item index="/">
            <template slot="title">
              <i class="el-icon-house"></i>
              <span>Home</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/book">
           <template slot="title">
              <i class="el-icon-notebook-2"></i>
              <span>Book</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/publisher">
            <template slot="title">
              <i class="el-icon-office-building"></i>
              <span>Publisher</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/author">
            <template slot="title">
              <i class="el-icon-mic"></i>
              <span>Author</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/customer">
            <template slot="title">
              <i class="el-icon-lock"></i>
              <span>Customer</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/borrow">
            <template slot="title">
              <i class="el-icon-lock"></i>
              <span>Borrow</span>
            </template>
          </el-menu-item>
          <el-menu-item index="/authenticate">
            <template slot="title">
              <i class="el-icon-user"></i>
              <span>Authenticate</span>
            </template>
          </el-menu-item>
        </el-menu>
      </el-header>
      <el-container>
        <el-main>
          <el-row>
            <el-col :span="24"><router-view/></el-col>
          </el-row>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<style scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
