import {createApp} from 'vue'
import router from './router.js'
import App from './App.vue'

import Particles from "@tsparticles/vue3";
import {loadSlim} from "@tsparticles/slim";
import "@/assets/style.scss";

import axios from 'axios'


const app = createApp(App);

app.config.globalProperties.$axios = axios;
app.config.globalProperties.$serverAddress = 'http://localhost:3000/api/photochallengeapi/';
app.config.globalProperties.$axiosPermitted = true;


app.use(Particles, {
    init: async engine => {
        // await loadFull(engine); // you can load the full tsParticles library from "tsparticles" if you need it
        await loadSlim(engine); // or you can load the slim version from "@tsparticles/slim" if don't need Shapes or Animations
    }
})
app.use(router)
app.mount('#app');


axios.interceptors.request.use(
    function (config) {
        console.log('checking request');
        console.log(app.config.globalProperties.$axiosPermitted);
        // Check the request method
        if (app.config.globalProperties.$axiosPermitted === false) {
            return Promise.reject(new Error(`Request method ${config.method} is not allowed.`));
        }

        return config;
    }.bind(this),
    function (error) {
        return Promise.reject(error);
    }
);