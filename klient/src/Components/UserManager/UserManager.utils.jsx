import React, { useMemo, useState } from "react";
import { useHistory } from "react-router-dom";
import Api from "../API/UserApi";
import Swal from "sweetalert2";
import EditIcon from "@material-ui/icons/Edit";
import { Tooltip, IconButton } from "@material-ui/core";
import DeleteIcon from "@material-ui/icons/Delete";

export default function useUserManager() {
  let history = useHistory();
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  async function fetchUsers() {
    try {
      let api = new Api();
      setIsLoading(true);
      const res = await api.fetchUsers();
      setData(res);
      setIsLoading(false);
    } catch (error) {
      Swal.fire("Error", error.message, "error");
    }
  }

  function deleteUser(id) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true,
    }).then((result) => {
      if (result.value) {
        try {
          let api = new Api();
          api.deleteUser(id);
          Swal.fire(
            "Deleted!",
            "Your user has been deleted.",
            "success"
          ).then(() => window.location.reload(false));
        } catch (error) {
          console.log(error);
        }
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire("Cancelled", "Your user is safe :)", "success");
      }
    });
  }

  const columns = useMemo(
    () => [
      {
        Header: "User ID",
        accessor: "userId",
      },
      {
        Header: "Name",
        accessor: "firstName",
      },
      {
        Header: "Surname",
        accessor: "lastName",
      },
      {
        Header: "Phone",
        accessor: "mobileNumber",
      },
      {
        Header: "Mail",
        accessor: "email",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            <Tooltip title="Edit">
              <IconButton
                aria-label="Edit"
                onClick={() =>
                  history.push({
                    pathname: "/admin/user-manager/edit",
                    state: row.original.userId,
                  })
                }
              >
                <EditIcon className="m-1" />
              </IconButton>
            </Tooltip>

            <Tooltip title="Delete">
              <IconButton aria-label="Delete" onClick={() => deleteUser(row.original.userId)}>
                <DeleteIcon
                  className="m-1"
                />
              </IconButton>
            </Tooltip>
          </div>
        ),
      },
    ],
    []
  );

  return { columns, data, fetchUsers, isLoading };
}
