<template>
  <div class="container-fluid nopad">
    <div class="keepComponent col ">
      <div class="card img-fluid" @click="showModal">
        <img :src="keepProp.img" alt="" class="card-img rounded">
        <div class="card-img-overlay align-items-end row nomargpad ">
          <div class="keeptext col d-flex align-items-end pb-1 rounded justify-content-between ">
            <div class="row ">
              <h5 class="col-12 bigfont">
                <div>
                  {{ keepProp.name }}

                  <img v-if="keepProp.creator" :src="keepProp.creator.picture" class="iconsize rounded " alt="" @click="goToProfile(keepProp.creatorId)">
                </div>
              </h5>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="modal fade"
         :id="keepProp.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-body minh">
            <div class="row">
              <div class="col-6">
                <img :src="keepProp.img" alt="" class="card-img rounded">
              </div>
              <div class="col-6 ">
                <div type="button" class="row justify-content-end" data-dismiss="modal">
                  <div class="mx-2">
                    X
                  </div>
                </div>
                <div class="row">
                  <div class="col">
                    <slot name="title"></slot>
                  </div>
                </div>
                <div class="minh col">
                  <div class="row justify-content-around">
                    <div class="col offset-1">
                      <i class="fa fa-eye fa-lg" aria-hidden="true"></i> {{ keepProp.views }}
                    </div>
                    <div class="col">
                      <i class="fa fa-lock fa-lg" aria-hidden="true"></i> {{ keepProp.keeps }}
                    </div>
                  </div>
                  <h4 class="row justify-content-center pt-3">
                    <strong>{{ keepProp.name }}</strong>
                  </h4>
                  <div class="row pt-1 justify-content-center">
                    <p class="p">
                      {{ keepProp.description }}
                    </p>
                  </div>
                  <div v-if="keepProp.vaultKeepId">
                    <div v-if="keepProp.creatorId == state.myId">
                      <button @click="deleteFromVault(keepProp.vaultKeepId, keepProp.id)" type="button" class="btn btn-sm btn-primary">
                        Remove from vault
                      </button>
                    </div>
                  </div>
                  <div class="row modal-footer mt-5 ">
                    <div class="col-12">
                      <div class="row justify-content-between">
                        <div class="dropdown col-4">
                          <button class="btn btn-secondary btn-sm dropdown-toggle"
                                  type="button"
                                  id="triggerId"
                                  data-toggle="dropdown"
                                  aria-haspopup="true"
                                  aria-expanded="false"
                          >
                            Add To Vault
                          </button>
                          <div class="dropdown-menu" aria-labelledby="triggerId">
                            <div v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" class="dropdown-item">
                              <div @click="goToVault(vault.id, keepProp.id)">
                                {{ vault.name }}
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="col-1" v-if="keepProp.creatorId == state.myId">
                          <i class="fa fa-trash text-danger btn" aria-hidden="true" @click="DeleteKeep(keepProp.id)"></i>
                        </div>
                        <router-link class="col-5" :to="{name: 'ProfilePage', params: {id: keepProp.creatorId}} ">
                          <div v-if="keepProp.creator" @click="goToProfile(keepProp.creatorId)">
                            {{ keepProp.creator.name }}
                          </div>
                        </router-link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
// import { profileService } from '../services/ProfileService'
import { vaultKeepService } from '../services/VaultKeepService'
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
import { vaultService } from '../services/VaultService'
import { useRouter, useRoute } from 'vue-router'

export default {
  name: 'KeepComponent',
  props: { keepProp: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()
    const route = useRoute()

    const state = reactive({
      thiskeep: props.keepProp,
      vaults: computed(() => AppState.vaults),
      myId: computed(() => AppState.account.id),
      myVaultKeeps: computed(() => AppState.activeVaultKeeps)
    })

    return {
      state,
      props,
      async showModal() {
        $('#' + props.keepProp.id + '').modal('show')
        if (props.keepProp.creatorId !== AppState.account.id) {
          try {
            await keepService.editViews(props.keepProp)
          } catch (error) {
            logger.error(error)
          }
        }
      },
      async goToProfile(profileId) {
        try {
          $('#' + props.keepProp.id + '').modal('hide')

          // await profileService.getProfile(profileId)
          // await vaultService.getVaults(profileId)
        } catch (error) {
          logger.error(error)
        }
        try {
          await keepService.getProfileKeeps(profileId)
        } catch (error) {
          logger.error(error)
        }
      },
      async DeleteKeep(keepId) {
        if (window.confirm('Are you sure you would like to delete?')) {
          try {
            await keepService.deleteKeep(keepId)
            $('#' + props.keepProp.id + '').modal('hide')
          } catch (error) {
            logger.error(error)
          }
        }
      },
      async goToVault(id, keepId) {
        const vaultKeep = { vaultId: id, keepId: keepId }
        await vaultKeepService.createVaultKeep(vaultKeep)
        await vaultService.getVaultById(id)
        $('#' + props.keepProp.id + '').modal('hide')
        router.push({ name: 'ActiveVault', params: { id: id } })
        await keepService.editKeepCount(props.keepProp)
      },
      async deleteFromVault(vaultKeepId, keepId) {
        // if (state.removable) {
        try {
          logger.log(vaultKeepId)
          $('#' + keepId + '').modal('hide')
          await vaultKeepService.deleteVaultKeep(vaultKeepId)

          await vaultService.getVaultKeeps(route.params.id)
        } catch (error) {
          logger.error(error)
          // }
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.nomargpad{
  padding: 0;
  margin: 0;
}
.nopad{
  padding: 0;
}
.minh {
    min-height:300px;
}
.keeptext{
  color: white;
  background: rgb(59, 56, 56);
background: linear-gradient(360deg, rgb(0, 0, 0) 0%, rgba(255, 255, 255, 0) 100%);
min-height: 30%;
min-width: 100%;
}
.bigfont{
  font-size: 30px;
}
.iconsize{
  height: 30px;
  margin-top: 2px;
}
.keepComponent{
  padding: 0;
}
.positionEnd{
  position: absolute, end;
}
.p {
    word-wrap: break-word;
    max-width: 350px;
}
</style>
