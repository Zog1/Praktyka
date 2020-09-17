import React, { useState } from "react";
import { useTable, useFilters, useSortBy, usePagination } from "react-table";
import { Table as BootstrapTable, Input } from "reactstrap";

export default function CarManagerTable({ columns, data }) {
  
  const [filter, setFilterBy] = useState({
    carId: "",
    registrationNumber: "",
    model: "",
    brand: "",
    imagePath: "",
    yearOfProduction: "",
  });

  const filterBy = (e) => {
    const value = e.target.value || undefined;
    setFilter(e.target.name, value);
    setFilterBy({
      ...filter,
      [e.target.name]: e.target.value,
    });
  };

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
    setFilter,
  } = useTable(
    {
      columns,
      data,
    },
    useFilters,
    useSortBy,
    usePagination
  );

  return (
    <BootstrapTable striped {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                {column.render("Header")}
              </th>
            ))}
          </tr>
        ))}
        <tr>
          <td>
            <Input
              value={filter.carId}
              onChange={filterBy}
              placeholder={"Search by carId"}
              name="carId"
            />
          </td>
          <td>
            <Input
              value={filter.registrationNumber}
              onChange={filterBy}
              placeholder={"Search by registrationNumber"}
              name="registrationNumber"
            />
          </td>
          <td>
            <Input
              value={filter.brand}
              onChange={filterBy}
              placeholder={"Search by brand"}
              name="brand"
            />
          </td>
          <td>
            <Input
              value={filter.model}
              onChange={filterBy}
              placeholder={"Search by model"}
              name="model"
            />
          </td>
          <td>
            <Input
              value={filter.urlToImg}
              onChange={filterBy}
              placeholder={"Search by urlToImg"}
              name="imagePath"
            />
          </td>
          <td>
            <Input
              value={filter.yearOfProduction}
              onChange={filterBy}
              placeholder={"Search by year of production"}
              name="yearOfProduction"
            />
          </td>
        </tr>
      </thead>
      <tbody {...getTableBodyProps()}>
        {rows.map((row, i) => {
          prepareRow(row);
          return (
            <tr {...row.getRowProps()}>
              {row.cells.map((cell) => {
                return (
                  <td {...cell.getCellProps()}> {cell.render("Cell")} </td>
                );
              })}
            </tr>
          );
        })}
      </tbody>
    </BootstrapTable>
  );
}
