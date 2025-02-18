import {createMemoryHistory, createRouter} from 'vue-router'

import PhotoEntry from './components/PhotoEntry.vue'
import PhotoViewer from './components/PhotoViewer.vue'
import HostArea from './components/HostArea.vue';
import HostLogin from './components/HostLogin.vue';
import Leaderboard from "@/components/Leaderboards.vue";
import Voting from "@/components/Voting.vue";
import MainPage from "@/components/MainPage.vue";
import MainLogin from "@/components/MainLogin.vue";
import photoEntry from "@/components/PhotoEntry.vue";

const specialStrings = new Array("Best photo", "Highest jump");

const challengeStrings = new Array("Take a picture with a principal engineer",
    "Wear on piece of a non-players clothing",
    "Take a picture standing on a table",
    "Drink a Water Round",
    "Re-enact a panel from The Man Hotter than the Sun™",
    "Show somebody the Bawdy Jug™",
    "Do a vault in the castle vaults (jump over something)",
    "Tap a can in the tap and can (no can can be tapped twice)",
    "Shake hands with a Salopian in the Salopian (the Salopian can't be player)",
    "Get a beer with lots of head in the Kings head",
    "Be a puritan in Cromwell's tap house (Don't laugh for the whole time in the pub)",
    "Take a picture with a farmer in the house of grain (the farmer can't be player)",
    "Balance a spoon on your face at either Wetherspoon",
    "Gallop like a horse in the Nag's head",
    "Swap a shoe with a non player",
    "Host a dance off with a non player",
    "Spell out Nidec with poses across 5 different pubs (HOTTOGO/YMCA Style)",
    "Steal non players hat and take a picture wearing it",
    "Do a head or hand stand",
    "Do a shoey with a Fosters, Aussie style.",
    "Take a picture with a Kebab Man (only if he called you bossman)",
    "Drink a drink with no hands",
    "Be slapped by a non-player",
    "A super cute picture");


const routes = [
    {path: '/', name: 'Login', component: MainLogin},
    {
        path: '/main', component: MainPage, children: [
            {
                path: '',
                component: PhotoEntry,
                props: {strings: challengeStrings, specialStrings: specialStrings},
            },

            {
                path: 'view',
                component: PhotoViewer,
                props: {strings: challengeStrings},
            },
            {
                path: 'host',
                component: HostArea,
                props: {strings: challengeStrings},
            },
            {
                path: 'hostlogin',
                component: HostLogin,
            },
            {
                path: 'leaderboard',
                component: Leaderboard,
                props: {specialStrings: specialStrings},
            },
            {
                path: 'voting',
                component: Voting,
                props: {specialStrings: specialStrings},
            }]
    }];

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

export default router