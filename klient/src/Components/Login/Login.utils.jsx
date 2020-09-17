import {useState} from "react"
import * as Yup from "yup";
import Api from "../API/AuthorizationApi";
import jwt_decode from "jwt-decode";
import Swal from "sweetalert2";
import {useHistory} from "react-router-dom"

export default function useLogin() {

  let history=useHistory()
  let initialValues = {
    email: "",
    password: "",
  };
  const [isSended,setIsSended]=useState(false)

  const validationSchema = Yup.object().shape({
    password: Yup.string().required("Required"),
    email: Yup.string().email("This isn't email").required("Required"),
  });

  async function onSubmit(fields, { resetForm }) {
    signIn(fields);
    resetForm({ state: "" });
    setIsSended(true)
  }

  async function signIn(params) {
    try {
      let api = new Api();
      const response = await api.signIn(params);
      setIsSended(false)
      if (sessionStorage.getItem("isLoggedIn")) {
        let decodedToken = jwt_decode(response.accessToken);
        sessionStorage.setItem(
          "userRole",
          decodedToken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ]
        );
        sessionStorage.setItem("userID", decodedToken.sub);
        sessionStorage.setItem("accessToken", response.accessToken);
        sessionStorage.setItem("refreshToken", response.refreshToken);
        switch (sessionStorage.getItem("userRole")) {
          case "Worker":
            sessionStorage.setItem("userRole", "user");
            window.location.reload()
            history.push('/home');
            break;
          case "Admin":
            sessionStorage.setItem("userRole", "admin");
            window.location.reload()
            history.push('/admin');
            break;
          default:
            throw new Error("Bad response from server");
        }
      }
      else{
        throw new Error("Try again");
      }
    } catch (error) {
      Swal.fire("Oops...", error.message, "error");
    }
  }

  return { initialValues,isSended, validationSchema, signIn, onSubmit };
}
