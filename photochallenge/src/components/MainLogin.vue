<script>
import {sha256} from 'js-sha256';
import app from "@/App.vue";

export default {
  name: 'MainLogin',
  props: ['strings'],
  data() {
    return {
      input: {
        password: ""
      }
    }
  },
  methods: {
    seeDemo(){
      this.$axiosPermitted.axiosPermitted = false;
      console.log("Axios turned on:");
      console.log(this.$axiosPermitted.axiosPermitted);
      this.$router.push('/main');
    },
    async login() {
      console.log("Axios turned on:");
      console.log(this.$axiosPermitted.axiosPermitted);
      //make sure password not empty
      if (this.input.password !== "") {
        this.$axios({
          method: "post",
          url: this.$serverAddress+"authenticate",
          data:{password: sha256(this.input.password)},
          withCredentials: false,
        })
            .then((res) => {
              if (res.data === true) {
                console.log("authenticated");
                this.$axiosPermitted.axiosPermitted = true;
                console.log("Axios turned on:");
                console.log(this.$axiosPermitted.axiosPermitted);
                this.$router.push('/main');
              } else {
                document.getElementById('failLoginNotify').style.display = "block";
              }
            })
            .catch(() => {
              document.getElementById('failLoginNotify').style.display = "block";
            });

      } else {
        document.getElementById('failLoginNotify').style.display = "block";
        this.$router.push('/')
      }

    },
    close() {
      document.getElementById('failLoginNotify').style.display = "none";
    }
  }
}

</script>

<template>
  <div class="section">
    <h1 class="title">Enter Game Code</h1>
    <p>Either enter the game code or see the demo version. You will not be allowed to make any requests to the server in
      the demo mode for server security.</p>
    <div class="field">
      <label class="label">Password:</label>
      <div class="control">
        <input class="input is-danger" placeholder="Password" v-model="input.password" type="password" id="password"
               name="password">
      </div>
    </div>


    <div class="control" >
      <button class="button is-danger" value="save" @click="login">Submit</button>
      <div class="block" style="width: 1em"></div>
      <button class="button is-success" value="save" @click="seeDemo">See Demo</button>
    </div>

    <div class="block" id="failLoginNotify" style="display:none">
      <div class="block ">
      </div>
      <div class="block">
        <div class="notification is-danger">
          <button @click="close" class="delete"></button>
          Login Failed. Wrong host username or password.
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped></style>