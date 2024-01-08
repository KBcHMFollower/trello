import React, { useEffect } from 'react';
import styles from './App.module.scss'
import SignInPage from '../../pages/RegisterPage';
import { BrowserRouter } from 'react-router-dom';
import { AppRouter } from '../AppRouter';
import { useAppDispatch, useAppSelector } from '../../hooks/redux';
import { LOGIN_ROUTE, REGISTER_ROUTE } from '../../utils/route-const';
import Header from '../Header/Header';
import { fetchCheckAuth } from '../../Api/thunks/auth-thunks';
import { Loading } from '../Loading/Loading';

function App() {

  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchCheckAuth());
  }, [])

  const { isAuth, isLoading } = useAppSelector(state => ({
    isAuth: state.user.isAuth,
    isLoading : state.user.isLoading
  }))

  return (
    <div>
      {isAuth && <Header />}
      <BrowserRouter>
        <div className='container mx-auto bg-gray-100 min-h-screen'>
          {isLoading ? (
            <>
            <Loading/>
            </>
            ):(
              <>
              <AppRouter isAuth={isAuth} />
              </>
            )
          }   
        </div>
      </BrowserRouter>
    </div>
  );
}

export default App;
