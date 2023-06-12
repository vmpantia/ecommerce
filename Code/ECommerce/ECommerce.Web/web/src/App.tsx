import { BrowserRouter, Route, Routes } from "react-router-dom"

//Components
import PrivateRoute from "./components/PrivateRoute"

//Pages
import Login from "./pages/Login"
import Register from "./pages/Register"
import Layout from "./pages/Layout"

const App = () => {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/login"    element={<Login />}/>
      <Route path="/register" element={<Register />}/>
      <Route path="/"         element={<PrivateRoute><Layout /></PrivateRoute>}>
      </Route>
    </Routes>
  </BrowserRouter>
  )
}

export default App
