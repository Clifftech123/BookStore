
import { Route, Routes } from 'react-router-dom'
import AuthPage from '../../pages/authPage/auth'
import RegisterPage from '../../pages/authPage/RegisterPage'
import { CategoryPage } from '../../pages/category/CategoryPage'
import ProtectedRoute from './ProtectedRout'
import CreateBookPage from '../../pages/Book/CreatebookPage'
import DashboardPage from '../../pages/Book/dashboard'
import CreatePage from '../../pages/category/createPage'



 const GlobalRouteComponent = () => {

    return (
      <Routes>
        <Route path="/" element={<AuthPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/get-all-category" element={<CategoryPage />} />
          <Route path="/create-book" element={<CreateBookPage />} />
          <Route path="/create-category" element={< CreatePage/>} />
        </Route>
      </Routes>
    );
  };




export default GlobalRouteComponent