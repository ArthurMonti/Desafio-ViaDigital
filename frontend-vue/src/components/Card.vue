<template>
  <div style="cursor: pointer" class="cartao">
    <slot />
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
.cartao {
  background-color: white;
  border: 2px solid #ddd;
  margin-top: 0.5%;
  margin-right: 2%;
  padding: 12px;
  font-size: 18px;
  color: black;
  display: block;
  box-shadow: 10px 5px 0.5% white;
  border-radius: 0.4rem;
}
</style>