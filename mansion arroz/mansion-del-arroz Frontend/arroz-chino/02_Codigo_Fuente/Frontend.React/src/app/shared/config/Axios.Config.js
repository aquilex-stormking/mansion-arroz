import axios from "axios";

const baseURL = process.env.REACT_APP_API_URL;

const http = axios.create({
  baseURL: baseURL,
  timeout: 600000,
  headers: { "X-Requested-With": "XMLHttpRequest" },
});

export default http;
