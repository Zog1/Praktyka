import React from "react";
import birthday from "../img/birthday.png";
import sit from "../img/sit.png";
import doors from "../img/doors.png";
import {Row} from "react-bootstrap"

const CarImageParams = (props) => {
  return (
    <Row className="mt-2 justify-content-center">
      <div className="pr-3">
        <img src={doors} alt="car doors" />
        {props.doorsnumber}
      </div>

      <img src={sit} alt="sit" />
      {props.sitsnumber}
      <div className="pl-3">
        <img src={birthday} alt="birthday cake" />
        {props.yearOfProduction}
      </div>
    </Row>
  );
};

export default CarImageParams;
