<template>
  <form @submit.prevent="createReport" class="row">
    <h3>Report a Restaurant</h3>
    <div class="col-12 mb-3">
      <section class="row">
        <div class="col-12">
          Which Restaurant are you reporting?
        </div>
        <div class="col-6">
          <img v-if="selectedRestaurant" :src="selectedRestaurant.imgUrl" class="restaurant-preview" alt="">
        </div>
        <div class="col-6">
          <select v-model="reportData.restaurantId" name="restaurant-picker" id="restaurant-picker" class="form-control">
            <option value="" disabled selected>please select a restaurant</option>
            <option v-for="restaurant in restaurants" :key="'select-'+restaurant.id"  :value="restaurant.id">{{ restaurant.name }}</option>
          </select>
        </div>

      </section>
    </div>
    <div class="mb-3 col-6">
      <label for="report-title">Title</label>
      <input v-model="reportData.title" type="text" id="report-title" name="report-title" class="form-control">
    </div>
    <div class="mb-3 col-6">
      <label for="report-title">Image of Disgust</label>
      <input v-model="reportData.pictureOfDisgust" type="text" id="report-title" name="report-title" class="form-control">
    </div>
    <div class="mb-3 col-12">
      <label for="report-title">Describe your experience</label>
      <textarea v-model="reportData.body" rows="4" id="report-title" name="report-title" class="form-control"></textarea>
    </div>
    <div class="mb-3 col-12 text-end">
      <button class="btn btn-primary">Submit <i class="mdi mdi-send"></i></button>
    </div>

  </form>
</template>


<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import {reportsService} from '../services/ReportsService.js'
import { AppState } from '../AppState.js';

const reportData = ref({})
const restaurants = computed(()=> AppState.restaurants)
const selectedRestaurant = computed(()=> AppState.restaurants.find(r => r.id == reportData.value.restaurantId))

onMounted(()=>{
  resetForm()
})

async function createReport(){
  try {
    await reportsService.createReport(reportData.value)
    Pop.success('Created Report')
    resetForm()
  } catch (error) {
    Pop.error(error)
  }
}
function resetForm(){
  reportData.value = {restaurantId: ''}
}

</script>


<style lang="scss" scoped>
.restaurant-preview{
  height: 5dvh;
  width: 100%;
  object-fit: cover;
  object-position: center;
}
</style>
