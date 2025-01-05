<script>

export default {
  data() {
    return {
      activator: false,
    }
  },
  name: "MainPage",
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
  <vue-particles id="tsparticles" @particles-loaded="particlesLoaded" :options="{
    particles: {
      color: {
        value: '#ffffff'
      },
      move: {
        direction: 'bottom',
        enable: true,
        outModes: 'out',
        speed: 2
      },
      number: {
        density: {
          enable: true,
          area: 800
        },
        value: 400
      },
      opacity: {
        value: 0.7
      },
      shape: {
        type: 'circle'
      },
      size: {
        value: 10
      },
      wobble: {
        enable: true,
        distance: 10,
        speed: 10
      },
      zIndex: {
        value: {
          min: 0,
          max: 100
        }
      }
    }
  }"/>
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
            <img width="30" height="300" src="../assets/logo.svg" alt="logo"/>
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
          <RouterLink to="/main/hostlogin" class="navbar-item">Host Area</RouterLink>
          <RouterLink to="/main" class="navbar-item">Submit Photos</RouterLink>
          <RouterLink to="/main/view" class="navbar-item">View Your Photos</RouterLink>
          <RouterLink to="/main/voting" class="navbar-item">Voting</RouterLink>
          <RouterLink to="/main/leaderboard" class="navbar-item">Leaderboard</RouterLink>
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

@media (min-width: 1024px) {

  .logo {
    margin: 0 2rem 0 0;
  }
}
</style>
