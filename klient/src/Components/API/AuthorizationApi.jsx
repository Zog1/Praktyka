import Swal from "sweetalert2"
import Axios from "axios";

export default class AuthorizationApi{

  constructor(){
    this.baseAxios = Axios.create({
      baseURL: process.env.REACT_APP_BASE_URL
    })
  }

  async signIn(params) {
    try {
      const response = await this.baseAxios.post(
        "/authorization/signIn",
        params
      );
      sessionStorage.setItem("isLoggedIn", true)
      return response.data;
    } catch (error) {
      if (error.response) {
        console.log(error.response)
        Swal.fire("Oops...", error.response.data, "error");
      }
    }
  }

  async sendPassword(fields) {
    console.log("AuthorizationApi -> sendPassword -> fields", fields)
    try {
      await this.baseAxios.patch("/authorization", fields);
      Swal.fire("Good job!", "You successfully set your password!", "success");
      return true
    } catch (error) {
      return false
    }
  }
}
