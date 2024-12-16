import { createMemoryHistory, createRouter } from 'vue-router'

import PhotoEntry from './components/PhotoEntry.vue'
import PhotoView from './components/PhotoViewer.vue'
// import PrizeView from 'components/PhotoViewer.vue'

const routes = [
    { path: '/', component: PhotoEntry },
    { path: '/view', component: PhotoView },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

export default router