<script>

export default {
  data() {
    return {
      activator: false,
    }
  },
  methods: {
    makeBurger() {
      console.log("making burgers")
      this.activator = !this.activator
      console.log(this.activator)
      return this.activator
    }
  }
}
</script>

<template>
  <header>
    <section class="hero is-danger">
      <div class="hero-body">
        <p class="title">Christmas Photo Challenge</p>
        <p class="subtitle">Catch them all for great prizes</p>
      </div>
    </section>

    <section>
      <nav class="navbar is-success" id="main nav-bar" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
          <a class="navbar-item" href="https://kittyfarren.dev/xmaschallenge">
            <img width="30" height="300" fill="none" src="./assets/logo.svg"></img>
          </a>
          <button id="nav-toggle" :class="{ 'is-active': activator }" class="navbar-burger" @click="makeBurger"
            aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
          </button>
        </div>

        <div class="navbar-menu" :class="{ 'is-active': activator }">
          <RouterLink to="/login" class="navbar-item">Host Area</RouterLink>
          <RouterLink to="/" class="navbar-item">Submit Photos</RouterLink>
          <RouterLink to="/view" class="navbar-item">View Your Photos</RouterLink>
          <RouterLink to="/leaderboard" class="navbar-item">Leaderboard</RouterLink>
        </div>
      </nav>
    </section>
  </header>

  <main>
    <RouterView/>
  </main>

</template>

<style scoped>
header {
  line-height: 1.5;
  flex-direction: column;

}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }
}
</style>
