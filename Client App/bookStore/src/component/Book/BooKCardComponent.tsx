import { toast } from "react-toastify";
import { useDeleteBookMutation, useGetBooksQuery } from "../../services/BookServices/BookServices";
import { Link } from "react-router-dom";

const BooKCardComponent = () => {

    const { data: bookData } = useGetBooksQuery();
    const [deleteBook, { isLoading: isDeleting }] = useDeleteBookMutation();


    console.log(bookData);


// handle delete book
const handleDeleteBook = async (id: number) => {
    try {
        await deleteBook(id).unwrap();
        toast.success('Book deleted successfully');
    } catch (error) {
        console.error('Error deleting book:', error);
    }
};
    return (
        <div className="max-w-screen-xl mx-auto px-4 md:px-8">
            <div className="items-start justify-between md:flex">
                <div className="max-w-lg">
                    <h3 className="text-gray-800 text-xl font-bold sm:text-2xl">
                        Book List
                    </h3>
                    <p className="text-gray-600 mt-2">
                        Browse our collection of books.
                    </p>
                </div>
                <div className="mt-3 md:mt-0">
                    <Link 
                    className="inline-block px-4 py-2 text-white duration-150 font-medium bg-indigo-600 rounded-lg hover:bg-indigo-500 active:bg-indigo-700 md:text-sm"
                     to="/create-book"
                    >
                        Add Book
                    </Link>
                </div>
            </div>
            <div className="mt-12 shadow-sm border rounded-lg overflow-x-auto">
                <table className="w-full table-auto text-sm text-left">
                    <thead className="bg-gray-50 text-gray-600 font-medium border-b">
                        <tr>
                            <th className="py-3 px-6">Book Name</th>
                            <th className="py-3 px-6">Description</th>
                            <th className="py-3 px-6">Category</th>
                            <th className="py-3 px-6">Price</th>
                            <th className="py-3 px-6"></th>
                        </tr>
                    </thead>
                    <tbody className="text-gray-600 divide-y">
                        {bookData?.map((book, idx) => (
                            <tr key={idx}>
                                <td className="px-6 py-4 whitespace-nowrap">{book.name}</td>
                                <td className="px-6 py-4 whitespace-nowrap">{book.description}</td>
                                <td className="px-6 py-4 whitespace-nowrap">{book.category}</td>
                                <td className="px-6 py-4 whitespace-nowrap">${book.price}</td>
                                <td className="text-right px-6 whitespace-nowrap">
                                  
                                    <button
                                     onClick={() => handleDeleteBook(book.id)}
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

export default BooKCardComponent;
