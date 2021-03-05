<template>
  <div class="container-fluid">
    <div class="component row">
      <div class="card-columns mt-3" @click="getVaultsFirst">
        <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
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
      myKeeps: computed(() => AppState.myKeeps),
      myVaults: computed(() => AppState.myVaults)
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
      async getVaultsFirst() {
        if (state.vaults === undefined) {
          await vaultService.getVaults(state.account.id)
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
  @media(min-width: 300px) and (max-width: 991.98px) {
    column-count: 2;
  }
  @media (min-width: 992px)  {
    column-count: 4;
  }
  }
</style>
