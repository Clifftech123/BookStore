import { Navigate, Outlet } from "react-router-dom";
import { useAppSelector } from "../../app/hook";

const ProtectedRoute = () => {
  const { token } = useAppSelector((state) => state.user);
  // Ensure the token is retrieved from localStorage if not present in Redux state
  const effectiveToken = token || localStorage.getItem('token');

  return effectiveToken ? <Outlet /> : <Navigate to="/" />;
};

export default ProtectedRoute;