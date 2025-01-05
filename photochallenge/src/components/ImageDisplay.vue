<script>
export default {
  props: ["image", "challengeNumber", "challengeString", "name"],
};
</script>


<template>
  <h3 class="subtitle">Challenge #{{challengeNumber}} - {{challengeString}}</h3>
  <h4 class="subtitle">from {{name}} </h4>
    <div>
      <img v-bind:src="image"  alt="challenge image"/>
    </div>
  </template>

<style scoped>

</style>