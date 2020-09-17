import React,  {useEffect} from "react"
import styles from './Footerstyle.module.css'; 
import useFooter from "./useFooter.utils"

export default function Footer(){

    const {year, getYear}=useFooter()

    useEffect(()=>{
         getYear() 
    },[])

    return ( 
        <div className={styles.global}>
            <p>Company Car Reservation <a href="https://wimii.pcz.pl/">PCz</a> ©{year}</p>
        </div>
    )
}