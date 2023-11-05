import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Authors } from "./components/Authors";
import { Home } from "./components/Home";
import {CreateAuthor} from "./components/CreateAuthor";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/authors',
    element: <Authors />
  },
  {
    path: '/createAuthor',
    element: <CreateAuthor />
  },
  
];

export default AppRoutes;
