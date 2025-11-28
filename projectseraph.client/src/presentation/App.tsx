import { useEffect, useState } from 'react';
import './App.css';
import Header from './components/Header.tsx';
import MeasurementButton from './components/MeasurementButton.tsx';

function App() {
    return (
        <div>
            <Header />
            <MeasurementButton text="Mål Blodsukker" route="#TODO" />
        </div>
    )
}

export default App;