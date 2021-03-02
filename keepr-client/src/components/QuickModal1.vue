<template>
  <div class="quickModal container-fluid">
    <!-- Button trigger modal -->

    <!-- Modal -->
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
export default {
  name: 'QuickModal1',
  setup() {
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
