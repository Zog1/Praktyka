import Api from "./API"
import Swal from "sweetalert2"

export default class UserApi extends Api{
   
    async createUser(params) {
        try {
          await this.baseAxios.post('/authorization/register',params)
          Swal.fire("Good job!", "You successfully added new user!", "success");
        } catch (error) {
          if(error.response.status===400 || 401 || 403)
          Swal.fire("Oops...","Can't create this user","error")
        }
      }

      async fetchUsers() {
        try {
          const res= await this.baseAxios.get('/users').catch((error)=>console.log(error))
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }

      async fetchUser(id) {
        try {
          const res = await this.baseAxios.get('/users/'+id)
          return res.data;
        } catch (error) {
          alert(error.message);
        }
      }
      

      async updateUser(id, fields) {
        try {
          await this.baseAxios.patch('/users/'+id, fields)
          Swal.fire("Good job!", "You successfully edited a user!", "success");
        } catch (error) {
          console.log(error);
        }
      }
      
      async deleteUser(id) {
        try {
          await this.baseAxios.delete('/users/'+id)
        } catch (error) {
          console.log(error);
        }
      }
}
