import { useState } from "react";
import { CreateBookPayload, useCreateBookMutation } from "../../services/BookServices/BookServices";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

const  CreateBookComponent = () => {

    const navigate = useNavigate();
    const [createBook] = useCreateBookMutation();
    const [formData, setFormData] = useState<CreateBookPayload>({
        name: '',
        description: '',
        price: 0,
        categoryId: 0,
      });
      
      const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
      };
      


     
      const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
          await createBook(formData).unwrap();
          toast.success("Book created successfully check list");
           navigate("/dashboard");
          setFormData({
            name: '',
            description: '',
            price: 0,
            categoryId: 0,
          });
        } catch (error) {
          console.error("Error creating book:", error);
        }
      };
      

    return (
        <main className="relative py-28 bg-gray-900">
            <div className="relative z-10 max-w-screen-xl mx-auto text-gray-600 sm:px-4 md:px-8">
                <div className="max-w-lg space-y-3 px-4 sm:mx-auto sm:text-center sm:px-0">
                    <p className="text-white text-3xl font-semibold sm:text-4xl">
                        Create Book
                    </p>
                    <p className="text-gray-300">
                     file This form to create a new book.
                    </p>
                </div>
                <div className="mt-12 mx-auto px-4 p-8 bg-white sm:max-w-lg sm:px-8 sm:rounded-xl">
                    <form
                      onSubmit={handleSubmit}
                        className="space-y-5"
                    >

                        {/* Name */}
                        <div>
                            <label className="font-medium">
                              Name
                            </label>
                            <input
                                type="text"
                                required
                                name="name"
                                value={formData.name}
                                onChange={handleInputChange}
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-gray-800 shadow-sm rounded-lg"
                            />
                        </div>

                        {/*   Descriptions   */}
                        <div>
                            <label className="font-medium">
                               Descriptions 
                            </label>
                            <textarea
                             value={formData.description}
                             onChange={handleInputChange}
                            name="description"
                             required className="w-full mt-2 h-36 px-3 py-2 resize-none appearance-none bg-transparent outline-none border focus:border-gray-800 shadow-sm rounded-lg"></textarea>
                        </div>
                        
                        {/*   Price   */}
                        <div>
                            <label className="font-medium">
                               price
                            </label>
                            <input
                                type="number"
                                required
                                name="price"
                                value={formData.price}
                                onChange={handleInputChange}
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-gray-800 shadow-sm rounded-lg"
                            />
                        </div>
                        <div>
                            <label className="font-medium">
                            categoryId
                            </label>
                            <input
                                type="number"
                                required
                                name="categoryId"
                                value={formData.categoryId}
                                onChange={handleInputChange}
                                className="w-full mt-2 px-3 py-2 text-gray-500 bg-transparent outline-none border focus:border-gray-800 shadow-sm rounded-lg"
                            />
                        </div>
                       
                 
                        <button
                            className="w-full px-4 py-2 text-white font-medium bg-gray-800 hover:bg-gray-700 active:bg-gray-900 rounded-lg duration-150"
                        >
                           create
                        </button>
                    </form>
                </div>
            </div>
            <div className='absolute inset-0 blur-[118px] max-w-lg h-[800px] mx-auto sm:max-w-3xl sm:h-[400px]' style={{ background: "linear-gradient(106.89deg, rgba(192, 132, 252, 0.11) 15.73%, rgba(14, 165, 233, 0.41) 15.74%, rgba(232, 121, 249, 0.26) 56.49%, rgba(79, 70, 229, 0.4) 115.91%)" }}></div>
        </main>
    )
}



export default CreateBookComponent;