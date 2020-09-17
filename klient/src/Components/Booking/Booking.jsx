import React from "react";
import { Col, Row, Button, Table, Container } from "reactstrap";
import { Formik, Field, Form, ErrorMessage } from "formik";
import moment from "moment";
import useBooking from "./Booking.utils";
import CarImageParams from "../CarImageParams/CarImageParams"

export default function Booking({ history }) {
  const {
    validationSchema,
    checkavailable,
    data,
    ref,
    initialValues,
    freeTerms,
    DEFAULT_IMAGE,
    onSubmit,
    checkAvilable,
  } = useBooking(history.location.state);

  return (
    <Container fluid>
      <Row className="justify-content-center">
        <h1>Reserve this car!</h1>
      </Row>
      <Row className="justify-content-center">
        <Col sm={8}>
          <img src={data.src} alt="booking car"  style={{height: "100%", width: "100%", objectfit: "contain"}}
            onError={e=>e.target.src=DEFAULT_IMAGE}/>
        </Col>
        <div className="upsertforms" style={{height: "500px", zIndex: 1, background: "white"}}>
        <Col>
          <Formik
            innerRef={ref}
            initialValues={initialValues}
            validationSchema={validationSchema}
            enableReinitialize
            onSubmit={onSubmit}
          >
            {({ errors, touched }) => {
              return (
                <Row className="justify-content-center">
                  <h4>{data.brand +" "+ data.model}</h4>
                  <h5>{" Registration number: "+data.registrationNumber}</h5>
                    <CarImageParams 
                    key={1}
                    doorsnumber={data.doors}
                    sitsnumber={data.sits}
                    yearOfProduction={data.yearOfProduction}
                     />
                    <Form id="userUpsert" className="mt-2">
                      <label>Start date</label>
                      <Field
                        name="rentaldate"
                        type="date"
                        min={moment().format("YYYY-MM-DD")}
                        className={
                          "form-control" +
                          (errors.rentaldate && touched.rentaldate
                            ? " is-invalid"
                            : "")
                        }
                      />
                      <ErrorMessage
                        name="rentaldate"
                        component="div"
                        className="invalid-feedback"
                      />
                      <label>End date</label>
                      <Field
                        name="returndate"
                        type="date"
                        id="enddate"
                        min={moment().format("YYYY-MM-DD")}
                        className={
                          "form-control" +
                          (errors.returndate && touched.returndate
                            ? " is-invalid"
                            : "")
                        }
                      />
                      <ErrorMessage
                        name="returndate"
                        component="div"
                        className="invalid-feedback"
                      />
                      <div className="d-flex pt-3">
                        <Button
                          color="primary"
                          className="mr-1"
                          onClick={() =>
                            checkAvilable(
                              data.car,
                              ref.current.values.rentaldate,
                              ref.current.values.returndate
                            )
                          }
                        >
                          Check date
                        </Button>
                        <Button color="success" type="submit" className="ml-1">
                          Confirm reservation
                        </Button>
                      </div>
                    </Form>
                </Row>
              );
            }}
          </Formik>
          <Row className="justify-content-center">
            <div
              style={{
                overflowY: "auto",
                height: "150px",
                width: "300px",
                paddingTop: "20px"
              }}
            >
              <Table bordered>
                <thead>
                  <tr>
                    <td>Next available terms</td>
                  </tr>
                </thead>
                <tbody>
                {(freeTerms &&
                        freeTerms.length !== 0) ?
                        freeTerms.map((reservation, i) => {
                          return (
                            <tr key={i}>
                              <td>{reservation}</td>
                            </tr>
                          );
                        }) : (checkavailable ? <tr><td>No available terms</td></tr> : <tr><td>Check available terms</td></tr> )}
                </tbody>
              </Table>
            </div>
      </Row>
        </Col>
        </div>
      </Row>
      </Container>
  );
}
