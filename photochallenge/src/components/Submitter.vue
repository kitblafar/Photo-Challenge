<script>

export default {

  name: 'Submitter',
  props: ['strings', 'special'],
  methods: {
    handleFileUpdate(event) {
      document.getElementById('selectedFileString').innerText = document.getElementById('image').files[0].name;
      this.formData.image = event.target.files[0];
    },
    deleteClick() {
      const deleteElements = document.querySelectorAll('.notification');
      deleteElements.forEach(element => {
        element.style.display = 'none'
      });
    },
    submit() {
      console.log("Axios turned on:");
      console.log(this.$axiosPermitted.axiosPermitted);
      const bar = document.getElementById('progressBar');
      bar.value = "10";
      document.getElementById('failNotify').style.display = "none";
      document.getElementById('successNotify').style.display = "none";

      bar.value = "20";

      //creates a multipart/form-data object
      let data = new FormData();
      data.append('Challenge', this.formData.challenge);
      data.append('Name', this.formData.name);
      data.append('File', this.formData.image);

      bar.value = "30";
      let url = this.$serverAddress + "imagesubmit";

      if (this.special) {
        url = url + "/special";
      }

      this.$axios({
        method: "post",
        url: url,
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
    }
  },
  data() {
    return {formData: {challenge: 0, name: 'YourName', image: null}};
  },
}
</script>

<template>
  <form class="form" @submit.prevent="submit">

    <div class="field">
      <label class="label">Name:</label>
      <div class="control">
        <input class="input is-danger" placeholder="Your Name" type="text" v-model="formData.name">
      </div>
    </div>

    <div class="field">
      <label class="label">Challenge Number:</label>
      <div class="control">
        <div class="select is-danger">
          <select v-model="formData.challenge">
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
        <input class="file-input" type="file" id="image" @change="handleFileUpdate"/>
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
      <button class="button is-danger" value="save">Submit</button>
    </div>

  </form>

  <div class="block"/>
  <progress class="progress is-danger" id="progressBar" value="0" max="100">
  </progress>

  <div class="block" id="failNotify" style="display:none">
    <div class="block ">
    </div>
    <div class="block">
      <div class="notification is-danger">
        <button class="delete" @click="deleteClick"></button>
        Photo didn't submit properly. Please check your connection and that you filled out the whole form.
      </div>
    </div>
  </div>
  <div class="block" id="successNotify" style="display:none">
    <div class="block ">
    </div>
    <div class="block">
      <div class="notification is-success">
        <button class="delete" @click="deleteClick"></button>
        Photo submitted successfully.
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>