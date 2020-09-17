import Swal from "sweetalert2";
import Api from './API' 

export default class ReservationsApi extends Api{

  async fetchUsersHistory() {
    try {
      const { data } = await this.baseAxios.get('/reservations');
      return data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }
  
  async fetchUserHistory(id) {
    try {
      const res = await this.baseAxios.get("/reservations/users/" + id);
      return res.data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }

  async cancelReservation(id) {
    try {
      await this.baseAxios.delete("/reservations/" + id);
      Swal.fire(
        "Deleted!",
        "Your reservation has been deleted.",
        "success"
      ).then(() => window.location.reload(false));
    } catch (error) {
      console.log(error);
    }
  }

  async checkAvilable(carId, rentalDate, returnDate) {
    try {
      const response = await this.baseAxios.get(
        "/terms/" + carId + "/" + rentalDate + "/" + returnDate
      );
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async addReservation(fields) {
    try {
      await this.baseAxios.post('/reservations', fields);
      Swal.fire("Success", "You successfully reserved term", "success");
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
      console.log(error);
    }
  }

  async finishReservationNow(reservationid,fields)
  {
    try{
      await this.baseAxios.delete('/reservations/'+reservationid)
      Swal.fire("Success", "You successfully finished reservation", "success");
    }catch (error){
      console.log(error)
    }
  }
}
