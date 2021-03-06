import Axios from 'axios'

export const baseURL = window.location.origin.includes('localhost') ? 'https://localhost:5001/api' : 'https://vaultkeeper.herokuapp.com/api/'
export const api = Axios.create({
  baseURL,
  timeout: 8000
})

export const setBearer = function(bearer) {
  api.defaults.headers.authorization = bearer
}

export const resetBearer = function() {
  api.defaults.headers.authorization = ''
}
