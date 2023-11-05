import {Home} from "./components/Router/Home";
import {Counter} from "./components/Router/Counter";
import {FetchData} from "./components/Router/FetchData";
import {Contacts} from "./components/Router/Contacts";
import {Phones} from "./components/Router/Phones";
import {Categories} from "./components/Router/Categories";

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
    element: <Contacts/>
  },
  {
    path: '/phones',
    element: <Phones/>
  },
  {
    path: '/categories',
    element: <Categories/>
  }
];

export default AppRoutes;
