import axios from 'axios'
export function deleteEntity (id: number, endpoint: string): void {
  axios.delete(endpoint + id)
    .then(function (response) {
      // handle success
      console.log(response.data)
    })
    .catch(function (error) {
      // handle error
      console.log(error)
    })
    .then(function () {
      // always executed
    })
}
