<template>
  <div class="about text-center container-fluid">
    <div class="row">
      <div class="col-3">
        <img class="rounded" :src="state.profile.picture" alt="" />
      </div>
      <div class="col-3">
        <h1>{{ state.profile.name }}</h1>
        <small v-if="state.vaults" class="row">{{ state.vaults.length }} Vaults</small>
        <small v-if="state.keeps" class="row">{{ state.keeps.length }} Keeps</small>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="row">
          <div class="col">
            <h4>
              Vaults <i class="fa fa-plus text-success"
                        aria-hidden="true"
                        type="button"
                        data-toggle="modal"
                        data-target="#vaultModal"
              ></i>
            </h4>
          </div>
        </div>
        <div class=" card-columns mt-3">
          <VaultComponent v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div class="row">
          <div class="col">
            <h4>Keeps <i class="fa fa-plus text-success" aria-hidden="true" type="button" data-toggle="modal" data-target="#keepModal"></i></h4>
          </div>
        </div>
        <div class="row">
          <div class="card-columns mt-3 ">
            <KeepComponent v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
          </div>
        </div>
      </div>
    </div>
    <KeepModal />
    <VaultModal />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { keepService } from '../services/KeepService'
import { vaultService } from '../services/VaultService'
import { profileService } from '../services/ProfileService'
import { useRoute } from 'vue-router'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()

    const state = reactive({
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeProfile)
    })
    onMounted(async() => {
      try {
        await profileService.getProfile(route.params.id)
        await vaultService.getVaults(route.params.id)
      } catch (error) {
        logger.log(error)
      }
      try {
        await keepService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.log(error)
      }
      // try {
      // } catch (error) {
      //   logger.log(error)
      // }
    })
    return {
      state, account: computed(() => AppState.account)

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
.card-columns {
  @media (min-width: 565px) and (max-width: 767.98px) {
    column-count: 2;
  }
  @media (min-width: 992px)  {
    column-count: 4;
  }
  }
</style>
