<script setup>
import carousel from './Carousel.vue'
</script>

<script>
let images = [];
let approvals = [];
let messages = [];

for (let i = 1; i < 25; i++) {
  images.push({
    id: i, // the ID is the challenge
    big: 'src/assets/logo.svg',
    thumb: 'src/assets/logo.svg',
  })
  approvals.push(false);
  messages.push("Image not submitted for this challenge");
}

export default {
  props: ['strings'],
  data() {
    return {
      componentKey: 0,
    };
  },
  methods: {
    deleteClick() {
      const deleteElements = document.querySelectorAll('.notification');
      deleteElements.forEach(element => {
        element.style.display = 'none'
      });
    },
    forceRerender() {
      this.componentKey += 1;
    },
    async getPhotos() {
      console.log("getting some images from ", document.getElementById('yourName').value);

      await this.$axios({
        method: "get",
        url: this.$serverAddress + String(document.getElementById('yourName').value),
        withCredentials: false,
      })
          .then((res) => {
            console.log(res);

            // set the images for each of the challenges you received back
            for (let i = 0; i < res.data.length; i++) {
              console.log("Challenge number: " + res.data[i].challenge);
              images[res.data[i].challenge].big = "data:image/png;base64," + res.data[i].image;
              images[res.data[i].challenge].thumb = "data:image/png;base64," + res.data[i].image;
              if (res.data[i].approved !== null) {
                approvals[res.data[i].challenge] = res.data[i].approved;
                messages[res.data[i].challenge] = res.data[i].message;
              } else {
                messages[res.data[i].challenge] = "Approval Pending";
              }
            }
            this.forceRerender();
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
  <div class="section">
    <h1 class="title">View Your Photos</h1>


    <div class="field">
      <label class="label" >Name:</label>
      <div class="control">
        <input class="input is-danger" placeholder="Your Name" type="text" id="yourName">
      </div>
    </div>
    <div class="control">
      <button class="button is-danger" value="save" @click="getPhotos">Get Photos</button>
    </div>

    <carousel :key="componentKey" :images="images" :strings="strings" :approvals="approvals" :messages="messages" :isView="true"/>

    <div class="block" id="failNotify" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-danger">
          <button class="delete" @click="deleteClick"></button>
          Could not load photos. Did you type your name correctly?
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped></style>