import { Routes, Route } from 'react-router-dom';
import './App.css';
import Header from './components/Header.tsx';
import BloodSugarForm from './components/BloodSugarForm.tsx';
import Home from './components/Home.tsx';
import NotFound from './components/NotFound.tsx';
import BloodpressureForm from './components/BloodpressureForm.tsx';
import User from '../domain/User.ts';

//REFACTOR
const user = new User(true);
//mellem header og routes skal vi have nogle if/conditional rendering paa:
//1: er det en nurse eller en borger?
//2: er noget (eg booleans?) en indikation paa at der skal tages tests?
function App() {
    return (
        <div>
            <Header />
            
            
            <Routes>
                <Route path="/" element={<Home user={user} /> } />
                <Route path="/measurebloodpressure" element={<BloodpressureForm /> } />
                <Route path="/measurebloodsugar" element={<BloodSugarForm /> } />
                <Route path="*" element={<NotFound /> } />
            </Routes>
        </div>
    )
}

export default App;