const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class KeepService {
  async getKeeps() {
    const res = await api.get('keeps')
    logger.log(res)
    AppState.keeps = res.data
  }

  async getMyKeeps() {
    debugger
    const res = await api.get('profiles/' + AppState.account.id + '/keeps')
    logger.log(res.data, 'mykeeps')
    AppState.myKeeps = res.data
  }

  async createKeep(newKeep) {
    logger.log(newKeep)
    newKeep.creatorId = AppState.account.id
    const res = await api.post('keeps', newKeep)
    logger.log(res, 'new keep')
    this.getKeeps()
  }
}

export const keepService = new KeepService()
