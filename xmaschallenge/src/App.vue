<script>

// delete button pressed
document.addEventListener('DOMContentLoaded', () => {
  (document.querySelectorAll('.notification .delete') || []).forEach(($delete) => {
    const $notification = $delete.parentNode;

    $delete.addEventListener('click', () => {
      $notification.parentNode.removeChild($notification);
    });
  });
});
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
      <nav class="navbar is-success" role="navigation" aria-label="main navigation">

        <div class="navbar-brand">
          <a class="navbar-item" href="https://kittyfarren.dev/xmaschallenge">
            <img width="30" height="300" fill="none" src="./assets/logo.svg"></img>
          </a>
          <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false"
            data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
          </a>
        </div>

        <div class="navbar-menu">
          <RouterLink to="/" class="navbar-item">Submit Your Photo</RouterLink>
          <RouterLink to="/view" class="navbar-item">View Your Photos</RouterLink>
        </div>
      </nav>
    </section>
  </header>

  <main>
    <RouterView />
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
