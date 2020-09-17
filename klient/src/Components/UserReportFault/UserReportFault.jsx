import React from "react"
import { Formik, Field, Form, ErrorMessage } from "formik";
import {Link} from "react-router-dom"
import "../styles/componentsstyle.css"
import Loader from "react-loader-spinner"
import useUserReportFault from "./UserReportFault.utils"

export default function UserReportFault({history}){

  const {initialValues, isSended, data, validationSchema, onSubmit}=useUserReportFault(history.location.state)

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={(values)=>onSubmit(values, data.carId)}
    >
      {({ errors, touched }) => {
        return (
          <Form  className="upsertforms">
            <h1>Report problem</h1>

            <label>Describe it!</label>
            <Field
            as="textarea"
              name="description"
              className={
                "form-control" +
                (errors.description && touched.description ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="firstName"
              component="div"
              className="invalid-feedback"
            />

            
            <div className="pt-3">
              <button
                type="submit"
                className="btn btn-primary"
                disabled={isSended}
              >
              {isSended && <Loader type="Oval" color="#00BFFF" height="20px" width="20px"/>}
                Save
              </button>

              <Link to={".."} className="btn btn-link">
                Cancel
              </Link>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}