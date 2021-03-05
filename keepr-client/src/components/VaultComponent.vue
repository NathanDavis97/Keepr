<template>
  <div class="container-fluid  nomargpad" @click="getActiveVault(vaultProp.id)">
    <router-link :to="{name: 'ActiveVault', params: {id: state.thisvault.id}} ">
      <div class="vaultComponent col nomargpad">
        <div class="card img-fluid size">
          <img :src="vaultProp.img" alt="" class="card-img rounded">
          <div class="  card-img-overlay align-items-end row nomargpad ">
            <div class="vaulttext col d-flex align-items-end pb-1 rounded justify-content-between ">
              <div class="row justify-content-between">
                <h5 class="col bigfont">
                  <div>{{ vaultProp.name }}</div>
                </h5>
              </div>
            </div>
          </div>
        </div>
      </div>
    </router-link>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
import { logger } from '../utils/Logger'

export default {
  name: 'VaultComponent',
  props: { vaultProp: { type: Object, required: true } },
  setup(props) {
    const state = reactive({
      thisvault: props.vaultProp,
      vaults: computed(() => AppState.vaults)
    })

    return {
      state,
      props,
      async getActiveVault(id) {
        try {
          await vaultService.getVaultById(id)
        } catch (error) {
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.nomargpad{
  padding: 0;
  margin: 0;
}
.vaulttext{
  color: white;
  background: rgb(59, 56, 56);
background: linear-gradient(360deg, rgb(0, 0, 0) 0%, rgba(255, 255, 255, 0) 100%);
min-height: 30%;
min-width: 100%;
}
.size{
    min-height: 200px;
    background: grey
    ;

}
.bigfont{
  font-size: 30px;
}
.iconsize{
  height: 30px;
  margin-top: 2px;
}

</style>
