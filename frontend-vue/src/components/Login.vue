<template>
  <div class="column is-4 is-offset-4">
    <div class="box">
      <form class>
        <div class="field">
          <label class="label">Nome de Usuário</label>
          <div class="control">
            <input
              :class="{
                'is-danger input': isEmpty && User.username == '',
                input: true,
              }"
              type="text"
              v-model="User.username"
              placeholder="Usuário"
            />
          </div>
        </div>
        <br />
        <div class="field">
          <label class="label">Senha</label>
          <div class="control">
            <input
              :class="{
                'is-danger input': isEmpty && User.username == '',
                input: true,
              }"
              type="password"
              v-model="User.password"
              placeholder="********"
            />
          </div>
        </div>
      </form>
      <button class="button is-primary btn" @click="Login">Login</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  created: function () {
    if (localStorage.getItem("user") != null) {
      this.$router.push("Board");
    }
  },
  data() {
    return {
      User: {
        id: 0,
        name: "",
        username: "",
        password: "",
      },
      isEmpty: false,
    };
  },
  methods: {
    Login: function () {
      if (this.User.username == "" || this.User.password == "") {
        this.isEmpty = true;
      } else {
        this.isEmpty = false;
        axios
          .post("https://localhost:44360/Usuarios/Login", this.User)
          .then((res) => {
            if(res.data.id != null)
            {
              this.User.id = res.data.id;
              this.User.name = res.data.name;
              this.User.password = "";
              this.$emit("logado", { User: this.User });
              localStorage.setItem("user", JSON.stringify(this.User));
              this.$router.push("Board");
            }
            else
            {
              alert("Usuario ou senha incorretos ou inexistentes");
            }
          })
          .catch((error) => {
            console.log(error);
            
          });
      }
    },
  },
};
</script>

<style scoped>
.btn {
  margin-top: 4%;
}
</style>