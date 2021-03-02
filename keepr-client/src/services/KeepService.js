const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class KeepService {
  async getKeeps() {
    const res = await api.get('keeps')
    logger.log(res)
    AppState.keeps = res.data
  }
}

export const keepService = new KeepService()
