import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import {Contacts} from "./components/Contacts";
import {Phones} from "./components/Phones";
import {Categories} from "./components/Categories";
import {EditContact} from "./components/EditContact";

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
    path: '/contacts',
    element: <Contacts />
  },
  {
    path: '/phones',
    element: <Phones />
  },
  {
    path: '/categories',
    element: <Categories />
  },
  {
    path: '/editContact/:id',
    element: <EditContact/>
  }
];

export default AppRoutes;
