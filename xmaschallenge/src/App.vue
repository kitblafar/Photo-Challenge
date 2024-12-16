
<template>

  <header>
    <img alt="xmas logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <h1 class="red">Xmas Photo Challenge</h1>
      <nav>
        <RouterLink to="/">Submit Your Photo</RouterLink>
        <RouterLink to="/view">View Your Photos</RouterLink>
      </nav>
    </div>
  </header>

  <main>
    <RouterView />
  </main>

</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    flex-direction: column;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
