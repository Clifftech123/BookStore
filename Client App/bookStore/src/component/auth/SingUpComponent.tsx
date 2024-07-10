import { Link } from "react-router-dom"
import { useRegister } from "../../hooks/useRegister.hook";


const SingUpComponent = () => {

    const { handleChange, handleSubmit, registerData } =  useRegister();
  return (
    <section>
      <main className="w-full h-screen flex flex-col items-center justify-center bg-gray-50 sm:px-4">
            <div className="w-full space-y-6 text-gray-600 sm:max-w-md">
                <div className="text-center">
                
                {/*  Heading test  */}
                    <div className="mt-5 space-y-2">
                        <h3 className="text-gray-800 text-2xl font-bold sm:text-3xl">Create an account</h3>
                        <p className="">Already have an account?<Link to="/"> <button 
                         type="button"
                        className="font-medium text-indigo-600 hover:text-indigo-500">
                            
                            Log in
                            </button></Link></p>
                    </div>
                </div>


                {/*  Form  */}
                <div className="bg-white shadow p-4 py-6 sm:p-6 sm:rounded-lg">
                    <form
                      onSubmit={handleSubmit}
                        className="space-y-5"

                    >

                             <small className="text-red-800">
                              
                              No space your name
                             </small>

                        {/* sing up name  */}
                        <div>
                            
                            <label className="font-medium">
                                Name
                            </label>
                            
                            <input
                                type="text"
                                 name="name"
                                 value={registerData.name}
                                required
                                onChange={handleChange}
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
                                 value={registerData.email}
                                onChange={handleChange}
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-indigo-600 shadow-sm rounded-lg"
                            />
                        </div>

                        {/* sing up password  */}
                        <small className=" text-red-600 mt-3">
                            your password must include numeric 
                        </small>
                        <div>
                            <label className="font-medium">
                                Password
                            </label>
                            <input
                                type="password"
                                required
                                name="password"
                                value={registerData.password}
                                onChange={handleChange}
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
  )
}

export default SingUpComponent