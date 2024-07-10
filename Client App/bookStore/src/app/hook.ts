
import { TypedUseSelectorHook, useDispatch } from 'react-redux'
import type { RootState, AppDispatch } from './store'
import { useSelector } from 'react-redux'


export const useAppDispatch: () => AppDispatch = useDispatch
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector

