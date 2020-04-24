import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    jwtToken: null
  },
  getters: {
    isAuthenticated: state => {
      return state.jwtToken !== null
    }
  },
  mutations: {
    setToken (state, token) {
      state.jwtToken = token
      Vue.axios.defaults.headers.common.authorization = `Bearer ${token}`
    },
    clearToken (state) {
      state.jwtToken = null
      Vue.axios.defaults.headers.common.authorization = null
    }
  },
  actions: {
    async authenticate ({ commit }, credential) {
      try {
        const response = await Vue.axios.post('/api/Authentication/Authenticate', {
          email: credential.email,
          password: credential.password
        })

        localStorage.setItem('token', response.data.token)

        commit('setToken', response.data.token)

        return true
      } catch (error) {
        return false
      }
    },
    tryAutoAuthenticate ({ commit }) {
      const token = localStorage.getItem('token')
      if (token !== null) {
        commit('setToken', token)
      }
    },
    clearToken ({ commit }) {
      commit('clearToken')
    }
  }
})
