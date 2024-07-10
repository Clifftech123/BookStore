
import axios from 'axios';

const API = axios.create({
  baseURL: 'https://localhost:5052/',
  headers: {
    'Content-Type': 'application/json', 
    Accept: 'application/json', 
  },
});

export default API;