import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { toast } from 'react-toastify'; 
import { useLoginUserMutation } from '../services/UserServices/UserServices';
import { useAppDispatch } from '../app/hook';
import { setToken, setUserForLogin } from '../features/UserSlice';
interface FormData {
  email: string;
  password: string;
}

export const useLogin = () => {
  const [formData, setFormData] = useState<FormData>({ email: '', password: '' });
  const [loginUser, { isLoading }] = useLoginUserMutation();
  const [errorMessage, setErrorMessage] = useState('');
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
  
    try {
      const response = await loginUser(formData).unwrap();
  
      // Access the token directly from the response object
      const token = response.token;
  
      if (token) {
        localStorage.setItem('token', token);
        dispatch(setToken(token));
    
        toast.success('Login successful');
        navigate('/dashboard'); 
      } else {
        setErrorMessage('Invalid token received');
      }
    } catch (err: any) {
      if (err.data?.errors) {
        setErrorMessage(err.data.errors.email || 'Invalid email or password');
      } else {
        setErrorMessage('An error occurred');
      }
    }
  };

  return { handleChange, handleSubmit, formData, isLoading, errorMessage };
};