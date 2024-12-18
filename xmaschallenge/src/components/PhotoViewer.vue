<script>
import axios from 'axios'

import bulmaCarousel from 'bulma-carousel/dist/js/bulma-carousel.min.js';

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
            <div id="carousel-demo" class="carousel" slides-to-scroll="1">

                <div class="item-1">
                  hello
                </div>

                <div class="item-2">
                    hello2
                </div>
            </div>
            <!-- End Carousel -->
        </div>
    </section>

</template>

<style scoped></style>