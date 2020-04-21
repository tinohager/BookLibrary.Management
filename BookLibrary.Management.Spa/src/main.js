import Vue from 'vue'
import App from './App.vue'
import router from './router'

import axios from 'axios'
import VueAxios from 'vue-axios'
import Element from 'element-ui'
import locale from 'element-ui/lib/locale/lang/en'
import 'element-ui/lib/theme-chalk/index.css'

Vue.use(VueAxios, axios)
Vue.use(Element, { locale })

axios.interceptors.response.use((response) => {
  return response
}, (error) => {
  // unauthorized
  if (error.response.status === 401) {
    router.push('/authenticate')
  }

  return Promise.reject(error)
})

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
