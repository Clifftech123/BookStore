import { configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import userReducer from "../features/UserSlice";
import { userApi } from "../services/UserServices/UserServices";
import { bookApi } from "../services/BookServices/BookServices";
import { categoryApi } from "../services/CategoryServices/CategoryServices";

// Configuring the Redux store with reducers and middleware
export const store = configureStore({
    reducer: {
      user: userReducer, 
      [userApi.reducerPath]: userApi.reducer, 
      [bookApi.reducerPath]: bookApi.reducer,
      [categoryApi.reducerPath]: categoryApi.reducer,

    },
     
   // Adjusting middleware type to match expected signature
middleware: (getDefaultMiddleware) =>
  getDefaultMiddleware<{ serializableCheck: false }>().concat(userApi.middleware,

    bookApi.middleware,  categoryApi.middleware,
  )
  });


  // Type definitions for Redux store dispatch and state
  export type AppDispatch = typeof store.dispatch;
  export type RootState = ReturnType<typeof store.getState>;
  
  // Setting up automatic cache invalidation and refetching for RTK Query
  setupListeners(store.dispatch);