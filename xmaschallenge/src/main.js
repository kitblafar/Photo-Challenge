import { createApp } from 'vue'
import router from './router.js'
import App from './App.vue'

import "@/assets/style.scss";
import "/node_modules/bulma-carousel/src/sass/index.sass"


createApp(App)
    .use(router)
    .mount('#app')