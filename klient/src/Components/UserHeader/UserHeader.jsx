import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/NavBar";
import logo from "../img/euvic-logo.PNG";

const UserHeader = () => {
  return (
    <Navbar expand="lg" className="shadow-lg p-3 mb-5 bg-white rounded">
      <Navbar.Brand href="/home">
        <img src={logo} alt="euvic-logo"></img>
      </Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav" className="mr-auto">
        <Nav className="ml-auto">
          <Nav.Link href="/reserve-car">Reserve car</Nav.Link>
          <Nav.Link href="/history">Reservations</Nav.Link>
          <Nav.Link onClick={()=>{
        sessionStorage.clear()
        window.location.assign('/')
        }}>Log out</Nav.Link>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
};
export default UserHeader;
