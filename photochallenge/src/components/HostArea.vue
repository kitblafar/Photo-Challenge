<script>
import axios from 'axios'
import ImageDisplay from "@/components/ImageDisplay.vue";
import {nextTick} from "vue";


// change the props of the approval image to see a different image
export default {
  name: 'HostArea',
  components: {ImageDisplay},
  props: ['strings'],
  data: function () {
    return {
      //Index of the active image
      activeIndex: 0,
      hostImages: [],
      hostImageInfo: [],
      componentKey: 0,
      message: "",
      isLoaded: false,
    }
  },

  mounted: function () {
    this.hostImageInfo = [];
    this.hostImages = [];
    let url = this.$serverAddress + "unapproved";
    this.fetchData(url)
        .then((res) => {
          console.log(res);
          console.log("Number of images to review: " + res.length.toString());
          for (let i = 0; i < res.length; i++) {
            // add an entry to the patch data records
            this.hostImageInfo.push({
              index: i,
              id: res[i].id,
              name: res[i].name,
              challenge: res[i].challenge
            });

            this.hostImages.push("data:image/png;base64," + res[i].image);
            // console.log("First data point name");
            // console.log(this.hostImageInfo[0].name);
            // console.log("Number of data points: " + this.hostImageInfo.length);
          }
          if (this.hostImageInfo.length === 0) {
            this.hostImageInfo.push({
              index: 0,
              id: 0,
              name: 'success',
              challenge: 0
            });
            this.hostImages.push("./src/assets/logo.svg");
          }
          this.isLoaded = true;
          this.loaded();
        })
        .catch((err) => {
          console.log("Error");
          this.hostImageInfo.push({
            index: 0,
            id: 0,
            name: 'error',
            challenge: 0
          });
          this.isLoaded = true;
          // this.loaded();
          throw err;
        });

  },
  methods: {
    loaded() {
      nextTick(() => {
        console.log("got all the data");

        document.getElementById('successNotify').style.display = 'none';
        document.getElementById('failNotifyLoad').style.display = 'none';
        document.getElementById('failNotifyDisapprove').style.display = 'none';
        document.getElementById('viewing').style.display = 'block';

        console.log("the success message is: ", this.hostImageInfo[0]);

        if (this.hostImageInfo[0].name === 'success') {
          document.getElementById('successNotify').style.display = 'block';
          document.getElementById('viewing').style.display = 'none';
        } else if (this.hostImageInfo[0].name === 'error') {
          document.getElementById('failNotifyLoad').style.display = 'block';
          document.getElementById('viewing').style.display = 'none';
        }
      });
    },
    async fetchData(url) {
      try {
        const response = await axios.get(url);
        return response.data;
      } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
      }
    },
    removePagination() {
      // Index of the element to remove
      let indexToRemove = this.activeIndex;
      console.log("removing image " + indexToRemove);
      console.log("total image number " + this.hostImages.length);

      if (this.hostImages.length === 1) {
        document.getElementById('successNotify').style.display = "block";
        document.getElementById('viewing').style.display = "none";
        return;
      }
      // Remove the element at the specified index
      if (indexToRemove > -1 && indexToRemove < this.hostImages.length) {
        this.hostImages.splice(indexToRemove, 1);
        this.hostImageInfo.splice(indexToRemove, 1);
      }
      this.forceRerender();
    },
    updateData(approval, message) {
      axios({
        method: "patch",
        url: this.$serverAddress + this.hostImageInfo[this.activeIndex].id,
        data: {
          id: this.hostImageInfo[this.activeIndex].id,
          approved: approval,
          message: message,
        },

      })
          .then((res) => {
            console.log(res);
            this.removePagination();
          })
          .catch((err) => {
            console.log("Error: " + err.message);
            throw err;
          });
    },
    approve(approval) {
      console.log("approving");
      document.getElementById('failNotifyDisapprove').style.display = "none";
      if (!approval) {
        if (this.message !== "") {
          this.updateData(false, this.message);
        } else {
          document.getElementById('failNotifyDisapprove').style.display = "block";
        }
      } else {
        this.updateData(true, "");
      }
      this.message = "";
    },
    deleteClick(){
      const deleteElements = document.querySelectorAll('.notification');
      deleteElements.forEach(element => {element.style.display = 'none'});
    },
    changePage(index) {
      this.activeIndex = index;
      this.forceRerender();
    },
    forceRerender() {
      this.componentKey += 1;
    },
  }

}

</script>
<template>
  <div class="section">
  <div v-if="isLoaded">
<!--   MAIN WINDOW-->
    <h1 class="title">Host Area</h1>
    <div id="viewing" style="display: block">
      <nav class="pagination" role="navigation" aria-label="pagination">
        <a href="#" class="pagination-previous">Previous</a>
        <a href="#" class="pagination-next">Next</a>
        <ul class="pagination-list">
          <li v-for="item in this.hostImageInfo">
            <button @click="this.changePage(item.index)" id="{{item.index}}" class="pagination-link">{{
                item.index
              }}
            </button>
          </li>
        </ul>
      </nav>

      <!-- The image display -->
      <ImageDisplay id="imageToDisplay"
                    :key="this.componentKey"
                    :image="this.hostImages[this.activeIndex]"
                    :name="this.hostImageInfo[this.activeIndex].name"
                    :challengeNumber="this.hostImageInfo[this.activeIndex].challenge"
                    :challengeString="this.strings[this.hostImageInfo[this.activeIndex].challenge]"/>

      <div class="field">
        <label class="label" for="fname">Disapproval reason:</label>
        <div class="control">
          <input class="input is-danger" placeholder="I didn't like it" v-model="message" type="text" id="disapproval">
        </div>
      </div>

      <button class="button is-success" @click="approve(true)">Approved</button>
      <button class="button is-danger" @click="approve(false)">Not Approved</button>
    </div>

<!--    NOTIFICATIONS-->
    <div class="block" id="failNotifyDisapprove" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-danger">
          <button class="delete" @click="deleteClick"></button>
          You must give a reason if you dissapprove.
        </div>
      </div>
    </div>
    <div class="block" id="failNotifyLoad" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-danger">
          <button class="delete" @click="deleteClick"></button>
          Images not loaded successfully from database.
        </div>
      </div>
    </div>
    <div class="block" id="successNotify" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-success">
          All photos approved/ submitted.
        </div>
      </div>
    </div>
  </div>
  <div v-else><p>Loading...</p></div>
  </div>
</template>

<style scoped></style>
