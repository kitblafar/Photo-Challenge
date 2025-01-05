<script>
import Carousel from "@/components/Carousel.vue";


export default {
  name: 'Voting',
  components: {Carousel},
  props: ['specialStrings'],
  data() {
    return {
      images: [],
      isLoaded: false,
      noImages: false,
      componentKey: 0,
      currentImageIndex: 0,
      activePage: 0,
      names: [],
      ids: [],
      voterName :'',
    }
  },
  methods: {
    updateImage(data){
      this.currentImageIndex = data;
    },
    // change the active leaderboard and request the required information
    changePage(page) {
      console.log("changing page", page);
      const deleteElements = document.querySelectorAll('.tab');
      deleteElements[this.activePage].classList.remove('is-active');
      this.images = [];
      this.names = [];
      this.ids = [];
      this.noImages = true;
      this.activePage = page;
      deleteElements[this.activePage].classList.add('is-active');
      this.isLoaded = false;
      this.fetchData(this.$serverAddress + "special/challenge/" + this.activePage.toString())
          .then((res) => {
            for (let i = 0; i < res.length; i++) {
              this.names.push(res[i].name);
              this.ids.push(res[i].id);
              this.images.push({
                id: i,
                big: "data:image/png;base64," + res[i].image,
                thumb: "data:image/png;base64," + res[i].image
              })
            }
            if (res.length === 0){
              this.noImages = true;
              this.names.push("No images submitted yet");
              this.ids.push(0);
              this.images.push({
                id: 0,
                big: "src/assets/santa.svg",
                thumb: "src/assets/santa.svg"
              })
            }
            console.log(this.names);
            this.isLoaded = true;
            this.forceRerender();
          })
          .catch((err) => {
            console.log(err);
            this.noImages = true;
            this.forceRerender();
          })
    },
    // get the axios promise
    async fetchData(url) {
      try {
        const response = await this.$axios.get(url);
        return response.data;
      } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
      }
    },
    vote() {
      console.log("all the ids");
      console.log(this.ids);
      let url = this.$serverAddress + "Special/" + this.ids[this.currentImageIndex];
      this.$axios({
        method: "patch",
        url: url,
        data: {
          id: this.ids[this.currentImageIndex],
          name: this.voterName
        },
        withCredentials: false,
      })
          .then((res) => {
            document.getElementById('successNotify').style.display = "block";
            console.log(res);
          })
          .catch((err) => {
            document.getElementById('failNotify').style.display = "block";
            throw err;
          });
    },
    // rerender the leaderboard
    forceRerender() {
      this.componentKey += 1;
    },
    deleteClick() {
      const deleteElements = document.querySelectorAll('.notification');
      deleteElements.forEach(element => {
        element.style.display = 'none'
      });
    },
  }
}
</script>

<template>
  <div class="section">
    <div class="content">
      <h1>Voting</h1>
      <p>Vote for the winner in each photo category. Enter your name to vote. If you sneakily vote twice (under
        different names) or take someone else's name, this will be evident in the leaderboard and you will be subject to
        mob justice.</p>
    </div>

    <div class="tabs">
      <ul>
        <li class="tab" v-for="(string, index) in this.specialStrings"><a
            @click="changePage(index)">{{ string }}</a></li>
      </ul>
    </div>
    <div v-if="noImages">
      No images submitted yet.
    </div>
    <div v-else-if="isLoaded">
      <div class="field">
        <label class="label">Name:</label>
        <div class="control">
          <input class="input is-danger" placeholder="Name" v-model="this.voterName" type="text" id="voterName">
        </div>
      </div>

      <button style="text-align: center" class="button is-success" @click="vote">Vote for this Image</button>
      <div class="block" id="successNotify" style="display:none">
        <div class="block ">
        </div>
        <div class="block">
          <div class="notification is-success">
            <button class="delete" @click="deleteClick"></button>
            Successfully voted!
          </div>
        </div>
      </div>
      <div class="block" id="failNotify" style="display:none">
        <div class="block ">
        </div>
        <div class="block">
          <div class="notification is-danger">
            <button class="delete" @click="deleteClick"></button>
            Vote failed!
          </div>
        </div>
      </div>

      <Carousel id="imageViewer" @update:data="updateImage" :key="this.componentKey" :images='this.images' :strings='this.names'
                :approvals="[]" :messages="[]" isView='false'/>
    </div>
    <div v-else>
      Loading...
    </div>
  </div>
</template>

<style scoped>

</style>