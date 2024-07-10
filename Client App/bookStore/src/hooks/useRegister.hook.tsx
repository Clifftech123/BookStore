import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { useRegisterUserMutation } from '../services/UserServices/UserServices';
import { useAppDispatch } from '../app/hook';
import { setUserForLogin } from '../features/UserSlice';

interface RegisterData {
  email: string;
  password: string;
  name: string;
}

export const useRegister = () => {
  const [registerData, setRegisterData] = useState<RegisterData>({ email: '', password: '', name: '' });
  const [registerUser, { isLoading, error }] = useRegisterUserMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setRegisterData({ ...registerData, [e.target.name]: e.target.value });
  };

  console.log(registerData);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      await registerUser(registerData).unwrap();
       dispatch(setUserForLogin(registerData));
      toast.success('Registration successful');
      navigate('/');
    } catch (err) {
       console.log(err);
    }
  };

  return { handleChange, handleSubmit, registerData, isLoading };
};
