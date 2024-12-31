<template>
    <div class="card-carousel">
        <div class="card-img">
            <h2 class="title" style="text-align: center;">Challenge #{{ (this.activeImage + 1).toString() }}</h2>
            <h4 class="subtitle" style="text-align: center;">Brief: {{ strings[this.activeImage] }}</h4>
            <h4 class="subtitle" v-if="approvals[this.activeImage]" style="text-align: center;">This image is approved.</h4>
            <h4 class="subtitle" v-else style="text-align: center;">This image is was not approved with message "{{messages[this.activeImage]}}".</h4>

                <img :src="currentImage" alt="Challenge #{{this.activeImage.toString()}} image.">
                <div class="actions">
                    <span @click="prevImage" class="prev">
                        &#8249;
                    </span>
                    <span @click="nextImage" class="next">
                        &#8250;
                    </span>
                </div>
        </div>
        <div class="thumbnails">
            <div v-for="(image, index) in images" :key="image.id"
                :class="['thumbnail-image', (activeImage == index) ? 'active' : '']" @click="activateImage(index)">
                <img :src="image.thumb" >
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'Carousel',
    props: ['images', 'strings', 'approvals', 'messages'],
    data() {
        return {
            //Index of the active image
            activeImage: 0
        }
    },
    computed: {
        // currentImage gets called whenever activeImage changes
        // and is the reason why we don't have to worry about the
        // big image getting updated
        currentImage() {
            return this.images[this.activeImage].big;
        }
    },
    methods: {
        // Go forward on the images array
        // or go at the first image if you can't go forward
        nextImage() {
            var active = this.activeImage + 1;
            if (active >= this.images.length) {
                active = 0;
            }
            this.activateImage(active);
        },
        // Go backwards on the images array
        // or go at the last image
        prevImage() {
            var active = this.activeImage - 1;
            if (active < 0) {
                active = this.images.length - 1;
            }
            this.activateImage(active);
        },
        activateImage(imageIndex) {
            this.activeImage = imageIndex;
        }
    }
}
</script>


<style scoped>
.card-carousel {
    user-select: none;
    position: relative;
}


.thumbnails {
    display: flex;
    justify-content: space-evenly;
    flex-direction: row;
}

.thumbnail-image {
    display: flex;
    align-items: center;
    width: auto;
    cursor: pointer;
    padding: 2px;
}

.thumbnail-image>img {
    width: 100%;
    height: auto;
    transition: all 250ms;
}

.thumbnail-image:hover>img,
.thumbnail-image.active>img {
    opacity: 0.6;
    box-shadow: 2px 2px 6px 1px rgba(0, 0, 0, 0.5);
}

.card-img {
    position: relative;
    margin-bottom: 15px;
}

.card-img>img {
    display: block;
    margin: 0 auto;
}

.actions {
    font-size: 1.5em;
    height: 40px;
    position: absolute;
    top: 50%;
    margin-top: -20px;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    color: #585858;
}

.actions>span {
    cursor: pointer;
    transition: all 250ms;
    font-size: 45px;
}

.actions>span.prev {
    margin-left: 5px;
}

.actions>span.next {
    margin-right: 5px;
}

.actions>span:hover {
    color: #eee;
}
</style>
