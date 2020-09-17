import Swal from "sweetalert2"
import Api from './API'

export default class FaultApi extends Api{

  async createReport(fields){
      try{
        await this.baseAxios.post('/defects',fields)
      }catch(error){
        Swal.fire("Oops..",error,"error")
      }
  }

  async fetchFaults(){
    try {
        const response = await this.baseAxios.get('/defects')
        return response.data
    }catch (error){
        console.log(error)
    }
  }
  async updateStatus(fields, id) {
    try {
      await this.baseAxios.patch('/defects/'+id, fields);
    } catch (error) {
      console.log(error);
    }
  }
  async deleteFault(fields, id) {
    try {
      await this.baseAxios.patch('/defects/'+id, fields);
    } catch (error) {
      console.log(error);
    }
  }
}