import { BrowserRouter, Route, Routes } from "react-router-dom";

//Pages
import Register from "./pages/Register";
import Layout from "./pages/Layout";
import NotFound from "./pages/NotFound";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/register" element={<Register />} /> {/* Display Register if the URL is /register */}
        <Route path="/" element={<Layout />}>
          {/* Child Routes */}
        </Route>
        <Route path="*" element={<NotFound />} /> {/* Display Page Not Found if invalid URL */}
      </Routes>
    </BrowserRouter>
  );
};

export default App;
