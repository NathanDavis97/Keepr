<template>
  <div class="about text-center container-fluid">
    <div class="row mt-4">
      <div class="col-2">
        <img class="rounded shadow" :src="account.picture" alt="" />
      </div>
      <div class="col-3">
        <h1>{{ account.name }}</h1>
        <small class="row">{{ state.vaults.length }} Vaults</small>
        <small class="row">{{ state.myKeeps.length }} Keeps</small>
      </div>
    </div>
    <div class="row">
      <div class="col nomargpad">
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
        <div class=" card-columns mt-3 nomargpad">
          <VaultComponent v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col nopad">
        <div class="row">
          <div class="col nopad">
            <h4>Keeps <i class="fa fa-plus text-success" aria-hidden="true" type="button" data-toggle="modal" data-target="#keepModal"></i></h4>
          </div>
        </div>
        <div class="row">
          <div class="card-columns mt-3 nomargpad">
            <KeepComponent v-for="keep in state.myKeeps" :key="keep.id" :keep-prop="keep" />
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

export default {
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      myKeeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.vaults)
    })
    onMounted(async() => {
      try {
        await vaultService.getVaults(AppState.account.id)
      } catch (error) {
        logger.log(error)
      }
      try {
        await keepService.getProfileKeeps(AppState.account.id)
      } catch (error) {
        logger.log(error)
      }
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
  @media (min-width: 300px) and (max-width: 991.98px) {
    column-count: 2;
  }
  @media (min-width: 992px)  {
    column-count: 4;
  }
  }
</style>
