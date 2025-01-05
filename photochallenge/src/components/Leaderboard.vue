<script>

export default {
  name: 'Leaderboard',
  props: ['title', 'headers', 'items', 'isSpecial', 'image', 'winner'],
}
</script>

<template>
  <div class="content">
    <div v-if="isSpecial">
      <h3 class="subtitle">The winning: {{ this.title }}</h3>
      <h4 class="subtitle">from {{ winner }} </h4>
      <div>
        <img class="large" v-bind:src="image" alt="winning image"/>
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
        <tr v-if="this.isSpecial" v-for="item in this.items">
          <td>{{ item.position + 1 }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.votes }}</td>
          <td>
            <div style="flex-direction: row; display: flex;">
            <div v-for="names in item.voters" style="width: fit-content; padding-right: 1em;">{{ names }}</div>
            </div>
          </td>
        </tr>
        <tr v-else v-for="item in this.items">
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