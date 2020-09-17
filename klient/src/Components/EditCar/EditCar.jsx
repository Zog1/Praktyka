import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { Formik, Field, Form, ErrorMessage } from "formik";
import useEditCar from "./EditCar.utils";

export default function EditCar({ history }) {

  const {
    car,
    id,
    initialValues,
    validationSchema,
    isAddMode,
    fetchCar,
    onSubmit,
  } = useEditCar(history.location.state);

  useEffect(() => {
    if (!isAddMode) {
    fetchCar(id);
    }
  }, []);

  return (
    <Formik
      initialValues={isAddMode ? initialValues : car}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched }) => {
        return (
          <Form className="upsertforms">
            <h1>{isAddMode ? "Add Car" : "Edit Car"}</h1>
            <Row>
              <Col>
                <label>Brand</label>
                <Field
                  name="brand"
                  className={
                    "form-control" +
                    (errors.brand && touched.brand ? " is-invalid" : "")
                  }
                />
                <ErrorMessage
                  name="brand"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Registration number</label>
                <Field
                  name="registrationNumber"
                  type="text"
                  className={
                    "form-control" +
                    (errors.registrationNumber && touched.registrationNumber
                      ? " is-invalid"
                      : "")
                  }
                />
                <ErrorMessage
                  name="registrationNumber"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Model</label>
                <Field
                  name="model"
                  type="text"
                  className={
                    "form-control" +
                    (errors.model && touched.model ? " is-invalid" : "")
                  }
                />
                <ErrorMessage
                  name="model"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Type of car</label>
                <Field
                  name="typeOfCar"
                  as="select"
                  className={
                    "form-control" +
                    (errors.typeOfCar && touched.typeOfCar ? " is-invalid" : "")
                  }
                >
                  <option selected>Choose type...</option>
                  <option value="1">Classic</option>
                  <option value="0">Sport</option>
                  <option value="2">Retro</option>
                </Field>
              </Col>
              <Col>
                <label>Year Of Procuction</label>
                <Field
                  type="number"
                  name="yearOfProduction"
                  className={
                    "form-control" +
                    (errors.yearOfProduction && touched.yearOfProduction
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Number of sits</label>
                <Field
                  type="number"
                  name="numberOfSits"
                  className={
                    "form-control" +
                    (errors.numberOfSits && touched.numberOfSits
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Number of doors</label>
                <Field
                  type="number"
                  name="numberOfDoor"
                  className={
                    "form-control" +
                    (errors.numberOfDoor && touched.numberOfDoor
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Img src</label>
                <Field
                  name="imagePath"
                  className={
                    "form-control" +
                    (errors.imagePath && touched.imagePath ? " is-invalid" : "")
                  }
                />
              </Col>
            </Row>
            <div className="pt-3">
              <button type="submit" className="btn btn-primary">
                Save
              </button>

              <Link to={isAddMode ? "." : ".."} className="btn btn-link">
                Cancel
              </Link>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}
