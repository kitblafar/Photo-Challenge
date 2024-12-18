<script>
import axios from 'axios'
import bulmaCarousel from "bulma-carousel/src/js/index.js";

bulmaCarousel.attach('#carousel-demo', {
    slidesToScroll: 1,
    slidesToShow: 4
});

export default {
    methods: {
        getPhotos() {
        }
    }
}

</script>

<template>
  <section class="section">
    <div class="container">
      <!-- Start Carousel -->
      <div id="carousel-demo" class="carousel">
        <div class="item-1">
HI        </div>
        <div class="item-2">
HI        </div>
        <div class="item-3">
          <!-- Slide Content -->
        </div>
      </div>
      <!-- End Carousel -->
    </div>
  </section>

</template>

<style scoped></style>