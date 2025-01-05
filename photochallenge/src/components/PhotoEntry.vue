<script>
import Submitter from "@/components/Submitter.vue";

export default {

  name: 'PhotoViewer',
  components: {Submitter},
  props: ['strings', 'specialStrings'],
  methods: {
    changePage(page){
      const deleteElements = document.querySelectorAll('.tab');
      deleteElements[this.activePage].classList.remove('is-active');
      this.activePage = page;
      deleteElements[this.activePage].classList.add('is-active');
    }
  },
  data() {
    return {
      componentKey: 0,
      activePage: 0
    };
  },
}
</script>

<template>
  <div class="section">
    <div class="content">
      <h1 class="title">Submit Photos</h1>
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
        <li>Highest Jump.</li>
      </ul>
    </div>

    <div class="tabs">
      <ul>
        <li class="tab is-active is-success" @click="changePage(0)"><a>Highest Scorer</a></li>
        <li class="tab"><a @click="changePage(1)">Special Prizes</a></li>
      </ul>
    </div>

    <Submitter v-if="activePage === 0" :strings="strings" :special="false"/>
    <Submitter v-else :strings="specialStrings" :special="true"/>

  </div>


</template>