<template>
  <div>
    <h1>Authenticate</h1>

    <authenticate-form></authenticate-form>
  </div>
</template>

<script>
export default {
  name: 'Authenticate',
  components: {
    AuthenticateForm: () => import('@/components/Authenticate/AuthenticateForm.vue')
  },
  data () {
    return {
      visible: false
    }
  }
}
</script>
