import { BoardPage } from "./pages/BoardPage/BoardPage";
import { HomePage } from "./pages/HomePage/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import { BOARD_ROUTE, HOME_ROUTE, LOGIN_ROUTE, REGISTER_ROUTE } from "./utils/route-const";

export const unAuthRoutes = [
    {
        path : LOGIN_ROUTE,
        element : LoginPage
    },
    {
        path : REGISTER_ROUTE,
        element : RegisterPage
    },
]

export const authRoutes = [
    {
        path : HOME_ROUTE,
        element : HomePage
    },
    {
        path : BOARD_ROUTE,
        element: BoardPage
    }
]