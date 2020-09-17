import React, { useEffect } from "react";
import TableAdminHistory from "../TableAdminHistory/TableAdminHistory";
import Loader from "react-loader-spinner";
import useAdminHistory from "./AdminHistory.utils";

export default function AdminHistory() {
 
  const {isLoading,data,columns,fetchUsersHistory}=useAdminHistory()
  
  useEffect(() => {
    fetchUsersHistory();
  }, []);

  return (
  <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <TableAdminHistory columns={columns} data={data} />
      )}
    </div>);
}
