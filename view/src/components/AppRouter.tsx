import React, { FC } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom'
import { BOARD_ROUTE, HOME_ROUTE, LOGIN_ROUTE } from '../utils/route-const'
import { authRoutes, unAuthRoutes } from '../routes'
import { BoardPage } from '../pages/BoardPage/BoardPage'

type PropsType = {
    isAuth:boolean
}

export const AppRouter:FC<PropsType> = ({isAuth}) => {
  return (
    <Routes>
        {!isAuth ? (
            <>
            {unAuthRoutes.map(e=><Route path={e.path} Component={e.element}/>)}
            <Route path='*' element={<Navigate to={LOGIN_ROUTE} replace />} />
            </>
        ) : (
            <>
            {authRoutes.map(e=><Route path={e.path} Component={e.element}/>)}
            <Route path='*' element={<Navigate to={HOME_ROUTE} replace />}/>
            </>
        )}
    </Routes>
  )
}
