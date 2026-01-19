import axios from 'axios';

const API_URL = 'https://localhost:7034/api/users';

export const login = async (username: string, password: string) => {
  const response = await axios.post(`${API_URL}/auth`, { username, password });
  if (response.data.token) {
    localStorage.setItem('user_token', response.data.token);
  }
  return response.data;
};