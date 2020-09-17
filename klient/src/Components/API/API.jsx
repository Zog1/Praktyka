import axios from "axios";

export default class Api {
  constructor() {
    this.baseAxios = axios.create({
      baseURL: process.env.REACT_APP_BASE_URL,
    });

    // Request interceptor
    this.baseAxios.interceptors.request.use(
      (config) => {
        const token = sessionStorage.getItem("accessToken");
        return token
          ? {
              ...config,
              headers: {
                ...config.headers,
                Authorization: `Bearer ${token}`,
                ContentType: "application/json",
              },
            }
          : config;
      },
      (error) => {
        Promise.reject(error);
      }
    );

    //Response interceptor
    this.baseAxios.interceptors.response.use(
      (response) => {
        return response;
      },
      async (error) => {
        const originalRequest = error.config;

        if (
          (error.response.status === 401 || error.response.status === 403) &&
          originalRequest.url === "/refresh"
        ) {
          sessionStorage.clear()
          window.location.assign('/')
          return Promise.reject(error);
        }

        if (
          (error.response.status === 401 || error.response.status === 403) &&
          !originalRequest._retry
        ) {
          originalRequest._retry = true;
          const refreshToken = sessionStorage.getItem("refreshToken");
          const res = await this.baseAxios.post("/refresh", {
            RefreshToken: refreshToken,
          });
          if (res.status === 200) {
            sessionStorage.setItem("accessToken", res.data.accessToken);
            sessionStorage.setItem("refreshToken", res.data.refreshToken);
          /*  this.baseAxios.defaults.headers.common["Authorization"] =
              "Bearer " + sessionStorage.setItem("accessToken");*/
              return this.baseAxios(originalRequest); 
          } else {
            return error;
          }
        }
        return Promise.reject(error);
      }
    );
  }
}
