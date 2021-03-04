
const { AppState } = require('../AppState')
const { logger } = require('../utils/Logger')
const { api } = require('./AxiosService')

class ProfileService {
  async getProfile(profileId) {
    const res = await api.get('profiles/' + profileId)
    logger.log(res.data, 'activeProfile')
    AppState.activeProfile = res.data
    AppState.activeProfileId = profileId
  }
}

export const profileService = new ProfileService()
