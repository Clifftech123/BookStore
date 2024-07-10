import { toast } from "react-toastify";
import { useDeleteCategoryMutation, useGetCategoriesQuery } from "../../services/CategoryServices/CategoryServices";
import { Link } from "react-router-dom";

const CategoryCardComponent = () => {
    const { data: categoryData } = useGetCategoriesQuery();
    const [deleteCategory, { isLoading: isDeleting }] = useDeleteCategoryMutation();
    console.log(categoryData);
    

    const handleDeleteCategory = async (id: number) => {
        try {
            await deleteCategory(id).unwrap();
            toast.success('Category deleted successfully');
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };

    return (
        <div className="max-w-screen-xl mx-auto px-4 md:px-8">
            <div className="items-start justify-between md:flex">
                <div className="max-w-lg">
                    <h3 className="text-gray-800 text-xl font-bold sm:text-2xl">
                        Category List
                    </h3>
                    <p className="text-gray-600 mt-2">
                        Browse our collection of categories.
                    </p>
                </div>
                <div className="mt-3 md:mt-0">
                    <Link
                        to="/create-category"
                        className="inline-block px-4 py-2 text-white duration-150 font-medium bg-indigo-600 rounded-lg hover:bg-indigo-500 active:bg-indigo-700 md:text-sm"
                    >
                        Add Category
                    </Link>
                </div>
            </div>
            <div className="mt-12 shadow-sm border rounded-lg overflow-x-auto">
                <table className="w-full table-auto text-sm text-left">
                    <thead className="bg-gray-50 text-gray-600 font-medium border-b">
                        <tr>
                            <th className="py-3 px-6">Category Name</th>
                            <th className="py-3 px-6">Description</th>
                            <th className="py-3 px-6"></th>
                        </tr>
                    </thead>
                    <tbody className="text-gray-600 divide-y">
                        {categoryData?.map((category, idx) => (
                            <tr key={idx}>
                                <td className="px-6 py-4 whitespace-nowrap">{category.name}</td>
                                <td className="px-6 py-4 whitespace-nowrap">{category.description}</td>
                                <td className="text-right px-6 whitespace-nowrap">
                                    <button
                                        onClick={() => handleDeleteCategory(category.id!)}
                                        disabled={isDeleting}
                                        type="button"
                                        className="py-2 leading-none px-3 font-medium text-red-600 hover:text-red-500 duration-150 hover:bg-gray-50 rounded-lg"
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CategoryCardComponent;
