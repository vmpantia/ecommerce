import { BrowserRouter, Route, Routes } from "react-router-dom"

//Pages
import Login from "./pages/Login"
import Register from "./pages/Register"

const App = () => {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/login"    element={<Login />}/>
      <Route path="/register"    element={<Register />}/>
      <Route path="/"               element={<Login />}>
      </Route>
    </Routes>
  </BrowserRouter>
  )
}

export default App
