import React from "react"
import Nav from 'react-bootstrap/Nav'
import NavDropdown from 'react-bootstrap/NavDropdown'
import Navbar from 'react-bootstrap/NavBar'
import logo from '../img/euvic-logo.PNG';

const AdminHeader = ()=>{

    return (
<Navbar expand="lg" className="shadow-lg p-3 mb-5 bg-white rounded">
  <Navbar.Brand href="/admin"><img src={logo} alt='euvic-logo'style={{height: 85 + 'px'}}></img></Navbar.Brand>
  <Navbar.Toggle aria-controls="basic-navbar-nav" />
  <Navbar.Collapse id="basic-navbar-nav" className="ml-auto">
    <Nav className="ml-auto">
      <Nav.Link href="/reserve-car">Reserve car</Nav.Link>
      <Nav.Link href="/admin/user-history">Reservations</Nav.Link>
      <NavDropdown title="Add" id="basic-nav-dropdown">
        <NavDropdown.Item href="/admin/add-car">Car</NavDropdown.Item>
        <NavDropdown.Item href="/admin/add-user">User</NavDropdown.Item>
      </NavDropdown>
      <NavDropdown title="Manager tables" id="basic-nav-dropdown">
        <NavDropdown.Item href="/admin/user-manager">User</NavDropdown.Item>
        <NavDropdown.Item href="/admin/car-manager">Cars</NavDropdown.Item>
        <NavDropdown.Item href="/admin/admin-history">History</NavDropdown.Item>
        <NavDropdown.Item href="/admin/defects-manager">Defects</NavDropdown.Item>
      </NavDropdown>
      <Nav.Link onClick={()=>{
        sessionStorage.clear()
        window.location.assign('/')
        }}>Log out</Nav.Link>
    </Nav>
  </Navbar.Collapse>
</Navbar>)

}
export default AdminHeader