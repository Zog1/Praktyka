import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import useEditUser from "./EditUser.utils";

export default function EditUser({ history }) {

  const {
    user,
    initialValues,
    validationSchema,
    id,
    isAddMode,
    onSubmit,
    fetchUser,
  } = useEditUser(history.location.state);

  useEffect(() => {
    if (!isAddMode) {
    fetchUser(id);
    }
  }, []);

  return (
    <Formik
      initialValues={isAddMode ? initialValues : user}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched }) => {
        return (
          <Form className="upsertforms">
            <h1>{isAddMode ? "Add User" : "Edit User"}</h1>

            <label>First Name</label>
            <Field
              name="firstName"
              className={
                "form-control" +
                (errors.firstName && touched.firstName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="firstName"
              component="div"
              className="invalid-feedback"
            />

            <label>Last Name</label>
            <Field
              name="lastName"
              type="text"
              className={
                "form-control" +
                (errors.lastName && touched.lastName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="lastName"
              component="div"
              className="invalid-feedback"
            />

            <label>Email</label>
            <Field
              name="email"
              type="text"
              disabled={!isAddMode}
              className={
                "form-control" +
                (errors.email && touched.email ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="email"
              component="div"
              className="invalid-feedback"
            />

            <label>Identification number</label>
            <Field
              name="identificationNumber"
              type="text"
              className={
                "form-control" +
                (errors.identificationNumber && touched.identificationNumber
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="identificationNumber"
              component="div"
              className="invalid-feedback"
            />

            <label>Mobile number</label>
            <Field
              name="mobileNumber"
              className={
                "form-control" +
                (errors.mobileNumber && touched.mobileNumber
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="mobileNumber"
              component="div"
              className="invalid-feedback"
            />
            <div className="pt-3">
              <button
                type="submit"
                className="btn btn-primary"
              >
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
