import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../app/store';

interface UserState {
  name: string | null;
  email: string | null;
  password: string | null;
  token: string | null;
}

const initialState: UserState = {
  name: null,
  email: null,
  password: null,
  token: null,
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {

    // Set user for registration
    setUserForRegistration: (
      state,
      action: PayloadAction<{ name: string; email: string; password: string }>
    ) => {
      state.name = action.payload.name;
      state.email = action.payload.email;
      state.password = action.payload.password;
    },

    // Set user for login
    setUserForLogin: (
      state,
      action: PayloadAction<{ email: string; password: string }>
    ) => {
      state.email = action.payload.email;
      state.password = action.payload.password;
    },

       //  set token for user
       setToken: (state, action: PayloadAction<string>) => {
        state.token = action.payload
      },
  
      setUserProfile: (state, action: PayloadAction<{ name: string }>) => {
        state.name = action.payload.name;

      },

    // Logout user
    logoutUser: (state) => {
      state.name = null;
      state.email = null;
      state.password = null;
      state.token = null;

        // Remove token from localStorage
       localStorage.removeItem('token');
    },
  },
});

export const selectUser = (state: RootState) => state.user;

export const { setUserForRegistration, setUserForLogin, logoutUser,setToken, setUserProfile } = userSlice.actions;
export default userSlice.reducer;
