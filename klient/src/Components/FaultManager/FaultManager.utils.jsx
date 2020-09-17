import React, { useState, useMemo } from "react";
import { Input } from "reactstrap";
import { useHistory } from "react-router-dom";
import Api from "../API/FaultApi";
import Swal from "sweetalert2";

export default function useFaultManager() {
  let history = useHistory();
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  async function deleteFault(fields, id) {
    try {
      let api = new Api();
      await api.deleteFault(fields, id);
    } catch (error) {
      console.log(error);
    }
  }

  async function updateStatus(fields, id) {
    try {
      let api = new Api();
      await api.updateStatus(fields, id);
    } catch (error) {
      console.log(error);
    }
  }

  async function fetchFaults() {
    try {
      let api = new Api();
      setIsLoading(true);
      const response = await api.fetchFaults();
      setData(response);
      setIsLoading(false);
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
        history.goBack()
      );
    }
  }

  function handleChange(id, status) {
    const fields={
      id: id,
    }
    switch (status) {
      case "1":
        fields.status=1;
        updateStatus(fields, id);
        break;
      case "2":
        fields.status=2;
        deleteFault(fields, id);
        break;
      case "0":
        Swal.fire(
          "Whooa!",
          "You should pick in progress or all okay status",
          "question"
        );
        break;
      default:
        Swal.fire("Oops...", "Something went wrong", "error");
    }
  }

  const columns = useMemo(
    () => [
      {
        Header: "Registration number",
        accessor: "registrationNumber",
      },
      {
        Header: "Description",
        accessor: "description",
      },
      {
        Header: "Name",
        accessor: "name",
      },
      {
        Header: "Surname",
        accessor: "surname",
      },
      {
        Header: "Phone",
        accessor: "phoneNumber",
      },
      {
        Header: "Date of report",
        accessor: "dateOfReport",
        Cell: ({ row }) => (
          row.original.dateOfReport.split('T')[0]
        ),
      },
      {
        Header: "Status",
        accessor: "status",
        Cell: ({ row }) => (
          <Input
            type="select"
            defaultValue={row.original.status}
            onChange={(e) => handleChange(row.original.defectId, e.target.value)}
          >
            <option value="0"> to check </option>
            <option value="1"> in progress </option>
            <option value="2"> all okay </option>
          </Input>
        ),
      },
    ],
    []
  );

  return { data, isLoading, columns, fetchFaults };
}
