import { useState } from "react";

export function useTable(initState) {
  const [itemsPerPage, setItemsPerPage] = useState(8);
  const [currentPage, setCurrentPage] = useState(0);
  const [data, setData] = useState(initState);

  const paginateData = () => {
    return data.slice(currentPage, currentPage + itemsPerPage);
  };

  const hanbleChangePage = (value) => {
    if (value === 1) {
      setCurrentPage(0);
      return;
    }
    setCurrentPage((value - 1) * itemsPerPage);
  };

  return {
    data,
    setData,
    itemsPerPage,
    paginateData,
    setItemsPerPage,
    hanbleChangePage,
  };
}
