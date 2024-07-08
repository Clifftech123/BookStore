import { useState } from "react";


const AuthComponent = () => {
 const [showForm, setShowForm] = useState('login');
 
    
  return (
    <main>
    {showForm === 'login' ? (
        // Show login form
      <section className="w-full h-screen flex flex-col items-center justify-center px-4">
      <div className="max-w-sm w-full text-gray-600 space-y-8">
          <div className="text-center">
             
              <div className="mt-5 space-y-2">
                  <h3 className="text-gray-800 text-2xl font-bold sm:text-3xl">Log in to your account</h3>
                  <p className="">Don't have an account? <button 
                   onClick={() => setShowForm('signup')}
                   type="button"
                  className="font-medium text-indigo-600 hover:text-indigo-500">Sign up</button></p>
              </div>
          </div>
          <form
              onSubmit={(e) => e.preventDefault()}
          >
            {/*  Login email */}
              <div>
                  <label className="font-medium">
                      Email
                  </label>
                  <input
                      type="email"
                      required
                      className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                  />
              </div>

              {/* Loging password */}

                 <div>
                 <label className="font-medium">
                                Password
                            </label>
                            <input
                                type="password"
                                required
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
        
          <div className="text-center">
              <a href="javascript:void(0)" className="text-indigo-600 hover:text-indigo-500">Forgot password?</a>
          </div>
      </div>
  </section>
  
    ) : (

    //  Show signup form 
    <section>
      <main className="w-full h-screen flex flex-col items-center justify-center bg-gray-50 sm:px-4">
            <div className="w-full space-y-6 text-gray-600 sm:max-w-md">
                <div className="text-center">
                
                {/*  Heading test  */}
                    <div className="mt-5 space-y-2">
                        <h3 className="text-gray-800 text-2xl font-bold sm:text-3xl">Create an account</h3>
                        <p className="">Already have an account? <button 
                         type="button"
                         onClick={() => setShowForm('login')}
                         
                        className="font-medium text-indigo-600 hover:text-indigo-500">Log in</button></p>
                    </div>
                </div>


                {/*  Form  */}
                <div className="bg-white shadow p-4 py-6 sm:p-6 sm:rounded-lg">
                    <form
                        onSubmit={(e) => e.preventDefault()}
                        className="space-y-5"

                    >

                        {/* sing up name  */}
                        <div>
                            <label className="font-medium">
                                Name
                            </label>
                            <input
                                type="text"
                                name="name"
                                required
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                            />
                        </div>

                        {/* sing up email  */}
                        <div>
                            <label className="font-medium">
                                Email
                            </label>
                            <input
                                type="email"
                                required
                                name="email"
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                            />
                        </div>

                        {/* sing up password  */}
                        <div>
                            <label className="font-medium">
                                Password
                            </label>
                            <input
                                type="password"
                                required
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                            />
                        </div>
                        <button
                            className="w-full px-4 py-2 text-white font-medium bg-indigo-600 hover:bg-indigo-500 active:bg-indigo-600 rounded-lg duration-150"
                        >
                            Create account
                        </button>
                    </form>
                
                </div>
            </div>
        </main>
    </section>
    )}
  </main>


 



  )
}

export default AuthComponent