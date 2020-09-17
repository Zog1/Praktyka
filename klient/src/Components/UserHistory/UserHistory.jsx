import React, { useEffect } from "react";
import TableUserHistory from "../TableUserHistory/TableUserHistory";
import Loader from "react-loader-spinner";
import useUserHistory from "./UserHistory.utils";

export default function UserHistory() {
  
  const {data, isLoading, columns, userId, fetchUserHistory}=useUserHistory();

  useEffect(() => {
    fetchUserHistory(userId);
  }, [userId]);


  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (data ?
        <TableUserHistory columns={columns} data={data} /> : "You don't have reservations"
      )}
    </div>
  );
}
