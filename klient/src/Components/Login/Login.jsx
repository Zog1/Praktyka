import React from "react";
import "../styles/componentsstyle.css";
import { Formik, Field, Form, ErrorMessage } from "formik";
import { Link, Redirect } from "react-router-dom";
import useLogin from "./Login.utils";
import Loader from "react-loader-spinner";

export default function Login() {
  const { initialValues, isSended, validationSchema, onSubmit } = useLogin();

  return (
    <div>
      {sessionStorage.getItem("isLoggedIn") === null ? (
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          enableReinitialize
          onSubmit={onSubmit}
        >
          {({ errors, touched }) => {
            return (
              <Form className="upsertforms" id="loginform">
                <h1>Sign in</h1>
                <img
                  src="https://image.flaticon.com/icons/svg/3190/3190448.svg"
                  alt="secure"
                  height="150"
                  width="150"
                />
                <label>Email</label>
                <Field
                  name="email"
                  type="email"
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

                <label>Password</label>
                <Field
                  name="password"
                  type="password"
                  className={
                    "form-control" +
                    (errors.password && touched.password
                      ? " is-invalid"
                      : "")
                  }
                />
                <ErrorMessage
                  name="password"
                  component="div"
                  className="invalid-feedback"
                />

                <div className="pt-3">
                  <Link to={"."} className="btn btn-link">
                    Cancel
                  </Link>
                  <button type="submit" className="btn btn-primary" disabled={isSended}>
                     {isSended && <Loader type="Oval" color="#00BFFF" height="20px" width="20px"/>}
                    Log in
                  </button>
                </div>
              </Form>
            );
          }}
        </Formik>
      ) : (
        <Redirect to="/home" />
      )}
    </div>
  );
}
