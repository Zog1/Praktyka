import React, {useState} from "react"

const useFooter = ()=>{

    const [year, setYear]=useState(0)
    const getYear = ()=>{
        setYear(new Date().getFullYear());
    }
    return {year, getYear}
}
export default useFooter