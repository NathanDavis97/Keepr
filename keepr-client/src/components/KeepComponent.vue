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
                <img :src="keepProp.creator.picture" class="iconsize rounded " alt="">
              </h5>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="modal fade"
         id="quickModal1"
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
                <slot name="Img"></slot>
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col">
                    <slot name="title"></slot>
                  </div>
                </div>
                <slot name="description"></slot>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button type="button" class="btn btn-primary">
              Save
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
export default {
  name: 'KeepComponent',
  props: { keepProp: { type: Object, required: true } },
  setup() {
    return {
      async showModal() {
        $('#quickModal1').modal('show')
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
