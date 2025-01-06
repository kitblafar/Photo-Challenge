import {createApp, reactive} from 'vue'
import router from './router.js'
import App from './App.vue'

import Particles from "@tsparticles/vue3";
import {loadSlim} from "@tsparticles/slim";
import "@/assets/style.scss";

import axios from 'axios'


const app = createApp(App);

app.config.globalProperties.$axios = axios;
app.config.globalProperties.$serverAddress = 'http://localhost:3000/api/photochallengeapi/';
app.config.globalProperties.$axiosPermitted = reactive({
    axiosPermitted: true
});


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
        console.log("Axios turned on:");
        console.log(app.config.globalProperties.$axiosPermitted.axiosPermitted);
        console.log('checking request');
        console.log(config);
        // Check the request method
        if (!app.config.globalProperties.$axiosPermitted.axiosPermitted) {
            return Promise.reject(new Error(`Requests not allowed in demo mode`));
        }

        return config;
    }.bind(this),
    function (error) {
        return Promise.reject(error);
    }
);