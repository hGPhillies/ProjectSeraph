import Login from './Login.tsx';
import MeasurementButton from './MeasurementButton.tsx';
import { userContext } from '../../application/UserContext.tsx'

function Home() {
    //gets our global state for user
    const { user } = userContext();
    return (
        <div>
            {
                !user.loggedIn && (
                    <div>
                        <h2>Indtast CPR-nummer og adgangskode</h2>
                        <Login />
                    </div>
            )}
            {
                user.loggedIn && (
                    <div>
                        {
                        user.nurse ?
                            <h2>Sygeplejerske</h2>
                            :
                            <div>
                                <MeasurementButton text="Mål Blodtryk" link="/measurebloodpressure" />
                                <MeasurementButton text="Mål Blodsukker" link="/measurebloodsugar" />
                            </div>
                        }
                    </div>
                )

                
            }
        </div>
    );
}

export default Home;