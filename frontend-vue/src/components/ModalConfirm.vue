<template>
  <div>
    <a type="button" class="navbar-item" @click="show"> Deletar </a>
    <div :class="{ modal: !isShow, 'modal is-active': isShow }">
      <div class="modal-background" @click="close(false)"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">Deseja realmente excluir?</p>
          <button class="delete" aria-label="close"></button>
        </header>
        <footer class="modal-card-foot">
          <button class="button is-success" @click="close(true)">Sim</button>
          <button class="button is-danger" @click="close(false)">Não</button>
        </footer>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isShow: false,
    };
  },
  methods: {
    show: function () {
      this.isShow = true;
    },
    close: function (confirm) {
      this.isShow = false;
      this.$emit("confirm", confirm);
    },
  },
};
</script>

<style scoped>
</style>