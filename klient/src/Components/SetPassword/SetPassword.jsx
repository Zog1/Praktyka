import React, {useState} from "react";
import { useParams, useHistory } from "react-router-dom";
import Swal from "sweetalert2";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import Api from "../API/AuthorizationApi"
import Loader from "react-loader-spinner"

export default function SetPassword() {

  const { code } = useParams();
  let history = useHistory()
  const [isSended, setIsSended]=useState(false)

  let initialValues = {
    encodePassword: '',
    confirmEncodePassword: '',
    codeofverification: code
  }

  const validationSchema = Yup.object().shape({
    encodePassword: Yup.string()
      .min(8, "Too short!")
      .required("Required"),
      confirmEncodePassword: Yup.string()
      .min(8, "Too short!")
      .required("Required")
      .oneOf([Yup.ref('encodePassword'),null], 'Passwords must match')
  });


  function onSubmit(fields) {
    if (fields.encodePassword===fields.confirmEncodePassword) {
      sendPassword(fields);
      setIsSended(true)
    }
    else{
      Swal.fire("Oops...", "Try again password weren't same", "error");
    }
  }

  async function sendPassword(fields) {
    try {
      let api=new Api()
      const res=await api.sendPassword(fields)
      setIsSended(res)
      if(res===false)
      {
        throw new Error()
      }
      history.push('/')
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={(fields, {resetForm}) =>
      {
        onSubmit(fields)
        resetForm({fields: ''})
        }
        }
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form id="setPasswordForm">
            <h1>Set your password</h1>
            <label>Password</label>
            <Field
              name="encodePassword"
              type="password"
              className={
                "form-control" +
                (errors.encodePassword && touched.encodePassword ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="encodePassword"
              component="div"
              className="invalid-feedback"
            />

            <label>Confirm password</label>
            <Field
              name="confirmEncodePassword"
              type="password"
              className={
                "form-control" +
                (errors.confirmEncodePassword && touched.confirmEncodePassword
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="confirmEncodePassword"
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
                Set password
              </button>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}
