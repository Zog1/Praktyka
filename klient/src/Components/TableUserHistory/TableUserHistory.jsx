import React, { useState } from "react";
import { useTable, useFilters, useSortBy } from "react-table";
import { Table as BootstrapTable, Input } from "reactstrap";

export default function TableUserHistory({ columns, data }) {
  const [filter, setFilterBy] = useState({
    rentalDate: "",
    returnDate: "",
    registrationNumber: "",
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
    useSortBy
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
              value={filter.rentalDate}
              onChange={filterBy}
              placeholder={"Search by rental date"}
              name="rentalDate"
            />
          </td>
          <td>
            <Input
              value={filter.returnDate}
              onChange={filterBy}
              placeholder={"Search by return date"}
              name="returnDate"
            />
          </td>
          <td>
            <Input
              value={filter.registrationNumber}
              onChange={filterBy}
              placeholder={"Search by registration number"}
              name="registrationNumber"
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
