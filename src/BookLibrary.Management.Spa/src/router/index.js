import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/book',
    name: 'Book',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Book.vue')
  },
  {
    path: '/publisher',
    name: 'Publisher',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Publisher.vue')
  },
  {
    path: '/author',
    name: 'Author',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Author.vue')
  },
  {
    path: '/customer',
    component: () => import(/* webpackChunkName: "about" */ '../views/Parent.vue'),
    children: [
      {
        path: '',
        name: 'Customer',
        component: () => import(/* webpackChunkName: "about" */ '../views/Customer.vue'),
        props: true
      },
      {
        path: ':id',
        name: 'CustomerDetail',
        component: () => import(/* webpackChunkName: "about" */ '../views/CustomerDetail.vue'),
        props: true
      }
      // UserHome will be rendered inside User's <router-view>
      // when /user/:id is matched
      // ...other sub routes
    ]
  },
  {
    path: '/borrow',
    name: 'Borrow',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Borrow.vue')
  },
  {
    path: '/authenticate',
    name: 'Authenticate',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Authenticate.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
