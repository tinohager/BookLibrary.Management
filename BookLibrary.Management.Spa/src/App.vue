<template>
  <div id="app">
    <el-container>
      <el-header>
        <el-menu class="el-menu-demo" mode="horizontal" :router="true" :default-active="this.$route.path">
          <el-menu-item index="/">Home</el-menu-item>
          <el-menu-item index="/book">Book</el-menu-item>
          <el-menu-item index="/customer">Customer</el-menu-item>
          <el-menu-item index="/publisher">Publisher</el-menu-item>
          <el-menu-item index="/author">Author</el-menu-item>
          <el-menu-item index="/borrow">Borrow</el-menu-item>
          <el-menu-item index="/authenticate">Authenticate</el-menu-item>
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
