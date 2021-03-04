import { logger } from '../utils/Logger'

const { AppState } = require('../AppState')
const { api } = require('./AxiosService')

class VaultKeepService {
  async createVaultKeep(vaultKeep) {
    vaultKeep.creatorId = AppState.account.id
    const res = await api.post('vaultkeeps', vaultKeep)
    logger.log(res.data)
  }

  async deleteVaultKeep(vaultKeepId) {
    const res = await api.delete('vaultkeeps/' + vaultKeepId)
    logger.log(res.data)
  }
}

export const vaultKeepService = new VaultKeepService()
