import axios from 'axios'
// eslint-disable-next-line
export function deleteEntity (id: number, endpoint: string): Promise<any> {
  return new Promise(function (resolve, reject) {
    axios.delete(endpoint + id)
      .then(function (response) {
        // handle success
        console.log(response.data)
        resolve(response.data)
      })
      .catch(function (error) {
        // handle error
        console.log(error)
        reject(error)
      })
  })
}
