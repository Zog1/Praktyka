import React from "react"
import {Col} from "reactstrap"

export default function Feature(props){
    return (<Col sm={4}>
        <img src={props.icon} alt="feature icon"/>
        <p style={{paddingTop: "15px"}}>{props.text}</p>
        </Col>)
}