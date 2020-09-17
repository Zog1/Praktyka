import React, { useState, useMemo } from "react";
import { useHistory } from "react-router-dom";
import moment from "moment";
import Swal from "sweetalert2";
import Api from "../API/ReservationApi";
import { Tooltip, IconButton } from "@material-ui/core";
import DeleteIcon from '@material-ui/icons/Delete';
import MapIcon from '@material-ui/icons/Map';
import BuildIcon from '@material-ui/icons/Build';

export default function useUserHistory() {
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const history = useHistory();
  const userId=sessionStorage.getItem("userID")

  const columns = useMemo(
    () => [
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
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            {moment.utc(row.original.rentalDate).isBefore(moment().utc()) &&
              moment
                .utc()
                .isBefore(
                  moment.utc(row.original.returnDate).add(14, "days")
                ) && (
                  
                  <Tooltip title="Report problem">
              <IconButton aria-label="Report problem" onClick={() => reportProblem(row.original.carId)}>
                <BuildIcon
                  className="m-1"
                />
              </IconButton>
            </Tooltip>
              )}
            {moment.utc().isBefore(moment.utc(row.original.rentalDate)) &&
             (row.original.isFinished===false) && (
              <Tooltip title="Cancel reservation">
              <IconButton aria-label="Cancel reservation" onClick={() => cancelReservation(row.original.reservationId)}>
                <DeleteIcon
                  className="m-1"
                />
              </IconButton>
            </Tooltip>
            )}
            {moment.utc().isAfter(moment.utc(row.original.rentalDate)) &&
              moment.utc().isBefore(moment.utc(row.original.returnDate)) &&
              <Tooltip title="Map">
              <IconButton aria-label="Map" onClick={()=>history.push({
                pathname: '/history/map',
                state: row.original.reservationId
              })}>
                <MapIcon
                  className="m-1"
                />
              </IconButton>
            </Tooltip> }
            {row.original.isFinished===true && "Reservation finished"}
          </div>
        ),
      },
    ],
    []
  );

  function cancelReservation(id) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete reservation!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true,
    }).then((result) => {
      if (result.value) {
        try {
          let api = new Api();
          api.cancelReservation(id);
        } catch (error) {
          console.log(error);
        }
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire("Cancelled", "Your reservation is safe :)", "success");
      }
    });
  }

  function reportProblem(carId) {
    history.push({
      pathname: "/history/report",
      state: { carId },
    });
  }

  async function fetchUserHistory(id) {
    setIsLoading(true);
    try {
      setIsLoading(true);
      let api = new Api();
      const res = await api.fetchUserHistory(id);
      setIsLoading(false);
      setData(res);
    } catch (error) {
      history.push(".");
    }
  }

  return { data, isLoading, columns, userId, fetchUserHistory };
}
