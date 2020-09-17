import React, {useEffect} from "react";
import CarManagerTable from "../CarManagerTable/CarManagerTable";
import Loader from "react-loader-spinner";
import useCarManager from "./CarManager.utils"

export default function CarManager() {

  const {columns,data,fetchCars,isLoading} = useCarManager();

  useEffect(() => {
    fetchCars();
  }, []);

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : ( data ?
        <CarManagerTable columns={columns} data={data} />
        : "You don't have cars in database"
      )}
    </div>
  );
}
