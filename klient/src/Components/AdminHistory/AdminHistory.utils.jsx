import React, { useState, useMemo } from "react";
import { useHistory } from "react-router-dom";
import Api from "../API/ReservationApi";
import Location from "../API/LocationApi"
import moment from "moment"
import {Button} from "reactstrap"

export default function useAdminHistory() {
  let history = useHistory();
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  async function fetchUsersHistory() {
    setIsLoading(true);
    try {
      let api = new Api();
      const res = await api.fetchUsersHistory();
      setIsLoading(false);
      setData(res);
    } catch (error) {
      console.log(error);
      history.push(".");
    }
  }

  async function finishReservationNow(reservationId){
    let api=new Api()
    await api.finishReservationNow(reservationId)
    let location=new Location()
    await location.deleteLocalization(reservationId)
    window.location.reload(false)
  }

  const columns = useMemo(
    () => [
      {
        Header: "Reservation ID",
        accessor: "reservationId",
      },
      {
        Header: "Rental Date",
        accessor: "rentalDate",
      },
      {
        Header: "Return Date",
        accessor: "returnDate",
      },
      {
        Header: "Registration Number",
        accessor: "car.registrationNumber",
      },
      {
        Header: "Name",
        accessor: "user.firstName",
      },
      {
        Header: "Surname",
        accessor: "user.lastName",
      },
      {
        Header: "Phone",
        accessor: "user.mobileNumber",
      },
      {
        Header: "Mail",
        accessor: "user.email",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            {moment().utc().isBefore(moment.utc(row.original.returnDate)) &&
            row.original.isFinished===false &&
                <Button
                  color="primary"
                  onClick={() =>
                    finishReservationNow(
                      row.original.reservationId,
                      row.original.carId
                    )
                  }
                >
                  Return
                </Button>
              }
              {
                row.original.isFinished===true && "Reservation finished"
              }
          </div>
        ),
      },
    ],
    []
  );

  return { isLoading, data, columns, fetchUsersHistory };
}
