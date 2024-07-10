import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

interface Book {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: number;
  category?: string;
  createdDate?: string;
  updatedDate?: string;
}

export interface CreateBookPayload {
  name: string;
  description: string;
  price: number;
  categoryId: number;

}

interface UpdateBookPayload extends CreateBookPayload {
  id: number;
}

export const bookApi = createApi({
  reducerPath: 'bookApi',
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
  tagTypes: ['Book'],
  endpoints: (builder) => ({
    getBooks: builder.query<Book[], void>({

        // Get all books
      query: () => 'books',
      providesTags: ['Book'],
    }),


    // Get Book by id
    getBookById: builder.query<Book, number>({
      query: (id) => `books/${id}`,
      providesTags: ['Book'],
    }),

    // Create Book
    createBook: builder.mutation<Book, CreateBookPayload>({
      query: (book) => ({
        url: 'books',
        method: 'POST',
        body: book,
      }),
      invalidatesTags: ['Book'],
    }),

    // Update Book
    updateBook: builder.mutation<Book, UpdateBookPayload>({
      query: (book) => ({
        url: `books`,
        method: 'PUT',
        body: book,
      }),
      invalidatesTags: ['Book'],
    }),

    // Delete Book
    deleteBook: builder.mutation<void, number>({
      query: (id) => ({
        url: `books/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Book'],
    }),

    // Search Books
    searchBooks: builder.query<Book[], string>({
      query: (searchTerm) => `books/search/${searchTerm}`,
      providesTags: ['Book'],
    }),
  }),
});

export const {
  useGetBooksQuery,
  useGetBookByIdQuery,
  useCreateBookMutation,
  useUpdateBookMutation,
  useDeleteBookMutation,
  useSearchBooksQuery,
} = bookApi;
