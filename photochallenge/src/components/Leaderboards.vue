<script>
import Leaderboard from "@/components/Leaderboard.vue";


export default {
  name: 'Leaderboards',
  components: {Leaderboard},
  props: ['specialStrings'],
  data: function () {
    return {
      isLoaded: false,
      leaderboardItems: [],
      headers: [],
      image: null,
      noImage: true,
      winner: 'error',
      title: "error",
      isSpecial: false,
      activePage: 0,
      componentKey: 0,
      targetDate: '2025-01-15T12:00:00',
      countdown: {days: 0, hours: 0, minutes: 0, seconds: 0,},
    }
  },
  mounted: function () {
    // start the clock
    this.startCountdown();

    // load the initial leaderboard
    this.changePage(0);
  },
  methods: {
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
    // change the active leaderboard and request the required information
    changePage(page) {
      //clear the data
      this.leaderboardItems = []
      this.image = null
      this.noImage = true
      this.isLoaded = false;

      //change the active tab
      const deleteElements = document.querySelectorAll('.tab');
      deleteElements[this.activePage].classList.remove('is-active');
      this.activePage = page;
      deleteElements[this.activePage].classList.add('is-active');

      if (this.activePage === 0) {
        this.isSpecial = false;
        this.title = "Main Challenge";
        this.headers = ['Position', 'Name', 'Challenge Count'];
        this.leaderboardItems = [];
        this.fetchData(this.$serverAddress + "leaderboard")
            .then((res) => {
              console.log(res);
              for (let i = 0; i < res.length; i++) {
                this.leaderboardItems.push({
                  position: res[i].position,
                  name: res[i].name,
                  challengeCount: res[i].challengeCount,
                });
              }
              this.isLoaded = true;
              this.noImage = false;
              console.log(this.leaderboardItems);
              this.forceRerender();
            })
            .catch((err) => {
              console.log(err);
              this.isLoaded = true;
              this.noImage = true;
              this.forceRerender();
            })

      } else {
        this.isSpecial = true;
        this.title = this.specialStrings[this.activePage - 1];
        this.headers = ['Position', 'Name', 'Votes', 'Voters'];
        this.leaderboardItems = [];
        this.fetchData(this.$serverAddress + "leaderboard/Special/" + (this.activePage - 1).toString())
            .then((res) => {
              console.log(res);
              for (let i = 0; i < res.length; i++) {
                this.leaderboardItems.push({
                  position: res[i].position,
                  name: res[i].name,
                  voters: res[i].voters,
                  votes: res[i].votes
                });
              }
              this.fetchData(this.$serverAddress + "Special/Id/" + res[0].id)
                  .then((res) => {
                    console.log(res);
                    this.winner = res.name;
                    this.image = "data:image/png;base64," + res.image;
                    this.isLoaded = true;
                    this.noImage = false;
                    this.forceRerender();
                    console.log(this.leaderboardItems);
                  })
                  .catch((err) => {
                    console.log(err);
                    this.noImage = true;
                    this.isLoaded = true;
                  })
            })
            .catch((err) => {
              console.log(err);
              this.noImage = true;
              this.isLoaded = true;
            })
      }
    },

    // start the countdown
    startCountdown() {
      this.updateCountdown();
      setInterval(this.updateCountdown, 1000);
    },

    //update the countdown
    updateCountdown() {
      const now = new Date().getTime();
      const target = new Date(this.targetDate).getTime();
      const distance = target - now;
      if (distance > 0) {
        this.countdown.days = Math.floor(distance / (1000 * 60 * 60 * 24));
        this.countdown.hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        this.countdown.minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        this.countdown.seconds = Math.floor((distance % (1000 * 60)) / 1000);
      } else {
        this.countdown = {days: 0, hours: 0, minutes: 0, seconds: 0,};
      }
    },

    // rerender the leaderboard
    forceRerender() {
      this.componentKey += 1;
    },
  }
}
</script>

<template>
  <div class="section">
    <div class="content">
      <h1>Leaderboard</h1>
      Countdown to Competition Close on 15th Jan Midday:
      <p>{{ countdown.days }} days, {{ countdown.hours }} hours, {{ countdown.minutes }} minutes, and {{
          countdown.seconds
        }} seconds</p>
    </div>


    <div class="tabs">
      <ul>
        <li class="tab is-active is-success" @click="changePage(0)"><a>Main Challenge</a></li>
        <li class="tab" v-for="(string, index) in this.specialStrings"><a
            @click="changePage(index+1)">{{ string }}</a></li>
      </ul>
    </div>
    <div v-if="noImage">
      No images submitted yet.
    </div>
    <div v-else-if="isLoaded">
      <Leaderboard :title="this.title" :isSpecial="this.isSpecial" :key="this.componentKey"
                   :items="this.leaderboardItems"
                   :headers="this.headers" :image="this.image" :winner="this.winner"/>
    </div>
    <div v-else>
      Loading...
    </div>
  </div>
</template>

<style scoped>

</style>