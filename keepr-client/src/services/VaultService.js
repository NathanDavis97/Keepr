const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class VaultService {
  async getVaults(profileId) {
    logger.log(profileId)
    const res = await api.get('profiles/' + profileId + '/vaults')
    logger.log(res.data, 'vaults')
    AppState.vaults = res.data
  }

  async getVaultById(id) {
    const res = await api.get('vaults/' + id)
    logger.log(res.data)
    AppState.activeVault = res.data
  }

  async getVaultKeeps(id) {
    const res = await api.get('vaults/' + id + '/keeps')
    AppState.activeVaultKeeps = res.data
  }

  async createVault(newVault) {
    logger.log(newVault)
    newVault.description = ''
    newVault.creatorId = AppState.account.id
    const res = await api.post('vaults', newVault)
    logger.log(res, 'new vault')
    this.getVaults()
  }

  async deleteVault(vaultId) {
    console.log(vaultId, vaultId)
    const res = await api.delete('vaults/' + vaultId)
    return (res, 'succefully deleted')
  }
}

export const vaultService = new VaultService()
