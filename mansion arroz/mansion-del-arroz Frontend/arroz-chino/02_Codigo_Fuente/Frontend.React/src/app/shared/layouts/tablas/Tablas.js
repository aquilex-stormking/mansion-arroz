import React from "react";
import { Pagination } from "@mui/material";

export default function Tablas(props) {
  const hanbleChangePage = (event, value) => {
    props.hanbleChangePage(value);
  };

  return (
    <>
      <div className="col-12 table-responsive mt-4">
        <table className="table">
          <thead>
            <tr>
              {props.headers.map((item, index) => {
                return <th key={index}>{item}</th>;
              })}
            </tr>
          </thead>
          {props.children}
          {props.sizeData === 0 ? (
            <tfoot>
              <tr>
                <td>No hay información</td>
              </tr>
            </tfoot>
          ) : null}
        </table>
      </div>
      <div className="col-12 px-0 py-3 d-flex justify-content-end">
        <Pagination
          color="primary"
          showLastButton
          showFirstButton
          onChange={hanbleChangePage}
          count={Math.ceil(props.sizeData / props.itemsPerPage)}
        />
      </div>
    </>
  );
}
