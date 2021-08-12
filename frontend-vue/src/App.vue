<template>
  <div id="app">
    <nav class="navbar is-primary">
      <div class="navbar-end">
        <div class="navbar-end navbar-item has-dropdown is-hoverable" v-if="User.name!= ''">
        <a class="navbar-link">
          {{User.name}}
        </a>
        <div class="navbar-dropdown is-boxed">
          <a class="navbar-item" @click="Logout">
            Deslogar
          </a>
        </div>
      </div>
        <div  v-else class="navbar-item">
          <div class="buttons">
            <a class="button is-light" href="/">Entrar</a>
          </div>
        </div>
      </div>
      
    </nav>




    <router-view @logado="NavLogado($event)"/>
  </div>
</template>


<script>
export default {
  created:function(){
    if(localStorage.getItem("user")!=null)
    {
      this.User = JSON.parse(localStorage.getItem("user"));
    }
  },

  data(){
      return{
        User:{
          id: 0,
          name: "",
          username: "",
        }
      }
  },

  methods:{
        NavLogado: function($event)
        {
          this.User.name = $event.User.name;
          this.User.id = $event.User.id;
          this.User.username = $event.User.username;
        },
        Logout: function()
        {
          localStorage.removeItem("user");
          this.User.id =0;
          this.User.name= "";
          this.User.username= "";
          this.$router.push("/");

        }
  }
};
</script>

<style>
:root {
  background-color: #eaeaea;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
