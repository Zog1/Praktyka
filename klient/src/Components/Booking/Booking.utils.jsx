import { useState, useRef } from "react";
import * as Yup from "yup";
import Api from "../API/ReservationApi";

export default function useBooking(props) {

  const data=props
  const [checkavilable, setCheckAvilable] = useState(false);
  const [freeTerms, setFreeTerms] = useState([]);
  sessionStorage.setItem("carID", data.car);

  const DEFAULT_IMAGE =
  "https://pngimg.com/uploads/question_mark/question_mark_PNG136.png";

  const ref = useRef(null);

  let initialValues = {
    rentaldate: data.startdate,
    returndate: data.enddate,
  };
  const validationSchema = Yup.object().shape({
    rentaldate: Yup.date().required("Required"),
    returndate: Yup.date().min(
      Yup.ref("rentaldate"),
      "End date should be greater"
    ),
  });

  async function checkAvilable(carid,rentaldate,returndate) {
      
    let api = new Api();
    const response = await api.checkAvilable(carid,rentaldate,returndate);
    setFreeTerms(response);
    setCheckAvilable(true);
  }

  async function onSubmit(fields) {
    fields.userId = parseInt(sessionStorage.getItem("userID"));
    fields.carId = parseInt(sessionStorage.getItem("carID"));
    try {
      let api = new Api();
      await api.addReservation(fields);
    } catch (error) {
      console.log(error);
    }
  }

  return {
    validationSchema,
    checkavilable,
    data,
    ref,
    initialValues,
    freeTerms,
    DEFAULT_IMAGE,
    onSubmit,
    checkAvilable,
  };
}
