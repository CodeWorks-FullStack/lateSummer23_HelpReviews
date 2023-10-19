<template>
  <div class="container-fluid">

    <section class="row">
      <ReportForm/>
    </section>

    <section class="row">

      <div class="col-4 my-3" v-for="r in restaurants" :key=r.id>
        <RestaurantCard :restaurant="r" />
      </div>

    </section>
  </div>
</template>

<script>
import { AppState } from '../AppState.js'
import { computed, onMounted } from 'vue'
import { restaurantsService } from '../services/RestaurantsService.js'
import ReportForm from '../components/ReportForm.vue'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
    setup() {
        async function getRestaurants() {
            try {
                await restaurantsService.getRestaurants();
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, 'error');
            }
        }
        onMounted(() => {
            getRestaurants();
        });
        return {
            restaurants: computed(() => AppState.restaurants),
        };
    },
    components: { ReportForm }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
