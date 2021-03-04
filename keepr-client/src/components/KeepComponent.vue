<template>
  <div class="container-fluid" @click="showModal">
    <div class="keepComponent col ">
      <div class="card img-fluid">
        <img :src="keepProp.img" alt="" class="card-img rounded">
        <div class="card-img-overlay align-items-end row nomargpad ">
          <div class="keeptext col d-flex align-items-end pb-1 rounded justify-content-between ">
            <div class="row justify-content-between">
              <h5 class="col bigfont">
                <div>{{ keepProp.name }}</div>
                <img v-if="keepProp.creator" :src="keepProp.creator.picture" class="iconsize rounded " alt="">
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
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-body">
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
                <div>
                  {{ keepProp.views }}
                  {{ keepProp.keeps }}
                  {{ keepProp.name }}
                  {{ keepProp.description }}
                  {{ keepProp.id }}
                  <div>
                    <button type="button" class="btn btn-primary">
                      Save
                    </button>
                  </div>
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="triggerId"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Dropdown
                    </button>
                    <div class="dropdown-menu" aria-labelledby="triggerId">
                      <div v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" class="dropdown-item">
                        <div>{{ vault.name }}</div>
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

export default {
  name: 'KeepComponent',
  props: { keepProp: { type: Object, required: true } },
  setup(props) {
    const state = reactive({
      thiskeep: props.keepProp,
      vaults: computed(() => AppState.vaults)
    })

    return {
      state,
      props,
      async showModal() {
        $('#' + props.keepProp.id + '').modal('show')
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
</style>
