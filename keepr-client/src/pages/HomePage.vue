<template>
  <div class="container-fluid">
    <div class="component row">
      <div class="card-columns mt-3">
        <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
      </div>
    </div>
    <QuickModal1 />
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { keepService } from '../services/KeepService'
import { AppState } from '../AppState'

export default {
  name: 'Component',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => keepService.getKeeps())
    return { state }
  },
  components: {}
}
</script>

<style
    QuickModal1 lang="scss" scoped>
.card-columns {
    column-count: 4;

  }

</style>
