<template>
  <div>
    <div style="text-align: left" @click="show">
      {{ card.name }}
      <img
        v-if="card.description !== ''"
        src="../assets/more.png"
        alt="Minha Figura"
        class="image is-24x24"
      />
    </div>
    <div :class="{ modal: !isShow, 'modal is-active': isShow }">
      <div class="modal-background" @click="close"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <LabelEdit
            class="modal-card-title"
            style="text-align: left"
            :text="card.name"
            @updatedText="textUpdated($event)"
          ></LabelEdit>
          <button class="delete" aria-label="close" @click="close"></button>
        </header>
        <section class="modal-card-body">
          <textarea
            class="textarea is-primary"
            placeholder="Conteudo do Cartão"
            v-model="card.description"
          ></textarea>
        </section>
        <footer class="modal-card-foot">
          <button class="button is-danger" @click="close(true)">
            Deletar Cartão
          </button>
        </footer>
      </div>
    </div>
  </div>
</template>

<script>
import LabelEdit from "./LabelEdit.vue";
export default {
  data() {
    return {
      isShow: false,
      isDelete: false,
    };
  },
  props: {
    card: Object,
  },
  methods: {
    show: function () {
      //mostra o Modal
      this.isShow = true;
    },
    textUpdated: function (
      $event //atualiza o texto
    ) {
      this.card.name = $event;
    },
    close: function (
      isDelete //fecha o modal
    ) {
      this.isShow = false;
      this.$emit("close", { card: this.card, isDelete });
    },
  },
  components: {
    LabelEdit,
  },
};
</script>

<style scoped>
.event {
  pointer-events: auto;
}
</style>