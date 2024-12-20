<script setup>
import carousel from './Carousel.vue'

</script>

<script>
import axios from 'axios'

const forceRerender = () => {
  componentKey.value += 1;
};

var images = [];

for (let i = 1; i < 25; i++) {
  images.push({
    id: i, // the ID is the challenge
    big: 'src/assets/logo.svg',
    thumb: 'src/assets/logo.svg',
  })
}

export default {
  props: ['strings'],
  data() {
    return {
      componentKey: 0,
    };
  },
  methods: {
    forceRerender() {
      this.componentKey += 1;
    },
    getPhotos() {
      console.log("getting some images from ", document.getElementById('yourName').value);
      var url = "http://localhost:3000/api/xmasapi/" + String(document.getElementById('yourName').value);

      axios({
        method: "get",
        url: url,
        withCredentials: false,
      })
        .then((res) => {
          console.log("logging");
          console.log(res);

          // set the images for each of the challenges you received back
          for (let i = 0; i < res.data.length; i++) {
            console.log("Challenge number: " + res.data[i].challenge);
            images[res.data[i].challenge - 1].big = "data:image/png;base64," + res.data[i].image;
            images[res.data[i].challenge - 1].thumb = "data:image/png;base64," + res.data[i].image;
          }
          forceRerender;
        })
        .catch((err) => {
          document.getElementById('failNotify').style.display = "block";
          throw err;
        });
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
  }" />
  <h1 class="title">View Your Photos</h1>


  <div class="field">
    <label class="label" for="fname">Name:</label>
    <div class="control">
      <input class="input is-danger" placeholder="Your Name" type="text" id="yourName">
    </div>
  </div>
  <div class="control">
    <button class="button is-danger" value="save" @click="getPhotos">Get Photos</button>
  </div>

  <carousel :key="componentKey" :images="images" :strings="strings"/>

  <div class="block" id="failNotify" style="display:none">
    <div class="block ">
    </div>
    <div class="block">
      <div class="notification is-danger">
        <button class="delete"></button>
        Could not load photos. Did you type your name correctly?
      </div>
    </div>
  </div>

</template>

<style scoped></style>