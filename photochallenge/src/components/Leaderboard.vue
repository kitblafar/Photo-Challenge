<script>
import ImageDisplay from "@/components/ImageDisplay.vue";

export default {
  name: 'Leaderboard',
  props: ['title', 'headers', 'items', 'isSpecial', 'image', 'winner'],
}
</script>

<template>
  <div class="content">
    <div v-if="isSpecial">
      <h3 class="subtitle">The winning: {{this.title}}</h3>
      <h4 class="subtitle">from {{winner}} </h4>
      <div>
        <img v-bind:src="image" />
      </div>
    </div>
    <div class="table-container">
      <table class="table is-fullwidth is-striped is-hoverable">
        <thead>
        <tr>
          <th v-for="header in this.headers">{{ header }}</th>
        </tr>
        </thead>
        <tbody>
        <tr v-if="this.isSpecial" v-for="item in this.items" >
          <td>{{ item.position + 1}}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.votes }}</td>
          <td>{{ item.voters }}</td>
        </tr>
        <tr v-else v-for="item in this.items"  >
          <td>{{ item.position + 1 }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.challengeCount }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>

</style>