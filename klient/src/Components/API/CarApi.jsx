import Swal from "sweetalert2"
import Api from "./API"

export default class CarApi extends Api{

    async createCar(params) {
        try {
          await this.baseAxios.post('/cars', params)
          Swal.fire("Good job!", "You successfully added new car!", "success");
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong...", "error");
        }
      }

      async fetchCars() {
        try {
          const res = await this.baseAxios.get('/cars');
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }

      async fetchCar(id) {
        try {
          const res = await this.baseAxios.get('/cars/'+id)
          return res.data;
        } catch (error) {
          alert(error.message);
        }
      }

      async updateCar(id, params) {
        try {
            await this.baseAxios.patch('/cars/'+id,params)
          Swal.fire("Good job!", "You successfully edited a car!", "success");
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong", "error");
        }
      }
      
      async deleteCar(id) {
        try {
            await this.baseAxios.delete('/cars/'+id)
        } catch (error) {
          console.log(error);
        }
      }
    
      async fetchCarByDate(startdate, enddate) {
        try {
          const res = await this.baseAxios.get('/terms/'+ startdate + "/" + enddate)
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }
}