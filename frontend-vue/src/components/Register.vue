<template>
  <div class="column is-half is-offset-one-quarter">
    <div class="box">
      <form>
        <div class="field">
          <label class="label is-left">Nome Completo</label>
          <div class="control">
            <input
              :class="{
                'is-danger input': isEmpty && User.name == '',
                input: true,
              }"
              type="text"
              placeholder="Nome"
              v-model="User.name"
            />
            <p
              v-if="isEmpty && User.name == ''"
              style="text-align: left"
              class="help is-danger"
            >
              Preencha este campo
            </p>
          </div>
        </div>
        <br />
        <div class="field">
          <label class="label">Nome de Usuário</label>
          <div class="control">
            <input
              :class="{
                'is-danger input': isEmpty && User.username == '',
                input: true,
              }"
              type="text"
              placeholder="Usuário"
              v-model="User.username"
            />
            <p
              v-if="isEmpty && User.username == ''"
              style="text-align: left"
              class="help is-danger"
            >
              Preencha este campo
            </p>
          </div>
        </div>
        <br />
        <div class="field">
          <label class="label">Senha</label>
          <div class="control">
            <input
              :class="{
                'is-danger input': isEmpty && User.password == '',
                input: true,
              }"
              type="password"
              placeholder="********"
              v-model="User.password"
            />
            <p
              v-if="isEmpty && User.password == ''"
              style="text-align: left"
              class="help is-danger"
            >
              Preencha este campo
            </p>
          </div>
        </div>
      </form>
      <br />
      <button class="button is-primary btn" @click="Register">Registrar</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  components: {},
  data() {
    return {
      User: {
        name: "",
        username: "",
        password: "",
      },
      isEmpty: false,
    };
  },
  methods: {
    Register: function () {
      if (
        this.User.name == "" ||
        this.User.username == "" ||
        this.User.password == ""
      ) {
        this.isEmpty = true;
      } else {
        this.isEmpty = false;
        axios
          .post("https://localhost:44360/Usuarios", this.User)
          .then(() => {
            alert("Registro realizado com Sucesso!");
            this.$router.push("/");
          })
          .catch((error) => {
            console.log(error);
            alert(error);
          });
      }
    },
  },
};
</script>

<style scoped>
.footer {
  position: absolute;
  bottom: 0;
}
</style>