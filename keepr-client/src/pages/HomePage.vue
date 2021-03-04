<template>
  <div class="container-fluid">
    <div class="component row">
      <div class="card-columns mt-3">
        <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" @click="getVaults" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'

export default {
  name: 'Component',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      myKeeps: computed(() => AppState.myKeeps)
    })
    onMounted(async() => {
      try {
        await keepService.getKeeps()
      } catch (error) {
        logger.log(error)
      }
    })

    return {
      state,
      async getVaults() {
        try {
          await vaultService.getVaults()
        } catch (error) {

        }
        try {
          await keepService.getMyKeeps()
        } catch (error) {
          logger.log(error)
        }
      }
    }
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
