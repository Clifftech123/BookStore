
import { useLogin } from "../../hooks/useLogin.hook";
import { Link } from "react-router-dom";


const AuthComponent = () => {
 const { handleChange, handleSubmit, formData } = useLogin();
 
    
  return (
    <main>

      <section className="w-full h-screen  flex flex-col items-center justify-center px-4">
      <div className="max-w-sm w-full bg-gray-5 py-10   border p-2 shadow text-gray-600 space-y-8">
          <div className="text-center">
             
              <div className="mt-5 space-y-2">
                  <h3 className="text-gray-800 text-2xl font-bold sm:text-3xl">Log in to your account</h3>
                  <p className="">Don't have an account? <Link  to="/register"
    
                   type="button"
                  className="font-medium text-indigo-600 hover:text-indigo-500">Sign up</Link></p>
              </div>
          </div>
          <form
             onSubmit={handleSubmit}
                className="  py-6 sm:p-6 sm:rounded-lg "
             
          >
            {/*  Login email */}
              <div>
                  <label className="font-medium">
                      Email
                  </label>
                  <input
                    onChange={handleChange}
                       name="email"
                      type="email"
                      value={formData.email}
                      className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                  />
              </div>

              {/* Loging password */}

                 <div>
                 <label className="font-medium">
                                Password
                            </label>
                            <input
                             onChange={handleChange}
                                type="password"
                                 name="password"
                                 value={formData.password}
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                            />
                        </div>
                {/* Login button */}
              <button
                  className="w-full mt-4 px-4 py-2 text-white font-medium bg-indigo-600 hover:bg-indigo-500 active:bg-indigo-600 rounded-lg duration-150"
              >
                  Sign in
              </button>
          </form>
        
      
      </div>
  </section>
  
    
  </main>


 



  )
}

export default AuthComponent