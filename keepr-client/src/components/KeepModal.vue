<template>
  <div class="keepModal container-fluid">
    <!-- Button trigger modal -->
    <!-- Modal -->
    <div class="modal fade"
         id="keepModal"
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
                New Keep
              </div>
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form @submit.prevent="createKeep">
            <div class="modal-body">
              <div class="form-group text-left">
                <label for="">Title</label>
                <input type="text"
                       name=""
                       id=""
                       class="form-control"
                       placeholder="Keep Title"
                       aria-describedby="helpId"
                       v-model="state.newKeep.name"
                >
                <label for="">Image</label>

                <input type="text"
                       name=""
                       id=""
                       class="form-control"
                       placeholder="Image Url"
                       aria-describedby="helpId"
                       v-model="state.newKeep.img"
                >
                <label for="">Description</label>

                <input type="text"
                       name=""
                       id=""
                       class="form-control"
                       placeholder="Description"
                       aria-describedby="helpId"
                       v-model="state.newKeep.description"
                >
                <div class="col mt-2">
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
import { keepService } from '../services/KeepService'
import { logger } from '../utils/Logger'
import $ from 'jquery'

export default {
  name: 'KeepModal',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          logger.log(state.newKeep)
          await keepService.createKeep(state.newKeep)
          $('#keepModal').modal('hide')
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
