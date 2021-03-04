const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class VaultService {
  async getVaults() {
    const res = await api.get('profiles/' + AppState.account.id + '/vaults')
    logger.log(res.data, 'vaults')
    AppState.vaults = res.data
  }

  async createVault(newVault) {
    logger.log(newVault)
    newVault.creatorId = AppState.account.id
    const res = await api.post('vaults', newVault)
    logger.log(res, 'new vault')
    this.getVaults()
  }
}

export const vaultService = new VaultService()
