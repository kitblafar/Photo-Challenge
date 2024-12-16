<script>
export default {
  methods: {
    goToAbout() {
      this.$router.push('/view')
    },
  },
}
</script>

<template>
  <div class="photosubmit">
    <h1 class="red">Submit your Photo</h1>
    <h3>
      The Rules:
    </h3>
    <ul>
      <li>All tasks MUST be accompanied by an image.</li>
      <li>You may not submit two images to the same task, the most recent submission will be taken.</li>
      <li>The scoring will be one point per task.</li>
    </ul>

    <h3>Prizes:</h3>
      <ul>
        <li>Highest Scorer.</li>
        <li>Best Image Overall.</li>
        <li>Highest Jump (TASK 24)</li>
      </ul>
  </div>
</template>

<style scoped>
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
}

h3 {
  font-size: 1.2rem;
}

.greetings h1,
.greetings h3 {
  text-align: center;
}

@media (min-width: 1024px) {
  .greetings h1,
  .greetings h3 {
    text-align: left;
  }
}
</style>
