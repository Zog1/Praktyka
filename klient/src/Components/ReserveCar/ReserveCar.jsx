import React, { useEffect } from "react";
import { Col, Form, Input, Row, Button, Label, Container } from "reactstrap";
import Loader from "react-loader-spinner";
import moment from "moment";
import useReserveCar from "./ReserveCar.utils";

export default function ReserveCar() {
  const {
    data,
    filters,
    isLoading,
    fetchCars,
    handleChange,
    createCarCard,
    checkAvailability,
    filteredCarCard
  } = useReserveCar();

  useEffect(() => {
    fetchCars();
  }, []);

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <Container fluid>
          <Row>
            <Col sm={3}>
              <Form className="text-center p-2 mb-5 shadow-lg rounded">
                <Label size="md">Search car by:</Label>
                <Input
                  type="text"
                  placeholder="Brand"
                  name="brand"
                  value={filters.brand}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="text"
                  placeholder="Model"
                  name="model"
                  value={filters.model}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="text"
                  placeholder="Registration number"
                  name="registrationNumber"
                  value={filters.registrationNumber}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="text"
                  placeholder="Minimum year of production"
                  name="yearOfProduction"
                  value={filters.yearOfProduction}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="text"
                  placeholder="Minimum number of doors"
                  name="numberOfDoor"
                  value={filters.numberOfDoor}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="text"
                  placeholder="Minimum sits places"
                  name="numberOfSits"
                  value={filters.numberOfSits}
                  onChange={handleChange}
                  className="mb-1"
                />
                <Input
                  type="date"
                  min={moment().format("YYYY-MM-DD")}
                  onChange={handleChange}
                  name="startdate"
                  className="mb-1"
                />
                <Input
                  type="date"
                  min={moment().format("YYYY-MM-DD")}
                  onChange={handleChange}
                  name="enddate"
                  className="mb-1"
                />
                <Button
                  color="success"
                  className="mb-1"
                  disabled={filters.startdate === '' || filters.enddate === ''}
                  onClick={() =>
                    checkAvailability(filters.startdate, filters.enddate)
                  }
                >
                  Search
                </Button>
              </Form>
            </Col>
                {data && data.length!==0 ? filteredCarCard(data).map(createCarCard) : "We don't have cars in this term"}
          </Row>
          </Container>
      )}
    </div>
  );
}
