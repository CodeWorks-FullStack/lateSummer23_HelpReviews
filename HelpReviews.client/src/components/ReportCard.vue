<template>
  <section class="row">
<h3 class="col-12 text-center fw-bold text-success">"{{ report.title }}"</h3>
<div class="col-6">
  <p>
    {{ report.body }}
  </p>
</div>
<div class="col-6 d-flex text-end justify-content-end">
  <div>
    <p class="pb-0">I FORGOT THE DATE</p>
    <span class="text-center">{{ report.creator.name }}</span>
  </div>
  <img :src="report.creator.picture" class="img-fluid rounded-circle " alt="">
</div>
<div class="col-12 d-flex justify-content-center">
  <img :src="report.pictureOfDisgust" class="report-picture" alt="">
</div>

  </section>
</template>


<script setup>
import { Report } from '../models/Report.js';


const props = defineProps({report: {type: Report, required: true}})

</script>


<style lang="scss" scoped>
img:not(.report-picture){
  height: 50px;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
}

.report-picture{
  max-height: 35vh;
  max-width: 100%;
  object-fit: contain;
}
</style>
