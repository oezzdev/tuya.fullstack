import api from "../../../services/api";

export const login = async (username: string, password: string) => {
  const response = await api.post(`/users/auth`, { username, password });
  if (response.data.token) {
    localStorage.setItem('user_token', response.data.token);
  }
  return response.data;
};