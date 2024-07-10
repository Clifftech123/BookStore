import { useState } from "react";
import { Category, useCreateCategoryMutation } from "../../services/CategoryServices/CategoryServices";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";


const  CreateCategoryComponent = () => {
    const navigate = useNavigate();
    const [createCategory] = useCreateCategoryMutation();

    const [formData, setFormData] = useState<Omit<Category, 'id'>>({
        name: '',
        description: '',
      });
      
      const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
      };
      
    
      const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
          await createCategory(formData).unwrap();
         toast.success('Category created successfully');
          setFormData({ name: '', description: '' });
          navigate('/get-all-category');
        } catch (error) {
          toast.error('Error creating category:');
          console.error('Error creating category:', error);
        }
      };
        
  return (
    <main className="relative  bg-white">
    <div className="relative z-10 max-w-screen-xl mx-auto text-gray-600 sm:px-4 md:px-8">
        <div className="max-w-lg space-y-3 px-4 sm:mx-auto sm:text-center sm:px-0">
            <p className="text-white text-3xl font-semibold sm:text-4xl">
                Create Category
            </p>
            <p className="text-gray-700">
                file This form to create a new category.
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
                    name="description"
                    value={formData.description}
                    onChange={handleInputChange}
                     required className="w-full mt-2 h-36 px-3 py-2 resize-none appearance-none bg-transparent outline-none border focus:border-gray-800 shadow-sm rounded-lg"></textarea>
                </div>
                
               
         
                <button
                    className="w-full px-4 py-2 text-white font-medium bg-gray-800 hover:bg-gray-700 active:bg-gray-900 rounded-lg duration-150"
                >
                   create 
                </button>
            </form>
        </div>
    </div>
    
</main>
  )
}

export default CreateCategoryComponent