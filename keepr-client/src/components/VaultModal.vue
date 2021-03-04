<template>
  <div class="vaultModal container-fluid">
    <!-- Button trigger modal -->
    <!-- Modal -->
    <div class="modal fade"
         id="vaultModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title row">
              <div class="col">
                New Vault
              </div>
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form @submit.prevent="createVault">
            <div class="modal-body">
              <div class="form-group">
                <label for=""></label>
                <input type="text"
                       name=""
                       id=""
                       class="form-control"
                       placeholder="Vault Title"
                       aria-describedby="helpId"
                       v-model="state.newVault.name"
                >
                <div class="col mt-2">
                  <input type="checkbox" name="" id="" v-model="state.newVault.isPrivate"> Private?
                  <small id="helpId" class="text-muted">Private vaults can only be seen by you</small>
                </div>
              </div>
              <div class="d-flex justify-content-end">
                <button type="submit" class="btn btn-primary">
                  Create
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultService } from '../services/VaultService'
import { logger } from '../utils/Logger'
import $ from 'jquery'

export default {
  name: 'VaultModal',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          logger.log(state.newVault)
          await vaultService.createVault(state.newVault)
          $('#vaultModal').modal('hide')
          state.newVault = {}
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

</style>
