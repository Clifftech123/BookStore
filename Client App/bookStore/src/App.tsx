import { ToastContainer } from "react-toastify"
import GlobalRouteComponent from "./component/routes/GlobalRoute"
import 'react-toastify/dist/ReactToastify.css';
import { useDispatch } from "react-redux";
import { setToken } from "./features/UserSlice";
import { useEffect } from "react";

function App() {
  const dispatch = useDispatch();

  useEffect(() => {

    const token = localStorage.getItem('token');

    if (token) {
      dispatch(setToken(token));
    }

  }, [dispatch]);


  return (
     <> 
     <section>
     <GlobalRouteComponent/>
      </section>   
     
    <section>
    <ToastContainer />
    </section>
  
      </>
  )
}

export default App
