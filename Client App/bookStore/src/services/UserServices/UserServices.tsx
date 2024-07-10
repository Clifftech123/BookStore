import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

interface UserServiceInterface {
  [x: string]: any;
  name?: string;
  email?: string;
  token?: string;
}

interface LoginInterface {
  email: string;
  password: string;
}

interface RegisterInterface {
  name: string;
  email: string;
  password: string;
}

export const userApi = createApi({
  reducerPath: 'userApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:5052/',
    prepareHeaders: (headers) => {
      const token = localStorage.getItem('token');
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
      return headers;
    },
  }),

  // Define the tagTypes property to specify the type of data returned by the endpoints.
  tagTypes: ['User'],
  endpoints: (builder) => ({
    loginUser: builder.mutation<UserServiceInterface, LoginInterface>({
      query: (credentials) => ({
        url: 'login',
        method: 'POST',
        body: credentials, // Directly pass the credentials object as the request body
      }),
    }),

    // Define the registerUser endpoint to register a new user.
    registerUser: builder.mutation<UserServiceInterface, RegisterInterface>({
      query: (newUser) => ({
        url: 'register',
        method: 'POST',
        body: newUser, 
      }),
    }),
  }),
});

// Export hooks for usage in functional components
export const {
  useLoginUserMutation,
  useRegisterUserMutation,
} = userApi;
