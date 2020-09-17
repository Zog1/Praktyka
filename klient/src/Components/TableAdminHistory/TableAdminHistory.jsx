import React, { useState } from "react";
import { useTable, useSortBy, useGlobalFilter } from "react-table";
import { Table as BootstrapTable, Input } from "reactstrap";
import {Col,Row} from "reactstrap"

export default function TableAdminHistory({ columns, data }) {
  
  const [globalFilter, setGlobalFilters]=useState()

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    setGlobalFilter,
    prepareRow,
  } = useTable(
    {
      columns,
      data,
    },
    useGlobalFilter,
    useSortBy
  );

  return (
    <div>
    <Row className="justify-content-end">
    <Col lg={3}>
        <Input
        value={globalFilter || ""}
        onChange={e => {
          setGlobalFilters(e.target.value || undefined)
          setGlobalFilter(e.target.value || undefined)
        }}
        placeholder={`Search all...`}
      />
      </Col>
      </Row>
    <BootstrapTable striped {...getTableProps()} className="mt-2">
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                {column.render("Header")}
              </th>
            ))}
          </tr>
        ))}
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
    </div>
  );
}
