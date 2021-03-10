const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class KeepService {
  async getKeeps() {
    const res = await api.get('keeps')
    logger.log(res)
    AppState.keeps = res.data
  }

  async getProfileKeeps(profileId) {
    const res = await api.get('profiles/' + profileId + '/keeps')
    AppState.profileKeeps = res.data
    logger.log(AppState.profileKeeps, 'mykeeps')
  }

  async editViews(keep) {
    keep.views++
    const res = await api.put('keeps/' + keep.id + '/edit', keep)
    logger.log(res.data)
    return res.data
  }

  async editKeepCount(keep) {
    keep.keeps++
    const res = await api.put('keeps/' + keep.id + '/edit', keep)
    logger.log(res.data)
    return res.data
  }

  async createKeep(newKeep) {
    logger.log(newKeep)
    newKeep.creatorId = AppState.account.id
    const res = await api.post('keeps', newKeep)
    logger.log(res, 'new keep')
    this.getProfileKeeps(AppState.account.id)
  }

  async deleteKeep(keepId) {
    const res = await api.delete('keeps/' + keepId)
    return (res, 'succefully deleted')
  }
}

export const keepService = new KeepService()
