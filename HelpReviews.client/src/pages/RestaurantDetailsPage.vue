<template>
  <div class="container">
    <div v-if="restaurant" class="row bg-light elevation-5">
      <h2 class="col-12">
        {{ restaurant.name }}
      </h2>
      <img :src="restaurant.imgUrl" alt="" class="col-12 cover-img">
      <div class="col-12">

        <p>{{ restaurant.description }}</p>

        <section class="row">
          <div class="col-6">
            <i class="mdi mdi-human">Visits {{ restaurant.visits }}</i>
            <i class="mdi mdi-note">Reports {{ restaurant.reportCount }}</i>
          </div>
          <div class="col-6">
            <button @click="toggleShutdownStatus" v-if="!restaurant.isShutdown && isOwner" class="btn btn-warning">Shutdown <i class="mdi mdi-cancel"></i></button>
            <button @click="toggleShutdownStatus" v-if="restaurant.isShutdown && isOwner" class="btn btn-success">Re-open <i class="mdi mdi-door-open"></i></button>
            <button v-if="isOwner" class="btn btn-danger">Delete <i class="mdi mdi-delete-forever"></i></button>
          </div>
        </section>


      </div>
    </div>
    <section class="row my-4">

      <div v-for="report in reports" :key="report.id" class="col-12 bg-light elevation-5 p-2">
      <ReportCard :report="report"/>
      </div>

    </section>
  </div>
</template>


<script setup>
import { useRoute } from 'vue-router';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import {computed, onMounted} from 'vue'
import { logger } from '../utils/Logger.js';
import { router } from '../router.js';
import ReportCard from '../components/ReportCard.vue';

const route = useRoute()

const restaurant = computed(()=> AppState.activeRestaurant)
const account = computed(()=> AppState.account)
const isOwner = computed(()=> AppState.account.id == AppState.activeRestaurant?.creatorId)
const reports = computed(()=> AppState.reports)

async function getRestaurantDetails(){
  try {
    await restaurantsService.getRestaurantDetails(route.params.restaurantId)
  } catch (error) {
    router.push({name: 'Home'})
    logger.error(error)
    Pop.toast('no soup for you', 'error', 'center')
  }
}

async function getRestaurantReports(){
  try {
    await restaurantsService.getRestaurantReports(route.params.restaurantId)
  } catch (error) {
    Pop.error(error)
  }
}

async function toggleShutdownStatus(){
  try {
    let rest = AppState.activeRestaurant
    rest.isShutdown = !rest.isShutdown
    if(await Pop.confirm("This doesn't seem like a good idea, you will lose money", 'Bababooy', 'YEAH MAN I KNOW WHAT IM DOIN, AM DA BOSS',)){
      await restaurantsService.updateRestaurant(rest)
    }
  } catch (error) {
    Pop.error(error)
  }
}

onMounted(()=>{
getRestaurantDetails()
getRestaurantReports()
})

</script>


<style lang="scss" scoped>
.cover-img{
  height: 25dvh;
  object-fit: cover;
  object-position: center;
}
</style>
