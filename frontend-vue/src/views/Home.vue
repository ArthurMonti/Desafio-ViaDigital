<template>
  <div>
    <div class="hero-body">
      <p class="title">Efetuar Login</p>
      <Login @logado="Logado($event)" />
    </div>
    <label class="checkbox">
      Não possui conta?<a href="Register"> Realize seu cadastro!</a>
    </label>
  </div>
</template>

<script>
import Login from "../components/Login.vue";

export default {
  components: {
    Login,
  },
  methods: {
    Logado: function ($event) {
      this.$emit("logado", { User: $event.User });
    },
  },
};
</script>

<style scoped>
body {
  background-color: #b6aeac;
}
</style>
