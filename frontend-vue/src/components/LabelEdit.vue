<template>
  <div>
    <div @click="onClick" v-if="!edit">{{ text }}</div>
    <input
      ref="input"
      class="input"
      type="text"
      @keyup.enter="returnText"
      @blur="returnText"
      v-if="edit"
      v-model="label"
    />
  </div>
</template>

<script>
export default {
  data() {
    return {
      label: "",
      edit: false,
    };
  },
  props: {
    text: String,
  },
  methods: {
    initText: function () // inicializa a string do name
    {
      this.label = this.text;
    },
    onClick: function () {
      this.edit = true;
      this.initText();
      this.$nextTick(() => this.$refs.input.focus()); //quando vira Input ele da foco
    },
    returnText: function () {
      this.edit = false;
      this.$emit("updatedText", this.label); // retorna a string atualizado
    },
  },
};
</script>

<style scoped>
</style>