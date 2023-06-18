import { BrowserRouter, Route, Routes } from "react-router-dom";

//Pages
import Register from "./pages/Register";
import Layout from "./pages/Layout";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import PrivateRoute from "./pages/PrivateRoute";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/Login" element={<Login />} /> {/* Display login if the URL is /login */}
        <Route path="/register" element={<Register />} /> {/* Display Register if the URL is /register */}
        <Route path="/" element={<PrivateRoute children={<Layout />} />}>
          {/* Child Routes */}
        </Route>
        <Route path="*" element={<NotFound />} /> {/* Display Page Not Found if invalid URL */}
      </Routes>
    </BrowserRouter>
  );
};

export default App;
