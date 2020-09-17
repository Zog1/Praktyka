import React from "react";
import { Button, Col } from "reactstrap";
import { useHistory } from "react-router-dom";
import CarImageParams from "../CarImageParams/CarImageParams"

export default function Cardcar(props) {
  const DEFAULT_IMAGE =
    "https://pngimg.com/uploads/question_mark/question_mark_PNG136.png";

  let history = useHistory();

  return (
    <Col sm={3}>
      <div className="CardCar" style={{height: "450px"}}>
        <div className="justify-content-center" style={{maxHeight: "200px", maxWidth: "300px"}}>
          <img
            src={props.src}
            alt="car"
            style={{height: "100%", width: "100%"}}
            onError={e=>e.target.src=DEFAULT_IMAGE}
          />
        </div>
        <Col className="text-center pb-3">
        <p>Brand: {props.brand}</p>
        <p>Model: {props.model}</p>
        <p>Registration number: {props.registrationNumber}</p>
        <CarImageParams 
                    key={1}
                    doorsnumber={props.doors}
                    sitsnumber={props.sits}
                    yearOfProduction={props.yearOfProduction}
                     />
        </Col>
        <div className="text-center">
          <Button
            color="success"
            onClick={() =>
              history.push({
                pathname: "/reserve-car/booking",
                state: props,
              })
            }
          >
            Book me!
          </Button>
        </div>
      </div>
    </Col>
  );
}
