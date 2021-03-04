<template>
  <div class="container-fluid">
    <div class="vaultComponent col ">
      <div class="card img-fluid">
        <img :src="vaultProp.img" alt="" class="card-img rounded">
        <div class="card-img-overlay align-items-end row nomargpad ">
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
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'

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
      props
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
.bigfont{
  font-size: 30px;
}
.iconsize{
  height: 30px;
  margin-top: 2px;
}
</style>
