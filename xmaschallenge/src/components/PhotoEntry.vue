<script>
import axios from 'axios'

export default {

  name: 'PhotoViewer',
  props: ['strings'],
  // setup(props) {
  //   console.log(props.strings);
  // },
  methods: {
    fileStringUpdate() {
      document.getElementById('selectedFileString').innerText = document.getElementById('image').files[0].name;
    },
    goToAbout() {
      this.$router.push('/view')
    },
    async submit() {

      document.getElementById('failNotify').style.display = "none";
      document.getElementById('successNotify').style.display = "none";

      const form = document.getElementById('photoEntryForm')
      const bar = document.getElementById('progressBar');
      bar.value = "10";

      form.addEventListener('submit', (e) => {

        bar.value = "20";
        //to prevent reload 
        e.preventDefault();
        //creates a multipart/form-data object 
        let data = new FormData(form);
        bar.value = "30";

        axios({
          method: "post",
          url: "http://localhost:3000/api/xmasapi/imagesubmit",
          data: data,
          withCredentials: false,
        })
          .then((res) => {

            bar.value = "100";
            document.getElementById('successNotify').style.display = "block";
            console.log(res);
          })
          .catch((err) => {
            bar.value = "100";
            document.getElementById('failNotify').style.display = "block";
            throw err;
          });
      });


    }
  },
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
  <div class="section">
    <div class="content">
      <h1 class="title">Submit your Photo</h1>
      <h4>
        The Rules:
      </h4>
      <ol>
        <li>All tasks MUST be accompanied by an image.</li>
        <li>You may not submit two images to the same task, the most recent submission will be taken.</li>
        <li>The scoring will be one point per task.</li>
      </ol>
      <h4>Prizes:</h4>
      <ul>
        <li>Highest Scorer.</li>
        <li>Best Image Overall.</li>
        <li>Highest Jump (TASK 24)</li>
      </ul>
    </div>

    <form class="form" id="photoEntryForm">

      <div class="field">
        <label class="label" for="fname">Name:</label>
        <div class="control">
          <input class="input is-danger" placeholder="Your Name" type="text" id="name" name="name">
        </div>
      </div>

      <div class="field">
        <label class="label" for="lname">Challenge Number:</label>
        <div class="control">
          <div class="select is-danger">
            <select id="challenge" name="challenge">
              <option v-for="(challenge, index) in strings" :value="index">
                {{ index + 1 }} - {{ challenge }}
              </option>
            </select>
          </div>
        </div>
      </div>

      <label class="label" for="imageUpload">Choose an image to upload:</label>
      <div class="file has-name">
        <label class="file-label">
          <input class="file-input" type="file" id="image" name="file" @change="fileStringUpdate" />
          <span class="file-cta">
            <span class="file-icon">
              <i class="fas fa-upload"></i>
            </span>
            <span class="file-label"> Choose an image… </span>
          </span>
          <span id="selectedFileString" class="file-name"> Please choose a file... </span>
        </label>
      </div>

      <div class="control">
        <button class="button is-danger" value="save" @click="submit">Submit</button>
      </div>

    </form>

    <div class="block" />
    <progress class="progress is-danger" id="progressBar" value="0" max="100">
    </progress>

    <div class="block" id="failNotify" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-danger">
          <button class="delete"></button>
          Photo didn't submit properly. Please check your connection and that you filled out the whole form.
        </div>
      </div>
    </div>
    <div class="block" id="successNotify" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-success">
          <button class="delete"></button>
          Photo submitted successfully.
        </div>
      </div>
    </div>

  </div>


</template>

<style scoped>
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
}

h3 {
  font-size: 1.2rem;
}

.greetings h1,
.greetings h3 {
  text-align: center;
}

@media (min-width: 1024px) {

  .greetings h1,
  .greetings h3 {
    text-align: left;
  }
}
</style>
