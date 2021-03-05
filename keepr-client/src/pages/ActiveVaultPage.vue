<template>
  <div class="activeVaultPage container-fluid">
    <div class="row">
      <h4 class="col-8">
        {{ state.activeVault.name }}
        <i v-if="state.activeVault.creatorId == state.myId" class="fa fa-trash text-danger btn " aria-hidden="true" @click="DeleteVault(state.activeVault.id)"></i>
      </h4>
      <h6 class="col offset-2 mt-2">
        <!-- <button type="button" class="btn btn-warning " @click="state.removable = !state.removable">
          Edit Vault
        </button> -->
      </h6>
    </div>
    <div class="row">
      <h6 class="col-3">
        Keeps in Vault : {{ state.keeps.length }}
      </h6>
    </div>
    <div class="card-columns mt-3">
      <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
import { logger } from '../utils/Logger'
import { useRoute, useRouter } from 'vue-router'
// import { vaultKeepService } from '../services/VaultKeepService'
// import $ from 'jquery'

export default {
  name: 'ActiveVaultPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.activeVaultKeeps),
      activeVault: computed(() => AppState.activeVault),
      activeVaultCreator: computed(() => AppState.activeVault.creatorId),
      myId: computed(() => AppState.account.id),
      removable: false
    })
    onMounted(async() => {
      try {
        await vaultService.getVaultKeeps(route.params.id)
        logger.log(route)
      } catch (error) {
        logger.log(error)
      }
      try {
        await vaultService.getVaultById(route.params.id)
      } catch (error) {
        logger.log(error)
      }
    })

    return {
      state,
      async DeleteVault(vaultId) {
        if (window.confirm('Are you sure you would like to delete?')) {
          try {
            await vaultService.deleteVault(vaultId)
            router.push({ name: 'ProfilePage', params: { id: state.myId } })
          } catch (error) {
            logger.error(error)
          }
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.card-columns {
  @media (min-width: 565px) and (max-width: 767.98px) {
    column-count: 2;
  }
  @media (min-width: 992px)  {
    column-count: 4;
  }
  }
</style>
