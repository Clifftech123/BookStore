
import { Route, Routes } from 'react-router-dom'
import AuthPage from '../../pages/authPage/atuh'
import DashboardPage from '../../pages/dashborad/dashboard'



 const GlobalRouteComponent = () => {
  return (
   <Routes>
     <Route path="/" element={<AuthPage/>}/>
     <Route path="/dashboard" element={<DashboardPage/>}/>
   </Routes>
  )
}


export default GlobalRouteComponent