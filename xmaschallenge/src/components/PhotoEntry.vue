<script>
import axios from 'axios'

export default {
  methods: {
    fileStringUpdate(){
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
          url: "http://10.75.12.141:3000/api/xmasapi",
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
              <option value="1">1- Take a picture with a principal engineer</option>
              <option value="2">2- Wear on piece of a non-players clothing</option>
              <option value="3">3- Take a picture standing on a table</option>
              <option value="4">4- Drink a Water Round</option>
              <option value="4">5- Re-enact a panel from The Man Hotter than the Sun™</option>
              <option value="4">6- Show somebody the Bawdy Jug™</option>
              <option value="4">7- Do a vault in the castle vaults (jump over something)</option>
              <option value="4">8- Tap a can in the tap and can (no can can be tapped twice)</option>
              <option value="4">9- Shake hands with a Salopian in the Salopian (the Salopian can’t be player)</option>
              <option value="4">10- Get a beer with lots of head in the Kings head</option>
              <option value="4">11- Be a puritan in Cromwell’s tap house (Don’t laugh for the whole time in the pub)
              </option>
              <option value="4">12- Take a picture with a farmer in the house of grain (the farmer can’t be player)
              </option>
              <option value="4">13- Balance a spoon on your face at either Wetherspoon</option>
              <option value="4">14- Gallop like a horse in the Nag’s head</option>
              <option value="4">15- Swap a shoe with a non player</option>
              <option value="4">16- Host a dance off with a non player</option>
              <option value="4">17- Spell out Nidec with poses across 5 different pubs (HOTTOGO/YMCA Style)</option>
              <option value="4">18- Steal non players hat and take a picture wearing it</option>
              <option value="4">19- Do a head or hand stand</option>
              <option value="4">20- Do a shoey with a Fosters, Aussie style.</option>
              <option value="4">21- Take a picture with a Kebab Man (only if he called you bossman)</option>
              <option value="4">22- Drink a drink with no hands</option>
              <option value="4">23- Be slapped by a non-player</option>
              <option value="4">24- Do the highest jump</option>
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
