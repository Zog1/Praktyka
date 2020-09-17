import React, { useEffect } from "react";
import UserManagerTable from "../UserManagerTable/UserManagerTable";
import Loader from "react-loader-spinner";
import useUserManager from './UserManager.utils'

export default function UserManager() {
  const {columns,data,fetchUsers,isLoading}=useUserManager()

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        data && <UserManagerTable columns={columns} data={data} />
      )}
    </div>
  );
}
