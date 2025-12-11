import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './presentation/App.tsx'
import {
    BrowserRouter as Router,
} from 'react-router-dom';
import { UserProvider } from './application/UserContext.tsx';

//router og userprovider wrapper hele appen saa de altid er tilgaengelige
createRoot(document.getElementById('root')!).render(
    <Router>
        <UserProvider>
            <StrictMode>
                <App />
            </StrictMode>
        </UserProvider>
    </Router>,
)
