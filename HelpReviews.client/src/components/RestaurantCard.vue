<template>
    <div class="elevation-5" v-if="restaurant.id">
        <img class="img-fluid" :src="restaurant.imgUrl" alt="">
        <div class="p-3">
            <span>{{ restaurant.name }}</span>
            <p>{{ restaurant.description }}</p>
            <div class="d-flex justify-content-around">
                <span><i class="mdi mdi-heart"></i> recent visits</span>
                <span><i class="mdi mdi-note"></i> reports</span>
            </div>
        </div>
    </div>
</template>

<script>
import { Restaurant } from '../models/Restaurant.js';

export default {
    props: {
        restaurant: { type: Restaurant, required: true }
    },
    setup() {
        return {};
    },
};
</script>


<style></style>